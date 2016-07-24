/*   
      SettingsForm.cs (KIRSmartAV)
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using KIRSmartAV.Properties;
using Microsoft.Win32;
using KIRSmartAV.ApplicationServices;
using KIRSmartAV.Localization;

namespace KIRSmartAV.Forms
{
    public partial class SettingsForm : Form
    {
        private static Settings _settings = Settings.Default;        
        private static LogManager _logger = LogManager.GetClassLogger();
        private bool _startupLastCheck = false;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            CheckStatus();
        }

        #region Methods
        private void CheckStatus()
        {
            // Startup
            using (var regKey = Registry.CurrentUser.OpenSubKey(Commons.StartupRegistryPath, true))
            {
                var regValue = regKey.GetValue("KIRSmartAV");
                chkRunOnStartup.Checked = ((regValue != null) && (regValue.ToString() != ""));
                _startupLastCheck = chkRunOnStartup.Checked;
            }

            // Quick fix
            chkEnableQuickFix.Checked = _settings.QuickFixEnabled;
            chkProcessSubdir.Checked = _settings.QuickFixRecrusive;

            // Chest path
            txtVirusChestDir.Text = _settings.ChestPath;

            // Language
            cboLanguage.SelectedIndex = _settings.UILanguage == "Indonesia" ? 0 : 1;
        }

        private void SaveChanges()
        {
            // quickfix
            _settings.QuickFixEnabled = chkEnableQuickFix.Checked;
            _settings.QuickFixRecrusive = chkProcessSubdir.Checked;

            // virus chest
            _settings.ChestPath = txtVirusChestDir.Text;

            // language
            _settings.UILanguage = cboLanguage.SelectedIndex == 0 ? "Indonesia" : "English";
            _settings.Save();

            // startup
            if (_startupLastCheck != chkRunOnStartup.Checked)
            {
                try
                {
                    using (var regKey = Registry.CurrentUser.OpenSubKey(Commons.StartupRegistryPath, true))
                    {
                        if (chkRunOnStartup.Checked)
                        {
                            regKey.SetValue("KIRSmartAV", Application.ExecutablePath + @" /startup", RegistryValueKind.String);
                        }
                        else
                        {
                            regKey.SetValue("KIRSmartAV", "");
                        }
                    }
                    _logger.Debug("Startup value changed.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(strings.UnableChangeRegistryText, strings.KIRSmartAVTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    _logger.Error("Can't change entry in registry.", ex);
                }
            }

            _logger.Info("Settings saved. May need to restart.");
            this.Close();
        }
        #endregion

        #region Buttons
        private void cmdOK_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Misc
        private void chkElevationManifest_CheckedChanged(object sender, EventArgs e)
        {
            // this feature is disabled
            if (chkElevationManifest.Checked)
            {
                MessageBox.Show(strings.AddElevationManifestText, strings.KIRSmartAVTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                chkElevationManifest.Checked = false;
                _logger.Info("User tried to use elevation manifest. Blocked.");
            }
        }

        private void cmdBrowse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtVirusChestDir.Text = fbd.SelectedPath;
            }
        }

        private void cmdUnset_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtVirusChestDir.Text = "[UNSET]";
        }

        private void chkEnableQuickFix_CheckedChanged(object sender, EventArgs e)
        {
            pnQuickFix.Enabled = chkEnableQuickFix.Checked;
        }
        #endregion
    }
}
