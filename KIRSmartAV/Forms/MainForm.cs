/*   
      MainForm.cs (KIRSmartAV)
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

using System;
using System.Windows.Forms;
using KIRSmartAV.ToolsForms;
using KIRSmartAV.Core;
using KIRSmartAV.Localization;
using KIRSmartAV.ApplicationServices;
using KIRSmartAV.Properties;
using System.IO;
using System.Security.Permissions;
using System.Collections.Generic;

namespace KIRSmartAV.Forms
{
    public partial class MainForm : Form
    {
        private static LogManager _logger = LogManager.GetClassLogger();
        private static Settings _appSettings = Settings.Default;

        private FormsGC _formsHandler = FormsGC.Instance;
        private bool _antivirusCapability = true;

        public MainForm()
        {
            InitializeComponent();
        }

        #region Methods
        private void CheckStatus()
        {
            // update quickfix
            lblStat_QuickFix.Text = _appSettings.QuickFixEnabled ? strings.ActiveText : strings.DeactiveText;
            
            // label version
            lblStat_Version.Text = "v" + Application.ProductVersion;

            // update signatures
            try
            {
                _logger.Debug("Initializing libclamav.");
                MClamSlim.ClamEngine.Initialize();

                _logger.Debug("Counting signatures.");
                lblStat_Database.Text = MClamSlim.AVHelpers.CountSignatures(Commons.DatabasePath).ToString();
                _antivirusCapability = true;
            }
            catch (Exception ex)
            {
                _logger.Error("Can't access libclamav.", ex);
                KcavContext.Instance.ShowBaloonTip(strings.KIRSmartAVTitle, strings.LibclamavError, ToolTipIcon.Warning);
                _antivirusCapability = false;
            }
        }

        private void RefreshListViewItems()
        {
            // clear all items
            lvTools.Items.Clear();
            ListViewItem curItem = null;

            // add items to listview
            curItem = lvTools.Items.Add("KClamAV");
            curItem.SubItems.Add(strings.KClamAV_Desc);
            curItem.ImageIndex = 0;

            curItem = lvTools.Items.Add("KUnhide");
            curItem.SubItems.Add(strings.KUnhide_Desc);
            curItem.ImageIndex = 1;

            curItem = lvTools.Items.Add("KCure");
            curItem.SubItems.Add(strings.KCure_Desc);
            curItem.ImageIndex = 2;

            curItem = lvTools.Items.Add("KProtect");
            curItem.SubItems.Add(strings.KProtect_Desc);
            curItem.ImageIndex = 3;

            curItem = lvTools.Items.Add("KCrypt");
            curItem.SubItems.Add(strings.KProtect_Desc);
            curItem.ImageIndex = 4;

            _logger.Debug("Initialized listview items.");
        }
        #endregion

        #region Buttons
        private void cmdAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _logger.Info("Showing \"AboutForm\"");
            using (var vw = new AboutForm())
                vw.ShowDialog();
        }

        private void cmdSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_formsHandler.HasShownForm)
            {
                MessageBox.Show(strings.CantShowSettings, strings.KIRSmartAVTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                _logger.Info("Showing \"SettingsForm\"");
                using (var vw = new SettingsForm())
                    vw.ShowDialog();
            }
        }

        private void cmdMinimize_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
        }
        #endregion

        #region Form Events
        private void lvTools_DoubleClick(object sender, EventArgs e)
        {
            // check if current focused item is not null
            var selectedTool = lvTools.FocusedItem;
            if (selectedTool == null)
            {
                return;
            }

            // check for evelated process
            bool isElevated = false;
            try
            {
                isElevated = WindowsOS.IsProcessElevated();
            }
            catch (Exception ex)
            {
                _logger.Error("Unable to get elevation status.", ex);
            }

            // pick right form
            switch (selectedTool.ImageIndex)
            {
                case 0:
                    if (_antivirusCapability)
                    {
                        _formsHandler.ShowForm(new frmKClamAV());
                    }
                    else
                    {
                        MessageBox.Show(strings.LibclamavError, strings.KIRSmartAVTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    break;

                case 1:
                    _formsHandler.ShowForm(new frmKUnhide());
                    break;

                case 2:
                    _formsHandler.ShowForm(new frmKCure());
                    break;

                case 3:
                    if (!isElevated)
                    {
                        MessageBox.Show(strings.ErrorNotElevatedText, strings.ErrorNotElevatedTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        _logger.Info("Form showing cancelled. Process not elevated.");
                        return;
                    }
                    else
                    {
                        _formsHandler.ShowForm(new frmKProtect());
                    }
                    break;

                case 4:
                    _formsHandler.ShowForm(new frmKCryptor());
                    break;
            }

            this.Hide();

            _logger.Info("Showing \"" + selectedTool.Text + "\" form.");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshListViewItems();
            CheckStatus();
        }        

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // override rules
            if (e.CloseReason == CloseReason.UserClosing)
            {
                _logger.Debug("Form close triggered, override to hide form.");
                e.Cancel = true;
                this.Hide();
            }
            else
            {
                _logger.Debug("Form close triggered, closing form.");
            }
        }
        #endregion
    }
}
