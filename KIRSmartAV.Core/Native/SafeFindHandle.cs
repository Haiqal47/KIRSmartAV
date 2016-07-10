/*   
  SafeFindHandle.cs (KIRSmartAV.Core)
  ====================================
  This file is a part of Fahmi's work which it's copyright(s)
  belongs to Fahmi Noor Fiqri as the owner of this code. Any 
  reproduction, distribution, manipulation, or other actions that 
  is not explicitly permitted by author is prohibited.
  
  { Feel free to ask to the author to get rights to edit this file
    visit this project at GitHub! https://github.com/fahminlb33 }
  
  Copyright (C) Fahmi Noor Fiqri 2016. All Rights Reserved.
*/
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
