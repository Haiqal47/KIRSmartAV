using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KIRSmartAV.ApplicationServices
{
    public class KcavContext : ApplicationContext
    {
        private NotifyIcon _trayIcon = null;
        private Form _ownedForm = null;
        private static KcavContext _currentContext = null;
        private static LogManager _logger = LogManager.GetClassLogger();

        public static KcavContext CurrentContext
        {
            get { return _currentContext; }
        }
        
        public KcavContext()
        {
            // prepare singleton
            if (_currentContext == null)
                _currentContext = this;

            // initialize main form
            _ownedForm = new Forms.MainForm();

            // subscribe to ThreadExit for clean up
            this.ThreadExit += KcavContext_ThreadExit;

            _logger.Debug("ApplicationContext initialized.");
        }

        private void KcavContext_ThreadExit(object sender, EventArgs e)
        {
            // remove tray icon
            _trayIcon.Visible = false;
        }
               
        public void ShowMainForm()
        {
            _logger.Debug("Showing \"MainForm.\"");
            _ownedForm.Show();
        }

        public void BringMainForm()
        {
            NativeMethods.SetForegroundWindow(_ownedForm.Handle);
        }

        #region Tray Icon
        public void InitTrayIcon()
        {
            // create menu items
            var cmenu = new ContextMenuStrip();
            ToolStripItem curItem = null;
            cmenu.ItemClicked += TaskbarStrip_ItemClicked;

            // kirsmartav
            curItem = cmenu.Items.Add("KIRSmartAV");
            curItem.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            curItem.Image = Properties.Resources.iconlogo;
            // separator
            cmenu.Items.Add(new ToolStripSeparator());
            // visit homepage
            cmenu.Items.Add("Visit homepage", Properties.Resources.globe_arrow);
            // about button
            cmenu.Items.Add("About", Properties.Resources.information_italic);
            // separator
            cmenu.Items.Add(new ToolStripSeparator());
            // exit button
            cmenu.Items.Add("Exit", Properties.Resources.cross_octagon);

            // create notify icon
            _trayIcon = new NotifyIcon();
            _trayIcon.ContextMenuStrip = cmenu;
            _trayIcon.Icon = _ownedForm.Icon;
            _trayIcon.Visible = true;

            _trayIcon.DoubleClick += TrayIcon_DoubleClick;
        }

        public void ShowBaloonTip(string title, string text, ToolTipIcon icon)
        {
            _trayIcon.ShowBalloonTip(500, title, text, icon);
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            _logger.Debug("Showing \"MainForm.\"");
            _ownedForm.Show();
        }

        private void TaskbarStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "KIRSmartAV":
                    _logger.Debug("Showing \"MainForm.\"");
                    _ownedForm.Show();
                    break;

                case "Visit homepage":
                    _logger.Debug("Opening homepage.");
                    Process.Start("https://kirsmartcyber47.wordpress.com/");
                    break;

                case "About":
                    _logger.Debug("Showing \"AboutForm.\"");
                    using (var vForm = new Forms.AboutForm())
                        vForm.ShowDialog();
                    break;

                case "Exit":
                    _logger.Debug("Terminating MainThread.");
                    this.ExitThread();
                    break;
            }
        }
        #endregion
    }
}
