/*   
  Program.cs (CodeTesterConsole)
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
