/*   
      NativeMethods.cs (KIRSmartAV)
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

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace KIRSmartAV.ApplicationServices
{
    static class NativeMethods
    {
        public const int DBT_DEVICEARRIVAL = 0x8000;
        public const int DBT_DEVICE_TYPE_VOLUME = 0x2;
        public const int WM_DEVICECHANGE = 0x219;
        public const int HWND_BROADCAST = 0xffff;

        public static readonly int KCAV_SHOWME = RegisterWindowMessage("WM_SHOWME");
        public static readonly int KCAV_QUICKFIXNOTIFY = RegisterWindowMessage("WM_QUICKFIXNOTIFY");

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow([In()] IntPtr hWnd);

        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

        [DllImport("user32", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern int RegisterWindowMessage(string message);

        public static  string MaskToLogicalPaths(int aMask)
        {
            int lMask = aMask;
            int lValue = 0;
            StringBuilder lReturn = new StringBuilder(128);

            try
            {
                lReturn = new StringBuilder(128);
                if (aMask > 0)
                {
                    while (lMask != 0)
                    {
                        if ((lMask & 1) != 0)
                        {
                            lReturn.Append(char.ConvertFromUtf32(65 + lValue));
                            lReturn.Append(":,");
                        }
                        lValue += 1;
                        lMask >>= 1;
                    }
                    lReturn.Remove(lReturn.Length - 1, 1);
                }
                return lReturn.ToString();
            }
            finally
            {
                lReturn = null;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct BroadcastHeader
    {
        //dbch_size
        public int Size;
        //dbch_devicetype
        public int Type;
        //dbch_reserved
        private int Reserved;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct Volume
    {
        //dbcv_size
        public int Size;
        //dbcv_devicetype
        public int Type;
        //dbcv_reserved
        private int Reserved;
        //dbcv_unitmask
        public int Mask;
        //dbcv_flags
        public int Flags;
    }
}
