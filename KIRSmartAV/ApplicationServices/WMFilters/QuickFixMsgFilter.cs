/*   
      QuickFixMsgFilter.cs (KIRSmartAV)
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

using KIRSmartAV.Core;
using KIRSmartAV.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;

namespace KIRSmartAV.ApplicationServices.MsgFilters
{    
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public class QuickFixMsgFilter : IMessageFilter, IDisposable
    {
        private static Settings _settings = Settings.Default;
        private static LogManager _logger = LogManager.GetClassLogger();

        private BackgroundWorker _worker = null;
        public QuickFixMsgFilter()
        {
            _worker = new BackgroundWorker();
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += Worker_DoWork;
        }

        public bool PreFilterMessage(ref Message m)
        {
            // check the message and parameter
            if (m.Msg == NativeMethods.WM_DEVICECHANGE && m.WParam.ToInt32() == NativeMethods.DBT_DEVICEARRIVAL)
            {
                // WM_DEVICECHANGE intercepted with DBT_DEVICEARRIVAL parameter
                BroadcastHeader bcHeader = (BroadcastHeader)Marshal.PtrToStructure(m.LParam, typeof(BroadcastHeader));
                if (bcHeader.Type == NativeMethods.DBT_DEVICE_TYPE_VOLUME)
                {
                    // marshal data to get logical path
                    Volume ldVolume = (Volume)Marshal.PtrToStructure(m.LParam, typeof(Volume));
                    var logicalPath = NativeMethods.MaskToLogicalPaths(ldVolume.Mask);

                    _logger.Info("Detected USB drive:\"" + logicalPath + "\"");

                    // check if quick fix is enabled
                    if (!_settings.IsSynchronized)
                    {
                        _settings.Reload();
                    }

                    // do quickfix?
                    if (_settings.QuickFixEnabled)
                    {
                        if (!_worker.IsBusy)
                        {
                            _worker.RunWorkerAsync(logicalPath);
                        }
                    }
                }
            }

            // return as unhandled
            return false;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            // prepare variables
            var driveRoot = e.Argument.ToString();
            var driveHidden = driveRoot + "\\" + FastIO.BlankSpaceCharacter;
            var driveRecovered = driveRoot + "\\" + Commons.KCrecoveredName;

            // check if it's is FAT32
            var driveVol = new DriveData(driveRoot);
            if (!driveVol.DriveFormat.StartsWith(Commons.FatFormat))
            {
                // this drive doesn't meet requirements.
                return;
            }

            // begin counting time
            _logger.Info("QuickFix taking action. Drive \"" + driveRoot + "\"");
            var counter = new Stopwatch();
            counter.Start();

            try
            {
                // restore root directory attribute
                _logger.Debug("QuickFix: Restoring no-name directory attribute.");
                FastIO.SetFileAttribute(driveHidden, FileAttributes.Normal, false);
            }
            catch (Exception ex)
            {
                _logger.Error("QuickFix: Can't change no-name attribute directory.", ex);
            }
   
            try
            {
                // restore root directory name
                _logger.Debug("QuickFix: Renaming no-name directory.");
                FastIO.MoveFile(driveHidden, driveRecovered, false);
            }
            catch (Exception ex)
            {
                _logger.Error("QuickFix: Can't rename no-name directory.", ex);
            }

            // restore directories
            var searchOpt = _settings.QuickFixRecrusive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (string folderPath in FastIO.EnumerateDirectories(driveRoot, searchOpt))
            {
                if (counter.ElapsedMilliseconds > 2000)
                {
                    // only run for 10 seconds
                    _logger.Debug("QuickFix: Operation timeout.");
                    break;
                }

                // normalize folders
                var normalizedFilePath = Path.GetFileName(folderPath).ToLowerInvariant();
                foreach (string reservedName in Commons.ReservedNames)
                {
                    if (normalizedFilePath == reservedName)
                    {
                        FastIO.SetFileAttribute(folderPath, FileAttributes.Hidden | FileAttributes.System);
                        _logger.Info("QuickFix restore attribute to Hidden.");
                    }
                    else
                    {
                        FastIO.SetFileAttribute(folderPath, FileAttributes.Normal);
                        _logger.Info("QuickFix restore attribute to Normal.");
                    }
                }
            }

            // stop counting and log away
            counter.Stop();
            _logger.Info("QuickFix operation finished. Drive \"" + driveRoot + "\" time elapsed: " + counter.ElapsedMilliseconds);
            NativeMethods.PostMessage((IntPtr)NativeMethods.HWND_BROADCAST, NativeMethods.KCAV_QUICKFIXNOTIFY, IntPtr.Zero, IntPtr.Zero);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        // This code added to correctly implement the disposable pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue && disposing)
            {
                if (_worker != null)
                {
                    if (!_worker.IsBusy)
                    {
                        _worker.CancelAsync();
                    }

                    Thread.Sleep(300);
                    _worker.Dispose();
                }
            }
            disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
