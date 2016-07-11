/*   
      NativeClasses.cs (KIRSmartAV.Core)
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
using System.Runtime.InteropServices;
using System.Text;

namespace KIRSmartAV.Core.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SHQUERYRBINFO
    {
        public int cbSize;
        public long i64Size;
        public long i64NumItems;
    }

    [Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto), BestFitMapping(false)]
    public class WIN32_FIND_DATA
    {
        public FileAttributes dwFileAttributes;
        public uint ftCreationTime_dwLowDateTime;
        public uint ftCreationTime_dwHighDateTime;
        public uint ftLastAccessTime_dwLowDateTime;
        public uint ftLastAccessTime_dwHighDateTime;
        public uint ftLastWriteTime_dwLowDateTime;
        public uint ftLastWriteTime_dwHighDateTime;
        public uint nFileSizeHigh;
        public uint nFileSizeLow;
        public int dwReserved0;
        public int dwReserved1;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string cFileName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
        public string cAlternateFileName;

        public override string ToString()
        {
            return "File name=" + cFileName;
        }
    }

    public enum TOKEN_INFORMATION_CLASS
    {
        TokenUser = 1,
        TokenGroups,
        TokenPrivileges,
        TokenOwner,
        TokenPrimaryGroup,
            TokenDefaultDacl,
            TokenSource,
            TokenType,
            TokenImpersonationLevel,
            TokenStatistics,
            TokenRestrictedSids,
            TokenSessionId,
            TokenGroupsAndPrivileges,
            TokenSessionReference,
            TokenSandBoxInert,
            TokenAuditPolicy,
            TokenOrigin,
            TokenElevationType,
            TokenLinkedToken,
            TokenElevation,
            TokenHasRestrictions,
            TokenAccessInformation,
            TokenVirtualizationAllowed,
            TokenVirtualizationEnabled,
            TokenIntegrityLevel,
            TokenUIAccess,
            TokenMandatoryPolicy,
            TokenLogonSid,
            MaxTokenInfoClass
    }

    public enum TOKEN_ELEVATION_TYPE
    {
        TokenElevationTypeDefault = 1,
        TokenElevationTypeFull,
        TokenElevationTypeLimited
    }
}
