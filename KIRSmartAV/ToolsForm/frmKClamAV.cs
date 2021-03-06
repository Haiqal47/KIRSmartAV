﻿/*   
      frmKClamAV.cs (KIRSmartAV)
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

using KIRSmartAV.Core;
using MClamSlim;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using KIRSmartAV.ApplicationServices;
using KIRSmartAV.Localization;

namespace KIRSmartAV.ToolsForms
{
    public partial class frmKClamAV : Form
    {
        private static LogManager _logger = LogManager.GetClassLogger();

        public frmKClamAV()
        {
            InitializeComponent();
        }

        #region Structures and Methods
        private struct MessageParams
        {
            public string Path { get; set; }
            public string ObjectScanned { get; set; }
            public string Status { get; set; }
            public string VirusName { get; set; }
        }
        
        private void ReportProgress(object sender, int progressPercentage, string statusText, string path = Commons.TextDots, string virusName = Commons.TextDots)
        {
            var msgReport = new MessageParams()
            {
                ObjectScanned = Path.GetFileName(path),
                Path = path,
                Status = statusText,
                VirusName = virusName,
            };

            var bw = (BackgroundWorker)sender;
            bw.ReportProgress(progressPercentage, msgReport);
        }
        #endregion

        #region Form Logic
        private void cmdBrowse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = fbd.SelectedPath;
            }
        }

        private void cmdStartScan_Click(object sender, EventArgs e)
        {
            if (cmdStartScan.ImageIndex == 0)
            {
                if (txtPath.TextLength < 1)
                {
                    MessageBox.Show(strings.ScanSelectPathText, strings.KIRSmartAVTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                bwPindai.RunWorkerAsync(txtPath.Text);
                _logger.Info("User starts a scan action.");
            }
            else
            {
                bwPerbaiki.RunWorkerAsync(lvDeteksi.Items.Count);
                _logger.Info("User starts a repair action.");
            }

            cmdBrowse.Enabled = false;
            cmdStartScan.Enabled = false;
            cmdStopScan.Enabled = true;
        }

        private void cmdStopScan_Click(object sender, EventArgs e)
        {
            if (bwPindai.IsBusy)
            {
                bwPindai.CancelAsync();
            }
            if (bwPerbaiki.IsBusy)
            {
                bwPerbaiki.CancelAsync();
            }

            cmdStopScan.Enabled = false;
        }
        #endregion
        
        #region Scaning BackgroundWorker Thread
        private void bwPindai_DoWork(object sender, DoWorkEventArgs e)
        {
            // prepare
            var timeCounter = new System.Diagnostics.Stopwatch();
            timeCounter.Start();

            var scanPath = e.Argument.ToString();
            var scanFileList = new List<string>();
            double progressPercentage = 0.0;
            
            // iterate to count
            ReportProgress(sender, 99, strings.ScanCountText);
            _logger.Debug("Preparing to count files. Directory: " + scanPath);
            foreach (FileData currentFile in FastIO.EnumerateFiles(scanPath, SearchOption.AllDirectories))
            {
                // check for cancellation
                if (bwPindai.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                if (!currentFile.Name.EndsWith(Commons.VirusExtension))
                {
                    scanFileList.Add(currentFile.FullPath);
                }
            }

            // initialize libclamav
            ReportProgress(sender, 99, strings.ScanPrepareClamav);
            _logger.Debug("Count completed. Total: " + scanFileList.Count.ToString());
            
            try
            {
                // create engine instance
                _logger.Debug("Creating cl_engine.");
                using (var scanEngine = ClamEngine.CreateNew())
                {
                    // load database
                    ReportProgress(sender, 99, strings.ScanPrepareDatabase);
                    _logger.Debug("Loading database...");
                    scanEngine.LoadCvdFile(Commons.DatabasePath);

                    // check for cancellation
                    if (bwPindai.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    // compile database
                    ReportProgress(sender, 99, strings.ScanCompileEngine);
                    _logger.Debug("Compiling engine...");
                    scanEngine.Compile();

                    // scan
                    var fileCount = scanFileList.Count;
                    for (int i = 0; i < fileCount; i++)
                    {
                        // check for cancellation
                        if (bwPindai.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        // open file descriptor
                        using (FileEntry currentFile = FileEntry.OpenFile(scanFileList[i]))
                        {
                            progressPercentage = (i + 1) / (double)fileCount * 100;

                            var scanResult = scanEngine.ScanFile(currentFile);
                            if (scanResult.IsVirus)
                            {
                                ReportProgress(sender, Convert.ToInt32(progressPercentage), strings.ScanVirusDetected, scanResult.FullPath, scanResult.VirusName);
                                _logger.Debug("Virus detected. Name: " + scanResult.VirusName + ", Path: " + scanResult.FullPath);
                            }
                            else
                            {
                                ReportProgress(sender, Convert.ToInt32(progressPercentage), strings.ScaningStill, scanResult.FullPath);
                            }
                        } //End using FileEntry
                    } //End for loop
                    _logger.Debug("Scan completed. Disposing engine.");
                } //End using ClamEngine
            }
            catch (Exception ex)
            {
                _logger.Error("Libclamav encoutered an error.", ex);
                MessageBox.Show(strings.ScanEngineError, strings.KIRSmartAVTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            timeCounter.Stop();
            _logger.Debug("Scan time elapsed: " + timeCounter.ElapsedMilliseconds.ToString());
        }

        private void bwPindai_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change status texts
            prgStatus.Value = e.ProgressPercentage;
            if (e.ProgressPercentage == 99)
                prgStatus.Style = ProgressBarStyle.Marquee;
            else
                prgStatus.Style = ProgressBarStyle.Blocks;

            // report to UI
            var msgs = (MessageParams)e.UserState;
            lblStatus.Text = msgs.Status;
            lblFilePath.Text = msgs.Path;
            lblFile.Text = msgs.ObjectScanned;

            // Add to lv
            if (msgs.VirusName != Commons.TextDots)
            {
                lblVirname.Text = msgs.VirusName;

                var lvi = lvDeteksi.Items.Add(msgs.ObjectScanned);
                lvi.SubItems.Add(msgs.VirusName);
                lvi.SubItems.Add(msgs.Path);
                lvi.ImageIndex = 0;
            }
        }

        private void bwPindai_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        { 
            lblFile.Text = Commons.TextDots;
            lblFilePath.Text = Commons.TextDots;

            cmdStopScan.Enabled = false;
            cmdStartScan.Enabled = true;

            if (lvDeteksi.Items.Count > 0)
            {
                lblStatus.Text = strings.ScanCompletedViruses;

                cmdStartScan.Text = strings.ScanRepairText;
                cmdStartScan.ImageIndex = 2;

                cmdBrowse.Enabled = false;
                _logger.Info("Scan completed. Virus found: " + lvDeteksi.Items.Count.ToString());
            }
            else
            {
                lblStatus.Text = strings.ScanCompletedNoVirus;

                cmdStartScan.Text = strings.ScanStartText;
                cmdStartScan.ImageIndex = 0;
                txtPath.Text = "";

                cmdBrowse.Enabled = true;
                _logger.Info("Scan completed. No virus detected.");
            }
        }
        #endregion

        #region Repairing BackgroundWorker
        private void bwPerbaiki_DoWork(object sender, DoWorkEventArgs e)
        {
            // prepare variables
            var itemsCount = Convert.ToInt32(e.Argument);
            var crpytoService = new FileEncoder();
            var chestPath = Commons.GetChestFolder();

            for (int i = 0; i < itemsCount; i++)
            {
                var currentItem = lvDeteksi.SafeGetItem(i);
                var filePath = currentItem[2];
                int imgIndex = 2;

                // check for cancellation
                if (bwPerbaiki.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // encode first
                try
                {
                    crpytoService.EncryptFile(filePath, Commons.GenerateFilePath(filePath, chestPath));
                    imgIndex = 1;
                }
                catch (Exception ex)
                {
                    imgIndex = 2;
                    _logger.Error("Can't encrypt virus file. Path: " + filePath, ex);
                }

                // delete original file
                try
                {
                    File.Delete(filePath);
                    imgIndex = 1;
                }
                catch (Exception ex)
                {
                    imgIndex = 2;
                    _logger.Error("Can't delete virus file. Path: " + filePath, ex);
                }

                // update UI
                lvDeteksi.SafeChangeItem(i, imgIndex);

                double progressPercentage = (i + 1) / (double)itemsCount * 100;
                ReportProgress(sender, Convert.ToInt32(progressPercentage), strings.ScanRepairing, filePath);
            }
        }

        private void bwPerbaiki_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change status texts
            prgStatus.Value = e.ProgressPercentage;
            if (e.ProgressPercentage == 99)
                prgStatus.Style = ProgressBarStyle.Marquee;
            else
                prgStatus.Style = ProgressBarStyle.Blocks;

            // report to UI
            var msgs = (MessageParams)e.UserState;
            lblFilePath.Text = msgs.Path;
            lblFile.Text = msgs.ObjectScanned;
            lblStatus.Text = msgs.Status;
        }

        private void bwPerbaiki_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cmdBrowse.Enabled = true;
            cmdStopScan.Enabled = false;

            cmdStartScan.Text = strings.ScanStartText;
            cmdStartScan.ImageIndex = 0;
            cmdStartScan.Enabled = true;

            _logger.Info("Repair action finished.");
        }
        #endregion
    }
}
