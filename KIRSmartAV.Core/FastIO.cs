/*   
  FastIO.cs (KIRSmartAV.Core)
  ====================================
  This file is a part of Fahmi's work which it's copyright(s)
  belongs to Fahmi Noor Fiqri as the owner of this code. Any 
  reproduction, distribution, manipulation, or other actions that 
  is not explicitly permitted by author is prohibited.
  
  { Feel free to ask to the author to get rights to edit this file
    visit this project at GitHub! https://github.com/fahminlb33 }
  
  Copyright (C) Fahmi Noor Fiqri 2016. All Rights Reserved.
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
