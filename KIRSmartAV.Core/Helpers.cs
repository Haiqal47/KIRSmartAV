/*   
  Helpers.cs (KIRSmartAV.Core)
  ====================================
  This file is a part of Fahmi's work which it's copyright(s)
  belongs to Fahmi Noor Fiqri as the owner of this code. Any 
  reproduction, distribution, manipulation, or other actions that 
  is not explicitly permitted by author is prohibited.
  
  { Feel free to ask to the author to get rights to edit this file
    visit this project at GitHub! https://github.com/fahminlb33 }
  
  Copyright (C) Fahmi Noor Fiqri 2016. All Rights Reserved.
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
