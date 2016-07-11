/*   
      FastIO.cs (KIRSmartAV.Core)
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
using System.Runtime.InteropServices;
using System.Text;

namespace KIRSmartAV.Core
{
    public class FastIO
    {
        internal const string UNICODE_PREFIX = @"\\?\";
        public const string BlankSpaceCharacter = @" ";

        public static bool SetFileAttribute(string path, FileAttributes attrib, bool ignoreException = true)
        {
            if (!NativeMethods.SetFileAttributes(UNICODE_PREFIX + path, attrib))
            {
                return true;
            }
            else
            {
                if (!ignoreException)
                    throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());

                return false;
            }
        }

        public static bool MoveFile(string file1, string file2, bool ignoreException = true)
        {
            if (NativeMethods.MoveFile(file1, file2))
            {
                return true;
            }
            else
            {
                if (!ignoreException)
                    throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());

                return false;
            }
        }

        public static IEnumerable<string> EnumerateDirectories(string path, SearchOption option)
        {
            if (path == null)
                throw new ArgumentNullException("path");
            if ((option != SearchOption.AllDirectories) && (option != SearchOption.TopDirectoryOnly))
                throw new ArgumentOutOfRangeException("option");

            string fullpath = Path.GetFullPath(path);
            var win_find_data = new WIN32_FIND_DATA();
            win_find_data.dwFileAttributes = FileAttributes.Directory;

            SafeFindHandle hndFindFile;
            var qDirectories = new Queue<string>();
            qDirectories.Enqueue(Path.GetFullPath(path));

            while (qDirectories.Count > 0)
            {
                string currentPath = qDirectories.Dequeue();
                string currentSearch = Path.Combine(currentPath, "*");
                hndFindFile = NativeMethods.FindFirstFile(currentSearch, win_find_data);

                if (!hndFindFile.IsInvalid)
                {
                    do
                    {
                        if ((win_find_data.dwFileAttributes & FileAttributes.Directory) == FileAttributes.Directory)
                        {
                            if ("." != win_find_data.cFileName && ".." != win_find_data.cFileName)
                            {
                                var curPath = Path.Combine(currentPath, win_find_data.cFileName);
                                if (option == SearchOption.AllDirectories)
                                    qDirectories.Enqueue(curPath);
                                yield return curPath;
                            }
                        }
                    } while (NativeMethods.FindNextFile(hndFindFile, win_find_data));
                }
            }
        }

        public static IEnumerable<FileData> EnumerateFiles(string path, SearchOption option)
        {
            if (path == null)
                throw new ArgumentNullException("path");
            if ((option != SearchOption.AllDirectories) && (option != SearchOption.TopDirectoryOnly))
                throw new ArgumentOutOfRangeException("option");

            string fullpath = Path.GetFullPath(path);
            var win_find_data = new WIN32_FIND_DATA();

            SafeFindHandle hndFindFile;
            var qDirectories = new Queue<string>();
            qDirectories.Enqueue(Path.GetFullPath(path));

            while (qDirectories.Count > 0)
            {
                string currentPath = qDirectories.Dequeue();
                string currentSearch = Path.Combine(currentPath, "*");
                hndFindFile = NativeMethods.FindFirstFile(currentSearch, win_find_data);

                if (!hndFindFile.IsInvalid)
                {
                    do
                    {
                        if ((win_find_data.dwFileAttributes & FileAttributes.Directory) == FileAttributes.Directory)
                        {
                            if ("." != win_find_data.cFileName && ".." != win_find_data.cFileName)
                            {
                                var curPath = Path.Combine(currentPath, win_find_data.cFileName);
                                if (option == SearchOption.AllDirectories)
                                    qDirectories.Enqueue(curPath);
                            }
                        }
                        else
                        {
                            var curPath = Path.Combine(currentPath, win_find_data.cFileName);
                            yield return new FileData(currentPath, win_find_data);
                        }
                    } while (NativeMethods.FindNextFile(hndFindFile, win_find_data));
                }
            }
        }
    }
}
