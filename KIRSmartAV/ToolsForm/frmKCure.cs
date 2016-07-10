/*   
  frmKCure.cs (KIRSmartAV)
  ====================================
  This file is a part of Fahmi's work which it's copyright(s)
  belongs to Fahmi Noor Fiqri as the owner of this code. Any 
  reproduction, distribution, manipulation, or other actions that 
  is not explicitly permitted by author is prohibited.
  
  { Feel free to ask to the author to get rights to edit this file
    visit this project at GitHub! https://github.com/fahminlb33 }
  
  Copyright (C) Fahmi Noor Fiqri 2016. All Rights Reserved.
*/
using KIRSmartAV.ApplicationServices;
using KIRSmartAV.Core;
using KIRSmartAV.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KIRSmartAV.ToolsForms
{
    public partial class frmKCure : Form
    {
        private KCureModule _module = null;
        private static LogManager _logger = LogManager.GetClassLogger();

        public frmKCure()
        {
            InitializeComponent();
        }

        private void cmdPerbarui_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cboDiskalepas.DataSource = DriveData.GetDrives();
            _logger.Debug("Refreshed drive combobox.");
        }

        private void cboDiskalepas_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = (cboDiskalepas.SelectedItem as DriveData).DriveLetter;
            _module = new KCureModule(selected);
            RefreshStatus();
            _logger.Info("User selects new flashdrive. Selected drive: " + selected);
        }

        private void RefreshStatus()
        {
            if (_module == null)
                return;

            //Anti Recycler
            if (_module.IsAntiRecyclerInstalled())
            {
                lblRecycler.Text = strings.ActiveText;
                cmdRecyclerToggle.Text = strings.DeactivateText;
            }
            else
            {
                lblRecycler.Text = strings.DeactiveText;
                cmdRecyclerToggle.Text = strings.ActivateText;
            }

            //Anti Autorun
            if (_module.IsAntiAutorunInstalled())
            {
                lblAutorun.Text = strings.ActiveText;
                cmdAutorunToggle.Text = strings.DeactivateText;
            }
            else
            {
                lblAutorun.Text = strings.DeactiveText;
                cmdAutorunToggle.Text = strings.ActivateText;
            }

            //SafeChest
            if (_module.IsSafeChestInstalled())
            {
                lblSafeChest.Text = strings.ExistText;
                cmdSafeChestToggle.Text = strings.UninstallText;
            }
            else
            {
                lblSafeChest.Text = strings.MissingText;
                cmdSafeChestToggle.Text = strings.InstallText;
            }

            _logger.Debug("Updated UI for selected flashdrive.");
        }
        
        #region Buttons Events
        private void cmdRecyclerToggle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_module == null)
                return;

            if (_module.IsAntiRecyclerInstalled())
                _module.UninstallAntiRecycler();
            else
                _module.InstallAntiRecycler();

            RefreshStatus();
            _logger.Info("Changed Anti-Recycler status.");
        }

        private void cmdAutorunToggle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_module == null)
                return;

            if (_module.IsAntiAutorunInstalled())
                _module.UninstallAntiAutorun();
            else
                _module.InstallAntiAutorun();

            RefreshStatus();
            _logger.Info("Changed Anti-Autorun status.");
        }

        private void cmdSafeChestToggle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_module == null)
                return;

            if (_module.IsSafeChestInstalled())
            {
                var result = MessageBox.Show(strings.SafeChestDeletionText, strings.SafeChestDeletionTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (result == DialogResult.OK)
                    _module.UninstallSafeChest();
            }
            else
            {
                _module.InstallSafeChest();
            }

            RefreshStatus();
            _logger.Info("Changed SafeChest status.");
        }
        #endregion
    }
}
