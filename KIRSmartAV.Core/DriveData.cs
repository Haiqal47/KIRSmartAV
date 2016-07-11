/*   
      DriveData.cs (KIRSmartAV.Core)
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

namespace KIRSmartAV.Core
{
    public class DriveData
    {
        public static DriveData[] GetDrives()
        {
            var dlist = new List<DriveData>();
            foreach (var drv in DriveInfo.GetDrives())
            {
                if (!drv.IsReady)
                    continue;

                if (drv.DriveType == DriveType.Removable)
                    dlist.Add(new DriveData(drv.Name));
            }
            return dlist.ToArray();
        }

        public DriveData(string name)
        {
            var drv = new DriveInfo(name);
            DriveLetter = drv.Name;
            DriveVolume = drv.VolumeLabel;
            TotalSpace = drv.TotalSize;
            UsedSpace = drv.TotalSize - drv.TotalFreeSpace;
            FreeSpace = drv.TotalFreeSpace;
            DriveFormat = drv.DriveFormat;
            drv = null;
        }

        public string DriveFormat { get; set; }

        public string DriveLetter { get; set; }

        public string DriveVolume { get; set; }

        public long TotalSpace { get; internal set; }

        public long UsedSpace { get; internal set; }

        public long FreeSpace { get; internal set; }

        public override string ToString()
        {
            if (DriveVolume != null && DriveVolume.Trim().Length != 0)
                return string.Concat(DriveVolume, " (", DriveLetter, ")");
            else
                return string.Concat("Diskalepas (", DriveLetter, ")");
        }
    }
}
