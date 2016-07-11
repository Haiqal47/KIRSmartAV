/*   
      cl_cvd.cs (MClamSlim)
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
using System.Runtime.InteropServices;
using System.Text;

namespace MClamSlim.Native
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct cl_cvd
    {
        [MarshalAs(UnmanagedType.LPStr)]
        public string time;

        public uint version;

        public uint sigs;

        public uint fl;

        [MarshalAs(UnmanagedType.LPStr)]
        public string md5;

        [MarshalAs(UnmanagedType.LPStr)]
        public string dsig;

        [MarshalAs(UnmanagedType.LPStr)]
        public string builder;

        public uint stime;
    }
}
