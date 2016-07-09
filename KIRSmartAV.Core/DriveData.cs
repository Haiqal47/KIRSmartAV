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
