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
        private static Mutex _mutex = new Mutex(true, "KIRSmartAVSingleInstance");
        private static KcavContext _appContext = null;
        private static LogManager _logger = LogManager.GetClassLogger();
        private static Settings _settings = Settings.Default;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            _logger.Debug("Application started.");
            if (_mutex.WaitOne(TimeSpan.Zero, true))
            {
                // enable visual styles
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                bool isStartup = false;
                bool hasErrors = false;
                bool langEn = true;

                // check culture info
                langEn = _settings.UILanguage == "Indonesia" ? false : true; 

                // command-line switches
                foreach (string argument in args)
                {
                    var argNomalized = argument.ToLowerInvariant();
                    if (argNomalized == "/startup")
                    {
                        // startup
                        isStartup = true;
                        _logger.Info("Startup mode initiated.");
                    }
                    else if (argNomalized == "/lang-id")
                    {
                        // language ID
                        langEn = false;
                        _logger.Info("Override culture info to Indonesia.");
                    }
                    else if (argNomalized == "/lang-en")
                    {
                        // language EN
                        langEn = true;
                        _logger.Info("Override culture info to English US.");
                    }
                    else
                    {
                        // invalid switch
                        hasErrors = true;
                    }
                }

                // if there is an invalid switch
                if (!hasErrors)
                {
                    // set culture info
                    if (langEn)
                    {
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                    }
                    else
                    { 
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("id-ID");
                    }

                    // prepare application context 
                    _appContext = new KcavContext();
                    _appContext.InitTrayIcon();

                    // is startup?
                    if (!isStartup)
                    {
                        _appContext.ShowMainForm();
                    }

                    // for checking broadcast messages
                    Application.AddMessageFilter(new BroadcastMsgFilter());

                    // start message loop
                    Application.Run(_appContext);
                }
                else
                {
                    _logger.Warning("Invalid command-line switch entered. Exiting...");
                    MessageBox.Show("Command-line contains invalid switch. Exiting...", "Command-line error.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                // release mutex and terminate
                _mutex.ReleaseMutex();
                _logger.Info("Message loop terminated and mutex was released.");
            }
            else
            {
                // post message to Broadcast handle
                NativeMethods.PostMessage((IntPtr)NativeMethods.HWND_BROADCAST, NativeMethods.WM_SHOWME, IntPtr.Zero, IntPtr.Zero);
                _logger.Info("Posting WM_SHOWME broadcast message to all window handle.");
            }
            _logger.Debug("Application terminated.");
        }
    }
}
