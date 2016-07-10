/*   
  frmKClamAV.cs (KIRSmartAV)
  ====================================
  This file is a part of Fahmi's work which it's copyright(s)
  belongs to Fahmi Noor Fiqri as the owner of this code. Any 
  reproduction, distribution, manipulation, or other actions that 
  is not explicitly permitted by author is prohibited.
  
  { Feel free to ask to the author to get rights to edit this file
    visit this project at GitHub! https://github.com/fahminlb33 }
  
  Copyright (C) Fahmi Noor Fiqri 2016. All Rights Reserved.
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
        
        private void ReportProgress(object sender, int progressPercentage, string statusText, string path = AioHelpers.TextDots, string virusName = AioHelpers.TextDots)
        {
            var msgReport = new MessageParams()
            {
                ObjectScanned = Path.GetFileName(path),
                Path = path,
                Status = statusText,
                VirusName = virusName,
            };
            (sender as BackgroundWorker).ReportProgress(progressPercentage, msgReport);
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
                bwPindai.CancelAsync();
            if (bwPerbaiki.IsBusy)
                bwPerbaiki.CancelAsync();

            cmdStopScan.Enabled = false;
        }
        #endregion
        
        #region Scaning BackgroundWorker Thread
        private void bwPindai_DoWork(object sender, DoWorkEventArgs e)
        {
            // prepare
            var scanPath = e.Argument.ToString();
            var scanFileList = new List<FileData>();
            double progressPercentage = 0.0;
            
            // iterate to count
            ReportProgress(sender, 99, strings.ScanCountText);
            _logger.Info("Preparing to count files. Directory: " + scanPath);
            foreach (FileData currentFile in FastIO.EnumerateFiles(scanPath, SearchOption.AllDirectories))
            {
                // check for cancellation
                if (bwPerbaiki.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                if (!currentFile.Name.EndsWith(AioHelpers.VirusExtension))
                {
                    scanFileList.Add(currentFile);
                }
            }

            // initialize libclamav
            ReportProgress(sender, 99, strings.ScanPrepareClamav);
            _logger.Info("Count completen. Total: " + scanFileList.Count.ToString());
            
            try
            {
                // create engine instance
                _logger.Debug("Creating cl_engine.");
                using (var scanEngine = ClamEngine.CreateNew())
                {
                    // load database
                    ReportProgress(sender, 99, strings.ScanPrepareDatabase);
                    _logger.Info("Loading database...");
                    scanEngine.LoadCvdFile(AioHelpers.DatabasePath);

                    // compile database
                    ReportProgress(sender, 99, strings.ScanCompileEngine);
                    _logger.Info("Loading database...");
                    scanEngine.Compile();

                    // scan
                    var fileCount = scanFileList.Count;
                    for (int i = 0; i < fileCount; i++)
                    {
                        // check for cancellation
                        if (bwPerbaiki.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        // open file descriptor
                        using (FileEntry currentFile = FileEntry.OpenFile(scanFileList[i].FullPath))
                        {
                            progressPercentage = (i + 1) / (double)fileCount * 100;

                            var scanResult = scanEngine.ScanFile(currentFile);
                            if (scanResult.IsVirus)
                            {
                                ReportProgress(sender, Convert.ToInt32(progressPercentage), strings.ScanVirusDetected, scanResult.FullPath, scanResult.VirusName);
                                _logger.Info("Virus detected. Name: " + scanResult.VirusName + ", Path: " + scanResult.FullPath);
                            }
                            else
                            {
                                ReportProgress(sender, Convert.ToInt32(progressPercentage), strings.ScaningStill, scanResult.FullPath);
                            }
                        } //End using FileEntry
                    } //End for loop
                    _logger.Debug("Scan finished. Disposing engine.");
                } //End using ClamEngine
            }
            catch (Exception ex)
            {
                _logger.Error("Libclamav encoutered an error.", ex);
                MessageBox.Show(strings.ScanEngineError, strings.KIRSmartAVTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
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
            if (msgs.VirusName != AioHelpers.TextDots)
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
            lblFile.Text = AioHelpers.TextDots;
            lblFilePath.Text = AioHelpers.TextDots;

            cmdStopScan.Enabled = false;
            cmdStartScan.Enabled = true;

            if (lvDeteksi.Items.Count > 0)
            {
                lblStatus.Text = strings.ScanCompletedViruses;

                cmdStartScan.Text = strings.ScanRepairText;
                cmdStartScan.ImageIndex = 2;

                cmdBrowse.Enabled = false;
                _logger.Info("Scan completed. Some viruses detected.");
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
            var chestDir = AioHelpers.GetChestFolder();

            // always create directory
            Directory.CreateDirectory(chestDir);
            _logger.Debug("Created chest folder.");

            for (int i = 0; i < itemsCount; i++)
            {
                var currentItem = lvDeteksi.SafeGetItem(i);
                var filePath = currentItem[2];
                int imgIndex = 2;

                // encode first
                try
                {
                    var movedName = AioHelpers.GenerateChestFilename(filePath, chestDir);
                    crpytoService.EncryptFile(filePath, movedName);
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
