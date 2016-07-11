/*   
      FileData.cs (KIRSmartAV.Core)
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KIRSmartAV.Core
{
    [Serializable]
    public class FileData
    {
        public readonly FileAttributes Attributes;

        public DateTime CreationTime
        {
            get { return this.CreationTimeUtc.ToLocalTime(); }
        }

        public readonly DateTime CreationTimeUtc;

        public DateTime LastAccesTime
        {
            get { return this.LastAccessTimeUtc.ToLocalTime(); }
        }

        public readonly DateTime LastAccessTimeUtc;

        public DateTime LastWriteTime
        {
            get { return this.LastWriteTimeUtc.ToLocalTime(); }
        }

        public readonly DateTime LastWriteTimeUtc;

        public readonly long Size;

        public readonly string Name;

        public readonly string FullPath;

        public override string ToString()
        {
            return this.Name;
        }

        internal FileData(string dir, WIN32_FIND_DATA findData)
        {
            this.Attributes = findData.dwFileAttributes;
            this.CreationTimeUtc = ConvertDateTime(findData.ftCreationTime_dwHighDateTime, findData.ftCreationTime_dwLowDateTime);
            this.LastAccessTimeUtc = ConvertDateTime(findData.ftLastAccessTime_dwHighDateTime, findData.ftLastAccessTime_dwLowDateTime);
            this.LastWriteTimeUtc = ConvertDateTime(findData.ftLastWriteTime_dwHighDateTime, findData.ftLastWriteTime_dwLowDateTime);
            this.Size = CombineHighLowInts(findData.nFileSizeHigh, findData.nFileSizeLow);
            this.Name = findData.cFileName;
            this.FullPath = System.IO.Path.Combine(dir, findData.cFileName).TrimEnd(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        }

        private static long CombineHighLowInts(uint high, uint low)
        {
            return (((long)high) << 0x20) | low;
        }

        private static DateTime ConvertDateTime(uint high, uint low)
        {
            long fileTime = CombineHighLowInts(high, low);
            return DateTime.FromFileTimeUtc(fileTime);
        }
    }
}
