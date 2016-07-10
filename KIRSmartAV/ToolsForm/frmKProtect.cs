/*   
  frmKProtect.cs (KIRSmartAV)
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
                lblAutorun.Text = strings.ActiveText;
                cmdAutorunToggle.Text = strings.DeactivateText;
                _logger.Info("KProtect status: Autorun-Enabled.");
            }
            else
            {
                lblAutorun.Text = strings.DeactiveText;
                cmdAutorunToggle.Text = strings.ActivateText;
                _logger.Info("KProtect status: Autorun-Disabled.");
            }

            //Check regedit
            if (_module.IsRegeditEnabled())
            {
                lblRegedit.Text = strings.ActiveText;
                cmdRegeditToggle.Text = strings.DeactivateText;
                _logger.Info("KProtect status: Regedit-Enabled.");
            }
            else
            {
                lblRegedit.Text = strings.DeactiveText;
                cmdRegeditToggle.Text = strings.ActivateText;
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
                _module.DisableAutorun();
            else
                _module.EnableAutorun();

            CheckStatus();
            _logger.Info("Autorun status changed.");
        }

        private void cmdRegeditToggle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_module.IsRegeditEnabled())
                _module.DisableRegedit();
            else
                _module.EnableRegedit();

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
