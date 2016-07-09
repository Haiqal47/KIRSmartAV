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

        public static readonly string[] ReservedNames = { "system volume information", "recycler", "recycler_detec", "autorun.inf" };        
        public static readonly string DatabasePath = Path.Combine(Core.Helpers.GetExecutingAssembly(), "database");

        private static readonly string DefaultChestDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), KCchestName);
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

        public static string GenerateEncryptedFilename(string origFilename)
        {
            var extension = Path.GetExtension(origFilename);
            var fname = Path.GetFileNameWithoutExtension(origFilename);
            var newFilename = "";

            for (int i = 0; i < 100; i++)
            {
                if (i == 0)
                    newFilename = origFilename + "." + VirusExtension;
                else
                    newFilename = string.Format("{0} ({1}){2}.{3}", fname, i, extension, VirusExtension);

                if (!File.Exists(newFilename)) break;
            }

            return newFilename;
        }
    }
}
