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
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace MClamSlim
{
    /// <summary>
    /// Managed Libclamav wrapper.
    /// </summary>
    [SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
    public class ClamEngine : IDisposable
    {
        private IntPtr _clamHandle;
        private bool _compiled = false;
        private uint _numOfSigs = 0;

        #region Initialization
        /// <summary>
        /// Initialize libclamav engine.
        /// </summary>
        /// <remarks>Call this method once per session.</remarks>
        public static void Initialize()
        {
            NativeMethods.cl_init(NativeConstants.CL_INIT_DEFAULT);
        }

        /// <summary>
        /// Initialize new instance of <see cref="ClamEngine"/> class.
        /// </summary>
        /// <returns>An instance to <see cref="ClamEngine"/>.</returns>
        public static ClamEngine CreateNew()
        {
            return new ClamEngine();
        }

        /// <summary>
        /// Initialize new instance of <see cref="ClamEngine"/> class.
        /// </summary>
        public ClamEngine()
        {
            _clamHandle = NativeMethods.cl_engine_new();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Number of loaded signature.
        /// </summary>
        public uint LoadedSignatures { get { return _numOfSigs; } }

        /// <summary>
        /// Indicates if the engine were loaded by database.
        /// </summary>
        public bool IsLoaded { get { return _numOfSigs > 0; } }
        
        /// <summary>
        /// Indicates if the engine were compiled.
        /// </summary>
        public bool IsCompiled { get { return _compiled; } }

        /// <summary>
        /// Indicates if the engine were ready for scanning.
        /// </summary>
        public bool IsScanReady { get { return IsLoaded & _compiled; } }
        #endregion

        #region Methods
        /// <summary>
        /// Load database file(s) to engine.
        /// </summary>
        /// <param name="file">Fullpath to databse file or folder.</param>
        /// <remarks>The <paramref name="file"/> parameter can be point to a file or directory containing database files.</remarks>
        /// <returns>Number of loaded signature.</returns>
        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
        [SecurityCritical]
        public uint LoadCvdFile(string file)
        {
            if (IsLoaded)
                if (!IsLoaded)
                    throw new InvalidOperationException("This engine is not loaded.");
            if (_compiled)
                throw new InvalidOperationException("This engine is already compiled.");

            uint ldatabase = 0;
            var dataLocation = Marshal.StringToHGlobalAnsi(file);

            var rval = NativeMethods.cl_load(dataLocation, _clamHandle, ref ldatabase, NativeConstants.CL_DB_STDOPT);
            Marshal.FreeHGlobal(dataLocation);

            if (rval == (int)cl_error_t.CL_SUCCESS)
            {
                _numOfSigs += ldatabase;
                return ldatabase;
            }
            else
            {
                throw new Exception(AVHelpers.ErrorCodeToString(rval));
            }
        }

        /// <summary>
        /// Compile the engine before use.
        /// </summary>
        /// <remarks>The engine must be <c>loaded</c>.</remarks>
        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
        public void Compile()
        {
            if (!IsLoaded)
                throw new InvalidOperationException("This engine is not loaded.");
            if (_compiled)
                throw new InvalidOperationException("This engine is already compiled.");

            var rv = NativeMethods.cl_engine_compile(_clamHandle);
            if (rv != (int)cl_error_t.CL_SUCCESS)
                throw new Exception(AVHelpers.ErrorCodeToString(rv));

            _compiled = true;
        }

        /// <summary>
        /// Scans a file from virus infection.
        /// </summary>
        /// <param name="filePath">File to be scanned.</param>
        /// <remarks>The engine must be <c>loaded</c> and <c>compiled</c>.</remarks>
        /// <returns><see cref="ScanResult"/> instance containing the scan result.</returns>
        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
        public ScanResult ScanFile(FileEntry filePath)
        {
            if (!this.IsLoaded)
                throw new InvalidOperationException("This engine is not loaded by database.");
            if (!_compiled)
                throw new InvalidOperationException("This engine is not compiled.");
            if (filePath == null)
                throw new ArgumentNullException("filePath");

            int fscaned = 0;
            IntPtr vname = IntPtr.Zero;
            var retv = NativeMethods.cl_scandesc(filePath.FileDescriptor, ref vname, ref fscaned, _clamHandle, NativeConstants.CL_SCAN_STDOPT);

            ScanResult result = null;
            if (retv == (int)cl_error_t.CL_VIRUS)
            {
                result = new ScanResult(Marshal.PtrToStringAnsi(vname), filePath.FullPath, fscaned, true);
            }
            else if (retv == (int)cl_error_t.CL_CLEAN)
            {
                result = new ScanResult("Clean", filePath.FullPath, fscaned, false);
            }
            else
            {
                throw new Exception(AVHelpers.ErrorCodeToString(retv));
            }
            return result;
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; 

        /// <summary>
        /// Frees memory used <see cref="ClamEngine"/> instance.
        /// </summary>
        /// <param name="disposing"><see langword="true"/> to dispose managed objects too.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) { }

                NativeMethods.cl_engine_free(_clamHandle);
                disposedValue = true;
            }
        }

        /// <summary>
        /// Frees memory used <see cref="ClamEngine"/> instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ClamEngine()
        {
            Dispose(false);
        }
        #endregion
    }
}
