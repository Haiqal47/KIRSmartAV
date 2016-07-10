/*   
  NativeMethods.cs (KIRSmartAV)
  ====================================
  This file is a part of Fahmi's work which it's copyright(s)
  belongs to Fahmi Noor Fiqri as the owner of this code. Any 
  reproduction, distribution, manipulation, or other actions that 
  is not explicitly permitted by author is prohibited.
  
  { Feel free to ask to the author to get rights to edit this file
    visit this project at GitHub! https://github.com/fahminlb33 }
  
  Copyright (C) Fahmi Noor Fiqri 2016. All Rights Reserved.
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

        public static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOWME");
        public static readonly int WM_QUICKFIXNOTIFY = RegisterWindowMessage("WM_QUICKFIXNOTIFY");


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
