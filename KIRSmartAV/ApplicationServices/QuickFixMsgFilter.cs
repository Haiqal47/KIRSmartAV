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
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;

namespace KIRSmartAV.ApplicationServices
{    
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public class QuickFixMsgFilter
    {
        private static Properties.Settings _settings = Properties.Settings.Default;
        private static LogManager _logger = LogManager.GetClassLogger();

        public void FilterMessage(Message m)
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

                    if (_settings.QuickFixEnabled)
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(QuickFixCallback), logicalPath);
                    }
                }
            }
        }

        private void QuickFixCallback(object args)
        {
            // check if it's is FAT32
            var driveVol = new DriveData(args.ToString());
            if ((driveVol.DriveFormat != AioHelpers.Fat32Format))
            {
                // this drive doesn't meet requirements.
                return;
            }

            _logger.Info("QuickFix taking action. Drive \"" + args.ToString() + "\"");

            // begin counting time
            var counter = new Stopwatch();
            counter.Start();

            // prepare variables
            var driveRoot = args.ToString();
            var driveHidden = driveRoot + "\\" + FastIO.BlankSpaceCharacter;
            var driveRecovered = driveRoot + "\\" + AioHelpers.KCrecoveredName;

            // restore root directory attribute
            _logger.Info("QuickFix: Restoring attribute and no-name directory.");
            try
            {
                FastIO.SetFileAttribute(driveHidden, FileAttributes.Normal, false);
            }
            catch (Exception ex)
            {
                _logger.Error("QuickFix: Can't change no-name attribute directory.", ex);
            }

            // restore root directory name
            try
            {
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
                if (counter.ElapsedMilliseconds > 10000)
                {
                    // only run in 10 seconds
                    _logger.Debug("QuickFix: Operation timeout.");
                    break;
                }

                // normalize folders
                var normalizedFilePath = Path.GetFileName(folderPath).ToLowerInvariant();
                if (normalizedFilePath != "system volume information" || normalizedFilePath != "autorun.inf")
                {
                    FastIO.SetFileAttribute(folderPath, FileAttributes.Normal);
                    _logger.Info("QuickFix restore attribute to Normal.");
                }
                else
                {
                    FastIO.SetFileAttribute(folderPath, FileAttributes.Hidden);
                    _logger.Info("QuickFix restore attribute to Hidden.");
                }
            }

            // stop counting and log away
            counter.Stop();
            _logger.Info("QuickFix operation finished. Drive \"" + driveRoot + "\" time elapsed: " + counter.ElapsedMilliseconds);
            NativeMethods.PostMessage((IntPtr)NativeMethods.HWND_BROADCAST, NativeMethods.WM_QUICKFIXNOTIFY, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
