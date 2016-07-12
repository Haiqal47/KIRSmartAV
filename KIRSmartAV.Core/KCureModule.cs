/*   
      KCureModule.cs (KIRSmartAV.Core)
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
            try
            {
                File.SetAttributes(Path.Combine(_letter, "autorun.inf"), FileAttributes.Hidden | FileAttributes.System);
            }
            catch { }
        }

        public void UninstallAntiAutorun()
        {
            try
            {
                File.SetAttributes(Path.Combine(_letter, "autorun.inf"), FileAttributes.Normal);
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
