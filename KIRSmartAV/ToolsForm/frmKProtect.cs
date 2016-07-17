/*   
      frmKProtect.cs (KIRSmartAV)
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
    public partial class frmKProtect : Form
    {
        private KProtectModule _module = new KProtectModule();
        private static LogManager _logger = LogManager.GetClassLogger();

        public frmKProtect()
        {
            InitializeComponent();
        }

        #region UI Presenter
        private string GetHotfixFile()
        {
            var fullpath = "updates\\";
            if (WindowsOS.IsVista())
            {
                if (WindowsOS.Is64BitOS())
                    fullpath += "vista_x64.msu";
                else
                    fullpath += "vista_x86.msu";
            }
            else
            {
                if (WindowsOS.Is64BitOS())
                    fullpath += "xp_x64.exe";
                else
                    fullpath += "xp_x86.exe";
            }

            return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fullpath);
        }

        private void CheckStatus()
        {
            //Check autorun
            if (_module.IsAutorunEnabled())
            {
                lblAutorun.Text = strings.DeactiveText;
                cmdAutorunToggle.Text = strings.ActivateText;
                _logger.Info("KProtect status: Autorun-Enabled.");
            }
            else
            {
                lblAutorun.Text = strings.ActiveText;
                cmdAutorunToggle.Text = strings.DeactivateText;
                _logger.Info("KProtect status: Autorun-Disabled.");
            }

            //Check ini
            if (_module.IsIniEnabled())
            {
                lbINI.Text = strings.ActiveText;
                cmdIniToggle.Text = strings.DeactivateText;
                _logger.Info("KProtect status: IniMapping-Enabled.");
            }
            else
            {
                lbINI.Text = strings.DeactiveText;
                cmdIniToggle.Text = strings.ActivateText;
                _logger.Info("KProtect status: IniMapping-Disabled.");
            }

            //Check regedit
            if (_module.IsRegeditEnabled())
            {
                lblRegedit.Text = strings.DeactiveText;
                cmdRegeditToggle.Text = strings.ActivateText;
                _logger.Info("KProtect status: Regedit-Enabled.");
            }
            else
            {
                lblRegedit.Text = strings.ActiveText;
                cmdRegeditToggle.Text = strings.DeactivateText;
                _logger.Info("KProtect status: Regedit-Enabled.");
            }

            //Cek pembaruan
            cmdApplyHotfix.Enabled = (WindowsOS.IsVista() || WindowsOS.IsXpOS());
            _logger.Info("KProtect status: IsVista-" + WindowsOS.IsVista().ToString() + " IsXP-" + WindowsOS.IsXpOS().ToString());
        }
        #endregion

        private void cmdAutorunToggle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_module.IsAutorunEnabled())
            {
                _module.DisableAutorun();
            }
            else
            {
                _module.EnableAutorun();
            }

            CheckStatus();
            _logger.Info("Autorun status changed.");
        }

        private void cmdIniToggle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_module.IsIniEnabled())
            {
                _module.DisableIni();
            }
            else
            {
                _module.EnableIni();
            }

            CheckStatus();
            _logger.Info("Initialization File Mapping changed.");
        }

        private void cmdRegeditToggle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_module.IsRegeditEnabled())
            {
                _module.DisableRegedit();
            }
            else
            {
                _module.EnableRegedit();
            }

            CheckStatus();
            _logger.Info("Regedit status changed.");
        }

        private void cmdApplyHotfix_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(GetHotfixFile());

            CheckStatus();
            _logger.Info("Hotfix applied.");
        }

        private void frmKProtect_Load(object sender, EventArgs e)
        {
            CheckStatus();
        }
    }
}
