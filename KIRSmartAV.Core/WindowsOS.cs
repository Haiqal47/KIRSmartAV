/*   
  WindowsOS.cs (KIRSmartAV.Core)
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
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;

namespace KIRSmartAV.Core
{
   public static class WindowsOS
    {
        #region Windows UAC
        private const string uacRegistryKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
        private const string uacRegistryValue = "EnableLUA";

        public static bool IsUacEnabled()
        {
            try
            {
                RegistryKey uacKey = Registry.LocalMachine.OpenSubKey(uacRegistryKey, false);
                return uacKey.GetValue(uacRegistryValue).Equals(1);
            }
            catch
            {
                return false;
            }
        }

        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
        public static bool IsProcessElevated()
        {
            if (IsUacEnabled())
            {
                IntPtr tokenHandle;
                if (!NativeMethods.OpenProcessToken(Process.GetCurrentProcess().Handle, NativeConstants.TOKEN_READ, out tokenHandle))
                {
                    throw new Exception("Could not get process token. Win32 Error Code: " + Marshal.GetLastWin32Error());
                }

                TOKEN_ELEVATION_TYPE elevationResult = TOKEN_ELEVATION_TYPE.TokenElevationTypeDefault;

                int elevationResultSize = Marshal.SizeOf((int)elevationResult);
                uint returnedSize = 0;
                IntPtr elevationTypePtr = Marshal.AllocHGlobal(elevationResultSize);

                bool success = NativeMethods.GetTokenInformation(tokenHandle, TOKEN_INFORMATION_CLASS.TokenElevationType, elevationTypePtr, (uint)elevationResultSize, out returnedSize);
                if (success)
                {
                    elevationResult = (TOKEN_ELEVATION_TYPE)Marshal.ReadInt32(elevationTypePtr);
                    bool isProcessAdmin = (elevationResult == TOKEN_ELEVATION_TYPE.TokenElevationTypeFull);
                    return isProcessAdmin;
                }
                else
                {
                    throw new Exception("Unable to determine the current elevation.");
                }
            }
            else
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                bool result = principal.IsInRole(WindowsBuiltInRole.Administrator);
                return result;
            }
        }
        #endregion

        #region Platform Query
        public static bool IsVista()
        {
            var osVersion = Environment.OSVersion.Version;
            return (osVersion.Major == 6 && osVersion.Minor == 0);
        }

        public static  bool IsXpOS()
        {
            var osVersion = Environment.OSVersion.Version;
            return (osVersion.Major == 5 && osVersion.Minor >= 1);
        }

        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
        public static  bool Is64BitOS()
        {
            var osVersion = Environment.OSVersion.Version;
            if ((osVersion.Major == 5 && osVersion.Minor >= 1) || osVersion.Major >= 6)
            {
                using (Process p = Process.GetCurrentProcess())
                {
                    bool retVal;
                    if (!NativeMethods.IsWow64Process(p.Handle, out retVal))
                    {
                        return false;
                    }
                    return retVal;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Recycle Bin
        [Obsolete]
        public static void QueryRecycleBin(out long totalSize, out long itemsCount)
        {
            var queryData = new SHQUERYRBINFO();
            queryData.cbSize = Marshal.SizeOf(queryData);
            
            int result = NativeMethods.SHQueryRecycleBin(null, ref queryData);

            if (result != 0)
                throw new Win32Exception(result);

            totalSize = queryData.i64Size;
            itemsCount = queryData.i64NumItems;
        }

        [Obsolete]
        public static void EmptyRecycleBin()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
