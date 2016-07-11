/*   
      FileEntry.cs (MClamSlim)
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

using MClamSlim.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MClamSlim
{
    /// <summary>
    /// File entry to be scanned.
    /// </summary>
    public class FileEntry : IDisposable
    {
        private int _fileDesc = -1;
        private string _filePath = "";

        #region Initialization
        /// <summary>
        /// Initialize new instance of <see cref="FileEntry"/> from <paramref name="filePath"/>.
        /// </summary>
        /// <param name="filePath">Fullpath to file to open.</param>
        public FileEntry(string filePath)
        {  
            _filePath = filePath;
        }

        /// <summary>
        /// Initialize new instance of <see cref="FileEntry"/> from <paramref name="filePath"/>.
        /// </summary>
        /// <param name="filePath">Fullpath to file to open.</param>
        /// <returns></returns>
        public static FileEntry OpenFile(string filePath)
        {
            return new FileEntry(filePath);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Indicates if this file descriptor is valid for use.
        /// </summary>
        public bool IsValid { get { return _fileDesc != -1; } }
        
        /// <summary>
        /// Fullpath to file on disk.
        /// </summary>
        public string FullPath { get { return _filePath; } }
        
        /// <summary>
        /// File descriptor to file on disk.
        /// </summary>
        public int FileDescriptor { get { Open(); return _fileDesc; } }
        #endregion

        #region Methods
        /// <summary>
        /// Open the file.
        /// </summary>
        protected virtual void Open()
        {
            if (_fileDesc == -1)
                _fileDesc = NativeMethods._wopen(_filePath, NativeConstants._O_RDONLY, NativeConstants._S_IREAD);

            if (_fileDesc == -1)
                throw new Win32Exception();
        }

        /// <summary>
        /// Closes the file.
        /// </summary>
        public void Close()
        {
            if (_fileDesc != -1)
                NativeMethods._close(_fileDesc);
            _fileDesc = -1;
        }
        #endregion

        #region IDisposable Implementation
        /// <summary>
        /// Dispose file entry so this class can't be used anymore.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~FileEntry()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose file entry so this class can't be used anymore.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing) { }
            Close();
        }
        #endregion
    }
}
