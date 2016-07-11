/*   
      frmKCure.cs (KIRSmartAV)
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
