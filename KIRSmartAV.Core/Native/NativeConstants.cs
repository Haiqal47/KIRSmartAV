/*   
  NativeConstants.cs (KIRSmartAV.Core)
  ====================================
  This file is a part of Fahmi's work which it's copyright(s)
  belongs to Fahmi Noor Fiqri as the owner of this code. Any 
  reproduction, distribution, manipulation, or other actions that 
  is not explicitly permitted by author is prohibited.
  
  { Feel free to ask to the author to get rights to edit this file
    visit this project at GitHub! https://github.com/fahminlb33 }
  
  Copyright (C) Fahmi Noor Fiqri 2016. All Rights Reserved.
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace KIRSmartAV.Core.Native
{
    internal class NativeConstants
    {
        internal const uint STANDARD_RIGHTS_READ = 0x00020000;
        internal const uint TOKEN_QUERY = 0x0008;
        internal const uint TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);
        internal const uint WM_ERASEBKGND = 0x14;
    }
}
