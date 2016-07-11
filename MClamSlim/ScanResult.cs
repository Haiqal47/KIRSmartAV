/*   
      ScanResult.cs (MClamSlim)
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

namespace MClamSlim
{
    /// <summary>
    /// Detailed scan result.
    /// </summary>
    public class ScanResult
    {
        private string _vname = "";
        private string _fpath = "";
        private int _scanned = 0;
        private bool _isVirus = false;

        internal ScanResult(string vname, string fpath, int sumscan, bool vir)
        {
            _vname = vname;
            _fpath = fpath;
            _scanned = sumscan;
            _isVirus = vir;
        }
        
        /// <summary>
        /// Virus name if the file is infected.
        /// </summary>
        public string VirusName
        {
            get { return _vname; }
        }
        
        /// <summary>
        /// Fullpath to file on disk.
        /// </summary>
        public string FullPath
        {
            get { return _fpath; }
        }

        /// <summary>
        /// Number of scanned data in total.
        /// </summary>
        public int Scanned
        {
            get { return _scanned; }
        }
        
        /// <summary>
        /// Indicates if the file is infected.
        /// </summary>
        public bool IsVirus
        {
            get { return _isVirus; }
        }
    }
}
