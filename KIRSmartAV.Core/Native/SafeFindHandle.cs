using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

namespace KIRSmartAV.Core.Native
{
    [SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
    public class SafeFindHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal SafeFindHandle() : base(true)
        {
        }

        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
        protected override bool ReleaseHandle()
        {
            return NativeMethods.FindClose(base.handle);
        }
    }
}
