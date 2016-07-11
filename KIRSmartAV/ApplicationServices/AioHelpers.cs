/*   
      AioHelpers.cs (KIRSmartAV)
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
using System.Text;

namespace KIRSmartAV.ApplicationServices
{
    static class AioHelpers
    {
        public const long MaximumCheckSize = 16000000000;
        public const string Fat32Format = "FAT32";

        public const string KCrecoveredName = "KCrecovered";
        public const string KCchestName = "KIRSmartChest";
        public const string TextDots = "...";
        public const string LogFormatString = "{0}  {1} {2}: {3}";

        public const string VirusExtension = "vir";
        public const string InputMultidottedMask = "*.*." + VirusExtension;
        public const string InputAnyFileMask = "*.*";

        public const string StartupRegistryPath = @"Software\Microsoft\Windows\CurrentVersion\Run";

        public static readonly string[] ReservedNames = { "system volume information", "recycler", "recycler_detec", "autorun.inf", "indexervolumeguide" };
        private static readonly string DefaultChestDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), KCchestName);

#if DEBUG
        public static readonly string DatabasePath = "D:\\database";
#else
        public static readonly string DatabasePath = Path.Combine(Core.Helpers.GetExecutingAssembly(), "database");
#endif
        
        public static string GetChestFolder()
        {
            string chestDir = "";
            if (Properties.Settings.Default.ChestPath == "[UNSET]")
            {
                chestDir = DefaultChestDirectory;
            }
            else
            {
                chestDir = Properties.Settings.Default.ChestPath;
            }

            // always create directory
            Directory.CreateDirectory(chestDir);
            return chestDir;
        }

        public static string GenerateChestFilePath(string sourceFilePath)
        {
            var extension = Path.GetExtension(sourceFilePath);
            var fname = Path.GetFileNameWithoutExtension(sourceFilePath);
            var outputPath = GetChestFolder();
            string outputFilePath = "";

            for (int i = 0; i < 100; i++)
            {
                string newFilename = "";
                if (i == 0)
                {
                    newFilename = Path.GetFileName(sourceFilePath) + "." + VirusExtension;
                }
                else
                {
                    newFilename = string.Format("{0} ({1}){2}.{3}", fname, i, extension, VirusExtension);
                }

                outputFilePath = Path.Combine(outputPath, newFilename);
                if (!File.Exists(outputFilePath)) break;
            }

            return outputFilePath;
        }
    }
}
