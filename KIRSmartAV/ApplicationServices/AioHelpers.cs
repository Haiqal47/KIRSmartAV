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
            if (Properties.Settings.Default.ChestPath == "[UNSET]")
            {
                return DefaultChestDirectory;
            }
            else
            {
                return Properties.Settings.Default.ChestPath;
            }
        }

        public static string GenerateChestFilename(string sourceFilePath, string outputPath)
        {
            var extension = Path.GetExtension(sourceFilePath);
            var fname = Path.GetFileNameWithoutExtension(sourceFilePath);
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
