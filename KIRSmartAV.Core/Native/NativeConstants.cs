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
