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

using System;
using System.Runtime.InteropServices;

namespace MClamSlim.Native
{
    internal class NativeMethods
    {
        private const string MSVCRT100_NAME = @"msvcr100.dll";
        private const string LIBCLAMAV_NAME = @"libclamav.dll";

        // Microsoft CRT imports
        [DllImport(MSVCRT100_NAME, EntryPoint = "_close", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int _close(int _FileHandle);

        [DllImport(MSVCRT100_NAME, EntryPoint = "_wopen", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        internal static extern int _wopen([In()] [MarshalAs(UnmanagedType.LPWStr)] string _Filename, int _OpenFlag, int _PermissionMode);

        // ClamAV imports
        [DllImport(LIBCLAMAV_NAME, EntryPoint = "cl_engine_new", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr cl_engine_new();

        [DllImport(LIBCLAMAV_NAME, EntryPoint = "cl_engine_free", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int cl_engine_free(IntPtr engine);

        [DllImport(LIBCLAMAV_NAME, EntryPoint = "cl_engine_compile", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int cl_engine_compile(IntPtr engine);

        [DllImport(LIBCLAMAV_NAME, EntryPoint = "cl_load", CallingConvention = CallingConvention.Cdecl, BestFitMapping = false, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
        internal static extern int cl_load([In()] IntPtr path, IntPtr engine, ref uint signo, uint dboptions);

        [DllImport(LIBCLAMAV_NAME, EntryPoint = "cl_scandesc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int cl_scandesc(int desc, ref IntPtr virname, ref int scanned, IntPtr engine, uint scanoptions);

        [DllImport(LIBCLAMAV_NAME, EntryPoint = "cl_init", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int cl_init(uint initoptions);

        [DllImport(LIBCLAMAV_NAME, EntryPoint = "cl_strerror", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr cl_strerror(int clerror);

        [DllImport(LIBCLAMAV_NAME, EntryPoint = "cl_countsigs", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int cl_countsigs([In()] [MarshalAs(UnmanagedType.LPStr)] string path, uint countoptions, ref uint sigs);
    }
}
