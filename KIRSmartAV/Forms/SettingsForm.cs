/*   
  SettingsForm.cs (KIRSmartAV)
  ====================================
  This file is a part of Fahmi's work which it's copyright(s)
  belongs to Fahmi Noor Fiqri as the owner of this code. Any 
  reproduction, distribution, manipulation, or other actions that 
  is not explicitly permitted by author is prohibited.
  
  { Feel free to ask to the author to get rights to edit this file
    visit this project at GitHub! https://github.com/fahminlb33 }
  
  Copyright (C) Fahmi Noor Fiqri 2016. All Rights Reserved.
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
            using (var regKey = Registry.CurrentUser.OpenSubKey(AioHelpers.StartupRegistryPath, true))
            {
                chkRunOnStartup.Checked = ((regKey.GetValue("KIRSmartAV") != null));
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
                    using (var regKey = Registry.CurrentUser.OpenSubKey(AioHelpers.StartupRegistryPath, true))
                    {
                        if (chkRunOnStartup.Checked)
                        {
                            regKey.SetValue("KIRSmartAV", Application.ExecutablePath + @" /startup", RegistryValueKind.String);
                        }
                        else
                        {
                            regKey.DeleteValue("KIRSmartAV");
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
