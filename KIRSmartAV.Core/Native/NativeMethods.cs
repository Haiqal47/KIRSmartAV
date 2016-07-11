/*   
      NativeMethods.cs (KIRSmartAV.Core)
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
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace KIRSmartAV.Core.Native
{
    [SuppressUnmanagedCodeSecurity]
    internal static class NativeMethods
    {
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        internal static extern bool PathCompactPathEx([Out] StringBuilder pszOut, string szPath, int cchMax, int dwFlags);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport("kernel32.dll")]
        internal static extern bool FindClose(IntPtr handle);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern SafeFindHandle FindFirstFile([MarshalAs(UnmanagedType.LPWStr)] string fileName, [In, Out] WIN32_FIND_DATA data);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool FindNextFile(SafeFindHandle hndFindFile, [In, Out, MarshalAs(UnmanagedType.LPStruct)] WIN32_FIND_DATA lpFindFileData);

        [DllImport("kernel32.dll", EntryPoint = "SetFileAttributesW", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetFileAttributes([In()] [MarshalAs(UnmanagedType.LPWStr)] string lpFileName, [MarshalAs(UnmanagedType.U4)] FileAttributes dwFileAttributes);

        [DllImport("kernel32.dll", EntryPoint = "MoveFileW", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool MoveFile([In()] [MarshalAs(UnmanagedType.LPWStr)] string lpExistingFileName, [In()] [MarshalAs(UnmanagedType.LPWStr)] string lpNewFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWow64Process([In()] IntPtr hProcess, [Out()] out bool Wow64Process);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool OpenProcessToken(IntPtr ProcessHandle, UInt32 DesiredAccess, out IntPtr TokenHandle);

        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool GetTokenInformation(IntPtr TokenHandle, TOKEN_INFORMATION_CLASS TokenInformationClass, IntPtr TokenInformation, uint TokenInformationLength, out uint ReturnLength);

        [DllImport("shell32.dll", CharSet = CharSet.Ansi, SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int SHEmptyRecycleBin(IntPtr hwnd, [MarshalAs(UnmanagedType.LPTStr)] string pszRootPath, uint dwFlags);

        [DllImport("shell32.dll", CharSet = CharSet.Ansi, SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int SHQueryRecycleBin([MarshalAs(UnmanagedType.LPTStr)] string pszRootPath, ref SHQUERYRBINFO pSHQueryRBInfo);
    }
}
