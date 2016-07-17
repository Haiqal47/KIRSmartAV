/*   
      frmKCryptor.cs (KIRSmartAV)
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
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KIRSmartAV.ToolsForms
{
    public partial class frmKCryptor : Form
    {
        private FileEncoder _encoder = null;
        private static LogManager _logger = LogManager.GetClassLogger();

        public frmKCryptor()
        {
            InitializeComponent();

            _encoder = new FileEncoder();
            _encoder.ProgressChanged += FileEncoder_ProgressChanged;
            _encoder.ProgressCompleted += FileEncoder_ProgressCompleted;
        }

        private void FileEncoder_ProgressCompleted(object sender, EventArgs e)
        {
            MessageBox.Show(strings.EncodeDecodeCompletedText, strings.EncodeDecodeCompletedTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            cmdExecute.Text = strings.StartText;
            cmdExecute.Tag = "START";
            cmdExecute.Enabled = true;
        }

        private void FileEncoder_ProgressChanged(object sender, FileEncoder.EncodeProgressChanged e)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<int>(((x) =>
                {
                    prgStatus.Value = x;
                })), e.ProgressPercentage);
            }
        }

        #region Encode Parts
        private void cmdInputEncode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofd.DefaultExt = AioHelpers.InputAnyFileMask;
            ofd.Filter = "All files (*.*)|" + AioHelpers.InputAnyFileMask;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtInputEncode.Text = ofd.FileName;
            }
        }

        private void cmdOutputEncode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtInputEncode.TextLength <= 2)
            {
                MessageBox.Show(strings.ErrorFileNotSelectedText, strings.ErrorFileNotSelectedTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // multidotted format : *.<ext>.<vExt>
            var newExt = "*" + Path.GetExtension(txtInputEncode.Text) + "." + AioHelpers.VirusExtension;
            sfd.DefaultExt = newExt;
            sfd.Filter = string.Format("Encrypted ({0})|{0}", newExt);

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                txtOutputEncode.Text = sfd.FileName;
            }
        }
        #endregion

        #region Decode Parts
        private void cmdDecodeInput_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofd.DefaultExt = AioHelpers.InputMultidottedMask;
            ofd.Filter = "Encrypted (" + AioHelpers.InputMultidottedMask + ")|" + AioHelpers.InputMultidottedMask;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtInputDecode.Text = ofd.FileName;
            }
        }

        private void cmdDecodeOutput_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtInputDecode.TextLength <= 2)
            {
                MessageBox.Show(strings.ErrorFileNotSelectedText, strings.ErrorFileNotSelectedTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var newExt = "*" + Regex.Match(txtInputDecode.Text, @"\..*").Value.Replace("." + AioHelpers.VirusExtension, "");
            sfd.DefaultExt = newExt;
            sfd.Filter = string.Format("Decrypted ({0})|{0}", newExt);

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                txtOutputDecode.Text = sfd.FileName;
            }
        }
        #endregion

        private void cmdExecute_Click(object sender, EventArgs e)
        {
            if ("START" == cmdExecute.Tag.ToString())
            {
                if (tabControl1.SelectedTab.Tag.ToString() == "ENCODE")
                {
                    if (txtInputEncode.TextLength == 0 || txtOutputEncode.TextLength == 0)
                    {
                        MessageBox.Show(strings.ErrorFileNotSelectedText, strings.ErrorFileNotSelectedTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    _encoder.EncryptFileAsync(txtInputEncode.Text, txtOutputEncode.Text);
                }
                else
                {
                    if (txtInputDecode.TextLength == 0 || txtOutputDecode.TextLength == 0)
                    {
                         MessageBox.Show(strings.ErrorFileNotSelectedText, strings.ErrorFileNotSelectedTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    _encoder.DecryptFileAsync(txtInputDecode.Text, txtOutputDecode.Text);
                }
                _logger.Info("Starting to transforming file blocks.");
                cmdExecute.Text = strings.StopText;
                cmdExecute.Tag = "STOP";
            }
            else
            {
                _logger.Info("Stop transforming file blocks.");
                _encoder.RequestStop();
                cmdExecute.Enabled = false;
            }
        }

        #region Dispose Override
        // below is moved Dispose method from WinForm Designer file.
        // it's moved because we need to dispose FileEncoder instance
        // when the form is being disposed.
        //
        //              Microsoft Code Analysis CA2213
        //============================================================

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // used to solve CA2213
                if (_encoder != null)
                {
                    _encoder.Dispose();
                }
                if (components != null)
                {
                    components.Dispose();
                }
            }
            
            base.Dispose(disposing);
        }
        #endregion
    }
}