//
//    MClamSlim
//    Most lightweight libclamav binding for .NET
//    ===========================================
//    Copyright(C) 2016  Fahmi Noor Fiqri
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU Lesser General internal License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU Lesser General internal License for more details.
//
//    You should have received a copy of the GNU Lesser General internal License
//    along with this program. If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.Text;

namespace MClamSlim.Native
{
    internal class NativeConstants
    {
        internal const int CL_COUNT_PRECISION = 4096;
        internal const int CL_INIT_DEFAULT = 0;
        internal const int CL_COUNT_ALL = 3;

        #region MSVCRT
        internal const int _O_RDONLY = 0;
        internal const int _S_IREAD = 256;
        #endregion

        #region Scan Options Constant
        internal const int CL_SCAN_RAW = 0;
        internal const int CL_SCAN_ARCHIVE = 1;
        internal const int CL_SCAN_MAIL = 2;
        internal const int CL_SCAN_OLE2 = 4;
        internal const int CL_SCAN_BLOCKENCRYPTED = 8;
        internal const int CL_SCAN_HTML = 16;
        internal const int CL_SCAN_PE = 32;
        internal const int CL_SCAN_BLOCKBROKEN = 64;
        internal const int CL_SCAN_ALGORITHMIC = 512;
        internal const int CL_SCAN_PHISHING_BLOCKSSL = 2048;
        internal const int CL_SCAN_PHISHING_BLOCKCLOAK = 4096;
        internal const int CL_SCAN_ELF = 8192;
        internal const int CL_SCAN_PDF = 16384;
        internal const int CL_SCAN_STRUCTURED = 32768;
        internal const int CL_SCAN_STRUCTURED_SSN_NORMAL = 65536;
        internal const int CL_SCAN_STRUCTURED_SSN_STRIPPED = 131072;
        internal const int CL_SCAN_PARTIAL_MESSAGE = 262144;
        internal const int CL_SCAN_HEURISTIC_PRECEDENCE = 524288;
        internal const int CL_SCAN_BLOCKMACROS = 1048576;
        internal const int CL_SCAN_SWF = 4194304;

        internal const int CL_SCAN_STDOPT = CL_SCAN_MAIL | CL_SCAN_OLE2 | CL_SCAN_HEURISTIC_PRECEDENCE |
                                          CL_SCAN_PDF | CL_SCAN_HTML | CL_SCAN_PE | CL_SCAN_ALGORITHMIC |
                                          CL_SCAN_ELF | CL_SCAN_SWF;
        #endregion

        #region Database Options Constant
        internal const int CL_DB_PHISHING = 2;
        internal const int CL_DB_PHISHING_URLS = 8;
        internal const int CL_DB_PUA = 16;
        internal const int CL_DB_CVDNOTMP = 32;
        internal const int CL_DB_OFFICIAL = 64;
        internal const int CL_DB_PUA_MODE = 128;
        internal const int CL_DB_PUA_INCLUDE = 256;
        internal const int CL_DB_PUA_EXCLUDE = 512;
        internal const int CL_DB_COMPILED = 1024;
        internal const int CL_DB_DIRECTORY = 2048;
        internal const int CL_DB_OFFICIAL_ONLY = 4096;
        internal const int CL_DB_BYTECODE = 8192;
        internal const int CL_DB_SIGNED = 16384;
        internal const int CL_DB_BYTECODE_UNSIGNED = 32768;
        internal const int CL_DB_UNSIGNED = 65536;
        internal const int CL_DB_BYTECODE_STATS = 131072;
        internal const int CL_DB_ENHANCED = 262144;
        internal const int CL_DB_PCRE_STATS = 524288;
        internal const int CL_DB_YARA_EXCLUDE = 1048576;
        internal const int CL_DB_YARA_ONLY = 2097152;

        internal const int CL_DB_STDOPT = CL_DB_PHISHING | CL_DB_PHISHING_URLS | CL_DB_BYTECODE;
        #endregion
    }
}
