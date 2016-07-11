/*   
      Program.cs (CodeTesterConsole)
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
using KIRSmartAV.Core;

namespace CodeTesterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var encr = new FileEncoder();
            encr.ProgressChanged += Encr_ProgressChanged;

            string inputfile = "D:\\bin\\paa.vor";
            string outputfile = "D:\\bin\\asasas.exe";

            encr.DecryptFile(inputfile, outputfile);
            Console.WriteLine("Encrypt 1 completed.");

           // encr.EncryptFileAsync(inputfile, outputfile);

            Console.WriteLine("Encrypt 2 completed.");
            Console.ReadKey();
        }

        private static void Encr_ProgressChanged(object sender, FileEncoder.EncodeProgressChanged e)
        {
            Console.WriteLine(e.ProgressPercentage);
        }
    }
}
