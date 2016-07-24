/*   
      frmKUnhide.cs (KIRSmartAV)
      ============================================
      Copyright(C) 2016  Fahmi Noor Fiqri
  
      This program is free software: you can redistribute it and/or modify
      it under the terms of the GNU Lesser General Public License as published by
      the Free Software Foundation, either version 3 of the License, or
      (at your option) any later version.
  
      This program is distributed in the hope that it will be useful,
      but WITHOUT ANY WARRANTY; without even the implied warranty of   
      MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
      GNU Lesser General Public License for more details.
  
      You should have received a copy of the GNU Lesser General Public License
      along with this program. If not, see <http://www.gnu.org/licenses/>.
*/

using KIRSmartAV.ApplicationServices;
using KIRSmartAV.Core;
using KIRSmartAV.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace KIRSmartAV.ToolsForms
{
    public partial class frmKUnhide : Form
    {
        private static LogManager _logger = LogManager.GetClassLogger();

        public frmKUnhide()
        {
            InitializeComponent();
        }

        #region Structures and Methods
        private struct DriveParameters
        {
            public string DriveLetter { get; set; }
            public long UsedSpace { get; set; }
            public bool FullCheck { get; set; }
            public bool DeleteShortcuts { get; set; }
        }

        private void RepairAttribute(string filePath)
        {
            // normalize filename
            var normalizedFileName = Path.GetFileName(filePath).ToLowerInvariant();
            FileAttributes attrNew = FileAttributes.Normal;

            // check if given name is reserved as hidden
            foreach (string reservedName in Commons.ReservedNames)
            {
                if (normalizedFileName == reservedName)
                {
                    attrNew = FileAttributes.Hidden;
                }
                else
                {
                    attrNew = FileAttributes.Normal;
                }
            }
            
            // change attribute
            try
            {
                FastIO.SetFileAttribute(filePath, attrNew, false);
            }
            catch (Exception ex)
            {
                _logger.Error("Set attribute failed. Path: " + filePath, ex);
            }
        }

        private void DeleteShortcut(string filePath)
        {
            // delete shortcut
            if (filePath.EndsWith(".lnk"))
            {
                try
                {
                    File.Delete(filePath);
                    _logger.Info("Deleted shortcut file: " + filePath);
                }
                catch (Exception ex)
                {
                    _logger.Error("Delete shortcut failed.", ex);
                }
            }
        }
        #endregion

        #region Form Logic
        private void cboDiskalepas_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedDrive = (DriveData)cboDiskalepas.SelectedItem;
            if (selectedDrive == null)
            {
                return;
            }

            // fill up UI
            lblFree.Text = Helpers.RoundByteSize(selectedDrive.FreeSpace);
            lblTotal.Text = Helpers.RoundByteSize(selectedDrive.TotalSpace);
            lblUsed.Text = Helpers.RoundByteSize(selectedDrive.UsedSpace);

            // draw UI
            capacityChart1.DrawPie(selectedDrive.TotalSpace, selectedDrive.FreeSpace);
        }

        private void cmdPerbarui_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cboDiskalepas.DataSource = DriveData.GetDrives();
        }

        private void cmdPerbaiki_Click(object sender, EventArgs e)
        {
            if (cmdPerbaiki.ImageIndex == 0)
            {
                if (cboDiskalepas.SelectedItem == null)
                {
                    MessageBox.Show(strings.NotifyDriveNotSelectedText, strings.NotifyDriveNotSelectedTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                var selectedDrive = (DriveData)cboDiskalepas.SelectedItem;
                var args = new DriveParameters()
                {
                    DriveLetter = selectedDrive.DriveLetter,
                    UsedSpace = selectedDrive.UsedSpace,
                    FullCheck = chkFullCheck.Checked,
                    DeleteShortcuts = chkHapusShortcut.Checked,
                };
                bwPerbaiki.RunWorkerAsync(args);

                prgProgress.Value = 0;
                prgProgress.Style = ProgressBarStyle.Marquee;

                cmdPerbaiki.Text = strings.StopText;
                cmdPerbaiki.ImageIndex = 1;

                _logger.Info("Uhide action started.");
            }
            else
            {
                if (bwPerbaiki.IsBusy)
                {
                    bwPerbaiki.CancelAsync();
                }
                    
                cmdPerbaiki.Enabled = false;
            }
        }
        #endregion

        #region BackgroundWorker Thread
        private void bwPerbaiki_DoWork(object sender, DoWorkEventArgs e)
        {
            var args = (DriveParameters)e.Argument;
            bwPerbaiki.ReportProgress(99);
            _logger.Info("KUnhide action started. Drive: " + args.DriveLetter);

            var hiddenPath = args.DriveLetter + "\\" + FastIO.BlankSpaceCharacter;
            try
            {
                // set folder attribute
                FastIO.SetFileAttribute(hiddenPath, FileAttributes.Normal, false);
                _logger.Debug("Set no-name folder attribute to normal.");
            }
            catch (Exception ex)
            {
                _logger.Error("Can't normalize no-name folder attribute.", ex);
            }

            try
            {
                // rename folder
                FastIO.MoveFile(hiddenPath, Path.Combine(args.DriveLetter, Commons.KCrecoveredName), false);
                _logger.Debug("Rename no-name directory to " + Commons.KCrecoveredName);
            }
            catch (Exception ex)
            {
                _logger.Error("Can't rename no-name folder.", ex);
            }

            //Restore folder attribute
            _logger.Info("Restoring folders attribute.");
            foreach (string currentFolder in FastIO.EnumerateDirectories(args.DriveLetter, SearchOption.AllDirectories))
            {
                if (bwPerbaiki.CancellationPending) return;

                RepairAttribute(currentFolder);
            }

            //Restore files attribute
            if (args.FullCheck)
            {
                _logger.Debug("Through analysis is enabled.");

                // do only if using through analysis
                long repairedSize = 0;
                _logger.Debug("Restoring files attribute.");
                foreach (FileData currentFile in FastIO.EnumerateFiles(args.DriveLetter, SearchOption.AllDirectories))
                {
                    if (bwPerbaiki.CancellationPending) return;

                    // repair attribute
                    RepairAttribute(currentFile.FullPath);

                    // delete shortcut
                    if (args.DeleteShortcuts)
                    {
                        DeleteShortcut(currentFile.FullPath);
                    }

                    // calculate progress
                    repairedSize += currentFile.Size;
                    double progress = (double)repairedSize / args.UsedSpace * 100;
                    if (progress < 0 || progress > 100) progress = 99;

                    bwPerbaiki.ReportProgress((int)progress);
                }
            }

            _logger.Info("Unhide action finished.");
        }

        private void bwPerbaiki_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            prgProgress.Value = 100;
            prgProgress.Style = ProgressBarStyle.Blocks;

            cmdPerbaiki.Text = strings.KUnhide_RepairAttribute;
            cmdPerbaiki.ImageIndex = 0;
            cmdPerbaiki.Enabled = true;
        }

        private void bwPerbaiki_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prgProgress.Value = e.ProgressPercentage;

            if (e.ProgressPercentage == 99)
            {
                prgProgress.Style = ProgressBarStyle.Marquee;
            }    
            else
            {
                prgProgress.Style = ProgressBarStyle.Blocks;
            }
        }
        #endregion
    }
}
