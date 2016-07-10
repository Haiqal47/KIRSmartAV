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
