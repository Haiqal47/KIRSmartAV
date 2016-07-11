/*   
      WindowsOS.cs (KIRSmartAV.Core)
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
