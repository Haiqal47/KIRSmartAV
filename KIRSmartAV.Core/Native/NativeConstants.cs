/*   
      NativeConstants.cs (KIRSmartAV.Core)
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
