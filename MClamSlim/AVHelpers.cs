//
//    MClamSlim
//    Most lightweight libclamav binding for .NET
//    ===========================================
//    Copyright(C) 2016  Fahmi Noor Fiqri
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU Lesser General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public License
//    along with this program. If not, see <http://www.gnu.org/licenses/>.
//

using MClamSlim.Native;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace MClamSlim
{
    /// <summary>
    /// Helper methods.
    /// </summary>
    public static class AVHelpers
    {
        /// <summary>
        /// Converts Libclamav error code to human-readable message.
        /// </summary>
        /// <param name="errorCode">Error code returned from Libclamav.</param>
        /// <returns>Human-readable error message.</returns>
        internal static string ErrorCodeToString(int errorCode)
        {
            IntPtr strData;
            strData = NativeMethods.cl_strerror(errorCode);
            return Marshal.PtrToStringAnsi(strData);
        }

        public static long CountSignatures(string cvdPath)
        {
            uint signatures = 0;
            var err_code = NativeMethods.cl_countsigs(cvdPath, NativeConstants.CL_COUNT_ALL, ref signatures);
            if (err_code == (int)cl_error_t.CL_SUCCESS)
            {
                return signatures;
            }
            else
            {
                throw new Exception(ErrorCodeToString(err_code));
            }
        }
    }
}
