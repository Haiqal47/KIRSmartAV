/*   
      Program.cs (KIRSmartAV)
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
using KIRSmartAV.ApplicationServices.MsgFilters;
using KIRSmartAV.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace KIRSmartAV
{
    static class Program
    {
        private struct AppStartupInfo
        {
            public bool IsEnglishCulture { get; set; }
            public bool IsStartup { get; set; }
            public bool HasErrors { get; set; }
        }

        private static Mutex _mutex = new Mutex(true, "KIRSmartAVSingleInstance");
        private static KcavContext _appContext = null;
        private static MessagePumpManager _msgPump = null;

        private static LogManager _logger = LogManager.GetClassLogger();
        private static Settings _settings = Settings.Default;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            _logger.Debug("Application started.");
            if (_mutex.WaitOne(TimeSpan.FromMilliseconds(500), true))
            {
                // enable visual styles
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // configure logger class
                LogManager.ConfigureLogger();
                
                // parse command line
                AppStartupInfo startupInfo = ParseCommandLine(args);

                // if there is an invalid switch
                if (!startupInfo.HasErrors)
                {
                    // set culture info
                    Thread.CurrentThread.CurrentUICulture = startupInfo.IsEnglishCulture ? new CultureInfo("en-US") : new CultureInfo("id-ID");

                    // prepare message pump
                    _msgPump = new MessagePumpManager();
                    _msgPump.AddMsgFilter(new ShowWindowMsgFilter());
                    _msgPump.AddMsgFilter(new NotifyBaloonMsgFilter());

                    _msgPump.AddWndProcFilter(new QuickFixMsgFilter());
                    _msgPump.StartWndProcHandler();

                    // prepare application context 
                    _appContext = new KcavContext();
                    _appContext.InitTrayIcon();

                    // is startup?
                    if (!startupInfo.IsStartup)
                    { 
                        _appContext.ShowMainForm();
                    }

                    // start message loop
                    Application.Run(_appContext);
                }
                else
                {
                    _logger.Warning("Invalid command-line switch entered. Exiting...");
                    MessageBox.Show("Command-line contains invalid switch. Exiting...", "Command-line error.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                // release Mutex and terminate
                _mutex.ReleaseMutex();
                _logger.Debug("Mutex released.");
            }
            else
            {
                // post message to Broadcast handle
                NativeMethods.PostMessage((IntPtr)NativeMethods.HWND_BROADCAST, NativeMethods.KCAV_SHOWME, IntPtr.Zero, IntPtr.Zero);
                _logger.Info("Posting WM_SHOWME broadcast message to all window handle.");
            }

            _logger.Debug("Application terminated.");
        }

        private static AppStartupInfo ParseCommandLine(string[] cmdArgs)
        {
            // init defaults
            var info = new AppStartupInfo()
            {
                HasErrors = false,
                IsEnglishCulture = true,
                IsStartup = false,
            };

            // check culture info
            info.IsEnglishCulture = (_settings.UILanguage == "Indonesia" ? false : true);

            // command-line switches
            foreach (string argument in cmdArgs)
            {
                var argNomalized = argument.ToLowerInvariant();
                if (argNomalized == "/startup")
                {
                    // startup
                    info.IsStartup = true;
                    _logger.Info("Startup mode initiated.");
                }
                else if (argNomalized == "/lang-id")
                {
                    // language ID
                    info.IsEnglishCulture = false;
                    _logger.Info("Override culture info to Indonesia.");
                }
                else if (argNomalized == "/lang-en")
                {
                    // language EN
                    info.IsEnglishCulture = true;
                    _logger.Info("Override culture info to English US.");
                }
                else
                {
                    // invalid switch
                    info.IsEnglishCulture = true;
                }
            }

            return info;
        }
    }
}