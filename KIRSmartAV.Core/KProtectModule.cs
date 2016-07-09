using KIRSmartAV.Core.Native;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Permissions;
using System.Text;

namespace KIRSmartAV.Core
{
    public class KProtectModule
    {
        private const string REGISTRY_AUTORUN_PATH = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer";
        private const string REGISTRY_INI_PATH = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\IniFileMapping\\Autorun.inf";
        private const string REGISTRY_EDITOR_PATH = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";

        #region Autorun Module
        public void EnableAutorun()
        {
            try
            {
                using (var kunci = Registry.CurrentUser.CreateSubKey(REGISTRY_AUTORUN_PATH, RegistryKeyPermissionCheck.ReadWriteSubTree))
                    kunci.SetValue("NoDriveTypeAutoRun", 145);

                Registry.LocalMachine.DeleteSubKeyTree(REGISTRY_INI_PATH);
            }
            catch { }
        }

        public void DisableAutorun()
        {
            try
            {
                using (var kunci = Registry.CurrentUser.CreateSubKey(REGISTRY_AUTORUN_PATH, RegistryKeyPermissionCheck.ReadWriteSubTree))
                    kunci.SetValue("NoDriveTypeAutoRun", 255);

                using (var kunci = Registry.LocalMachine.CreateSubKey(REGISTRY_INI_PATH, RegistryKeyPermissionCheck.ReadWriteSubTree))
                    kunci.SetValue("", "@SYS:DoesNotExist", RegistryValueKind.String);
            }
            catch { }
        }

        public bool IsAutorunEnabled()
        {
            try
            {
                bool enabled = false;
                using (var kunci = Registry.CurrentUser.CreateSubKey(REGISTRY_AUTORUN_PATH, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    var obj = kunci.GetValue("NoDriveTypeAutoRun");
                    enabled = (obj == null || (int)obj == 145);
                }

                using (var kunci = Registry.LocalMachine.CreateSubKey(REGISTRY_INI_PATH, RegistryKeyPermissionCheck.ReadWriteSubTree))
                    enabled &= (kunci.GetValue("") == null);

                return enabled;
            }
            catch { return true; }
        }
        #endregion

        #region Regedit Policy
        public void EnableRegedit()
        {
            try
            {
                using (var kunci = Registry.CurrentUser.CreateSubKey(REGISTRY_EDITOR_PATH, RegistryKeyPermissionCheck.ReadWriteSubTree))
                    kunci.SetValue("DisableRegistryTools", 0, RegistryValueKind.DWord);
            }
            catch { }
        }

        public void DisableRegedit()
        {
            try
            {
                using (var kunci = Registry.CurrentUser.CreateSubKey(REGISTRY_EDITOR_PATH, RegistryKeyPermissionCheck.ReadWriteSubTree))
                    kunci.SetValue("DisableRegistryTools", 1, RegistryValueKind.DWord);
            }
            catch { }
        }

        public bool IsRegeditEnabled()
        {
            try
            {
                using (var kunci = Registry.CurrentUser.CreateSubKey(REGISTRY_EDITOR_PATH, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    var result = kunci.GetValue("DisableRegistryTools");
                    return result == null || (int)result == 0;
                }
            }
            catch { return true; }
        }
        #endregion
    }
}
