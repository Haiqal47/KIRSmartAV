//
//    MClamSlim
//    Most lightweight libclamav binding for .NET
//    ===========================================
//    Copyright(C) 2016  Fahmi Noor Fiqri
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU Lesser General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public License
//    along with this program. If not, see <http://www.gnu.org/licenses/>.
//

namespace MClamSlim.Native
{
    internal enum cl_error_t : int
    {
        CL_CLEAN = 0,
        CL_SUCCESS = 0,
        CL_VIRUS,
        CL_ENULLARG,
        CL_EARG,
        CL_EMALFDB,
        CL_ECVD,
        CL_EVERIFY,
        CL_EUNPACK,
        CL_EOPEN,
        CL_ECREAT,
        CL_EUNLINK,
        CL_ESTAT,
        CL_EREAD,
        CL_ESEEK,
        CL_EWRITE,
        CL_EDUP,
        CL_EACCES,
        CL_ETMPFILE,
        CL_ETMPDIR,
        CL_EMAP,
        CL_EMEM,
        CL_ETIMEOUT,
        CL_BREAK,
        CL_EMAXREC,
        CL_EMAXSIZE,
        CL_EMAXFILES,
        CL_EFORMAT,
        CL_EPARSE,
        CL_EBYTECODE,
        CL_EBYTECODE_TESTFAIL,
        CL_ELOCK,
        CL_EBUSY,
        CL_ESTATE,
        CL_ELAST_ERROR,
    }
}
