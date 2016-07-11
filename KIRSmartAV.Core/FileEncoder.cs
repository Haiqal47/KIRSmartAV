/*   
      FileEncoder.cs (KIRSmartAV.Core)
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
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace KIRSmartAV.Core
{
    public class FileEncoder : IDisposable
    {
        private BackgroundWorker _bwEncrypt = null;
        private BackgroundWorker _bwDecrypt = null;

        public event EventHandler<EncodeProgressChanged> ProgressChanged;

        public FileEncoder()
        {
            _bwEncrypt = new BackgroundWorker();
            _bwEncrypt.DoWork += BWEncrypt_DoWork;

            _bwDecrypt = new BackgroundWorker();
            _bwDecrypt.DoWork += BWDecrypt_DoWork;
        }

        #region Async Event Handlers
        private void BWEncrypt_DoWork(object sender, DoWorkEventArgs e)
        {
            FileStream fsOutput = null;
            FileStream fsInput = null;
            CryptoStream encodedStream = null;

            try
            {
                // prepare streams
                var args = (string[])e.Argument;
                fsInput = File.OpenRead(args[0]);
                fsOutput = File.OpenWrite(args[1]);
                encodedStream = new CryptoStream(fsOutput, new ToBase64Transform(), CryptoStreamMode.Write);

                // prepare buffer
                var buffer = new byte[4096];
                int bytesRead = 0;
                long totalWritten = 0;
                long totalLength = fsInput.Length;

                // write encoded data
                while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                {
                    totalWritten += bytesRead;
                    var progress = (double)totalWritten * 100 / totalLength;
                    RaiseEventChanged(Convert.ToInt32(progress));

                    encodedStream.Write(buffer, 0, bytesRead);
                }

                if (totalLength != totalWritten)
                    RaiseEventChanged(100);
            }
            finally
            {
                // dispose all objects
                if (encodedStream != null)
                {
                    encodedStream.Dispose();
                }
                if (fsInput != null)
                {
                    fsInput.Dispose();
                }
            }
        }

        private void BWDecrypt_DoWork(object sender, DoWorkEventArgs e)
        {
            FileStream fsOutput = null;
            FileStream fsInput = null;
            CryptoStream encodedStream = null;

            try
            {
                // prepare streams
                var args = (string[])e.Argument;
                fsOutput = File.OpenWrite(args[1]);
                fsInput = File.OpenRead(args[0]);
                encodedStream = new CryptoStream(fsInput, new FromBase64Transform(), CryptoStreamMode.Read);

                // prepare buffer
                var buffer = new byte[4096];
                int bytesRead = 0;
                long totalWritten = 0;
                long totalLength = fsInput.Length;

                // write decoded data
                while ((bytesRead = encodedStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    totalWritten += bytesRead;
                    var progress = (double)totalWritten * 100 / totalLength;
                    RaiseEventChanged(Convert.ToInt32(progress));

                    fsOutput.Write(buffer, 0, bytesRead);
                }

                if (totalLength != totalWritten)
                    RaiseEventChanged(100);
            }
            finally
            {
                // dispose all objects
                if (encodedStream != null)
                {
                    encodedStream.Dispose();
                }
                if (fsOutput != null)
                {
                    fsOutput.Dispose();
                }
            }
        }
        #endregion

        #region Async Methods
        public void RequestStop()
        {
            if (_bwDecrypt.IsBusy)
                _bwDecrypt.CancelAsync();

            if (_bwEncrypt.IsBusy)
                _bwEncrypt.CancelAsync();
        }

        public void EncryptFileAsync(string inputFilePath, string outputFilepath)
        {
            if (string.IsNullOrEmpty(outputFilepath))
                throw new ArgumentNullException("outputFilepath");

            if (string.IsNullOrEmpty(inputFilePath))
                throw new ArgumentNullException("inputFilePath");

            if (_bwEncrypt.IsBusy)
                throw new InvalidOperationException("There is another encoding process on going.");

            _bwEncrypt.RunWorkerAsync(new string[] { inputFilePath, outputFilepath });
        }

        public void DecryptFileAsync(string inputFilePath, string outputFilepath)
        {
            if (string.IsNullOrEmpty(outputFilepath))
                throw new ArgumentNullException("outputFilepath");

            if (string.IsNullOrEmpty(inputFilePath))
                throw new ArgumentNullException("inputFilePath");

            if (_bwDecrypt.IsBusy)
                throw new InvalidOperationException("There is another decoding process on going.");

            _bwDecrypt.RunWorkerAsync(new string[] { inputFilePath, outputFilepath });
        }
        #endregion

        #region Sync Methods
        public void EncryptFile(string inputFilePath, string outputFilepath)
        {
            if (string.IsNullOrEmpty(outputFilepath))
                throw new ArgumentNullException("outputFilepath");

            if (string.IsNullOrEmpty(inputFilePath))
                throw new ArgumentNullException("inputFilePath");

            FileStream fsOutput = null;
            FileStream fsInput = null;
            CryptoStream encodedStream = null;

            try
            {
                // prepare streams
                fsInput = File.OpenRead(inputFilePath);
                fsOutput = File.OpenWrite(outputFilepath);
                encodedStream = new CryptoStream(fsOutput, new ToBase64Transform(), CryptoStreamMode.Write);

                // prepare buffer
                var buffer = new byte[4096];
                int bytesRead = 0;

                // write
                while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                {
                    encodedStream.Write(buffer, 0, bytesRead);
                }
            }
            finally
            {
                // dispose all objects
                if (encodedStream != null)
                {
                    encodedStream.Dispose();
                }
                if (fsInput != null)
                {
                    fsInput.Dispose();
                }
            }
        }

        public void DecryptFile(string inputFilePath, string outputFilepath)
        {
            if (string.IsNullOrEmpty(outputFilepath))
                throw new ArgumentNullException("outputFilepath");

            if (string.IsNullOrEmpty(inputFilePath))
                throw new ArgumentNullException("inputFilePath");

            FileStream fsOutput = null;
            FileStream fsInput = null;
            CryptoStream encodedStream = null;

            try
            {
                // prepare streams
                fsInput = File.OpenRead(inputFilePath);
                fsOutput = File.OpenWrite(outputFilepath);
                encodedStream = new CryptoStream(fsInput, new FromBase64Transform(), CryptoStreamMode.Read);

                // prepare buffer
                var buffer = new byte[4096];
                int bytesRead = 0;

                // write
                while ((bytesRead = encodedStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fsOutput.Write(buffer, 0, bytesRead);
                }
            }
            finally
            {
                // dispose all objects
                if (encodedStream != null)
                {
                    encodedStream.Dispose();
                }
                if (fsOutput != null)
                {
                    fsOutput.Dispose();
                }
            }
        }
        #endregion

        private void RaiseEventChanged(int progressPercentage)
        {
            if (ProgressChanged != null)
                ProgressChanged(this, new EncodeProgressChanged() { ProgressPercentage = progressPercentage, });
        }

        public class EncodeProgressChanged : EventArgs
        {
            public int ProgressPercentage { get; set; } = 0;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_bwDecrypt != null)
                    {
                        _bwDecrypt.Dispose();
                    }
                    if (_bwEncrypt != null)
                    {
                        _bwEncrypt.Dispose();
                    }                    
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~FileEncoder() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
