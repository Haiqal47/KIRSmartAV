using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace KIRSmartAV.Core
{
    [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
    public class KCureModule
    {
        string _letter = "";
        private const string RECYCLER_CONTENT = "===KIRSmartAV Anti-Recycler Module===";
        private const string SAFECHEST_NAME = "Δ SafeChest Δ";

        public KCureModule(string letter)
        {
            _letter = letter;
        }

        #region Anti-Recycler
        public void InstallAntiRecycler()
        {
            File.AppendAllText(Path.Combine(_letter, "RECYCLER"), RECYCLER_CONTENT);
            File.AppendAllText(Path.Combine(_letter, "RECYCLER_DETEC"), RECYCLER_CONTENT);

            File.SetAttributes(Path.Combine(_letter, "RECYCLER"), FileAttributes.Hidden | FileAttributes.System);
            File.SetAttributes(Path.Combine(_letter, "RECYCLER_DETEC"), FileAttributes.Hidden | FileAttributes.System);
        }

        public void UninstallAntiRecycler()
        {
            File.SetAttributes(Path.Combine(_letter, "RECYCLER"), FileAttributes.Normal);
            File.SetAttributes(Path.Combine(_letter, "RECYCLER_DETEC"), FileAttributes.Normal);

            File.Delete(Path.Combine(_letter, "RECYCLER"));
            File.Delete(Path.Combine(_letter, "RECYCLER_DETEC"));
        }

        public bool IsAntiRecyclerInstalled()
        {
            return File.Exists(Path.Combine(_letter, "RECYCLER")) && File.Exists(Path.Combine(_letter, "RECYCLER_DETEC"));
        }
        #endregion

        #region Anti-Autorun
        public void InstallAntiAutorun()
        {
            Helpers.RunAndWait(@"/c mkdir\\.\" + Helpers.TrimDriveName(_letter) + @"\autorun.inf\con\nul");
            File.SetAttributes(Path.Combine(_letter, "autorun.inf"), FileAttributes.Hidden);
        }

        public void UninstallAntiAutorun()
        {
            try
            {
                File.SetAttributes(Path.Combine(_letter, "autorun.inf"), FileAttributes.Normal);
                File.Delete(Path.Combine(_letter, "autorun.inf"));
            }
            catch { }
            Helpers.RunAndWait(@"/c rd\\.\" + Helpers.TrimDriveName(_letter) + @"\autorun.inf\con\nul");
            Helpers.RunAndWait(@"/c rd\\.\" + Helpers.TrimDriveName(_letter) + @"\autorun.inf\con");
            Helpers.RunAndWait(@"/c rd\\.\" + Helpers.TrimDriveName(_letter) + @"\autorun.inf");
        }

        public bool IsAntiAutorunInstalled()
        {
            return Directory.Exists(Path.Combine(_letter, "autorun.inf"));
        }
        #endregion

        #region SafeChest
        public void InstallSafeChest()
        {
            Directory.CreateDirectory(Path.Combine(_letter, SAFECHEST_NAME));
        }

        public void UninstallSafeChest()
        {
            Directory.Delete(Path.Combine(_letter, SAFECHEST_NAME), true);
        }

        public bool IsSafeChestInstalled()
        {
            return Directory.Exists(Path.Combine(_letter, SAFECHEST_NAME));
        }
        #endregion

    }
}
