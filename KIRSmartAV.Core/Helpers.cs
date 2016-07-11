/*   
      Helpers.cs (KIRSmartAV.Core)
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

using KIRSmartAV.Core.Native;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Permissions;
using System.Text;

namespace KIRSmartAV.Core
{
    public static class Helpers
    {
        public static string GetExecutingAssembly()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        [Obsolete]
        public static string CompactPath(string path, int maxLangth)
        {
            var sb = new StringBuilder(maxLangth + 1);
            NativeMethods.PathCompactPathEx(sb, path, sb.Capacity, 0);
            return sb.ToString();
        }

        public static string TrimDriveName(string letter)
        {
            return letter.Substring(0, 2);
        }

        public static string RoundByteSize(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (byteCount == 0) return "0 " + suf[0];

            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(byteCount, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 2);
            return (Math.Sign(byteCount) * num).ToString() + " " + suf[place];
        }

        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
        public static void RunAndWait(string args)
        {
            using (var proc = new Process())
            {
                var startInfo = new ProcessStartInfo()
                {
                    Arguments = args,
                    FileName = "cmd",
                    CreateNoWindow = true,
                    ErrorDialog = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                };
                proc.StartInfo = startInfo;
                proc.Start();
                proc.WaitForExit(2000);
            }
        }
    }
}
