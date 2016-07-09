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
                    encodedStream.FlushFinalBlock();
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
                //if (fsOutput != null)
                //{
                //    fsOutput.Dispose();
                //}
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
                    fsOutput.Flush();
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
                //if (fsInput != null)
                //{
                //    fsInput.Dispose();
                //}
                if (fsOutput != null)
                {
                    fsOutput.Dispose();
                }
            }
        }

        public void RequestStop()
        {
            if (_bwDecrypt.IsBusy)
                _bwDecrypt.CancelAsync();

            if (_bwEncrypt.IsBusy)
                _bwEncrypt.CancelAsync();
        }

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
                    encodedStream.FlushFinalBlock();
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
                //if (fsOutput != null)
                //{
                //    fsOutput.Dispose();
                //}
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
                    fsOutput.Flush();
                } 
            }
            finally
            {
                // dispose all objects
                if (encodedStream != null)
                {
                    encodedStream.Dispose();
                }
                //if (fsInput != null)
                //{
                //    fsInput.Dispose();
                //}
                if (fsOutput != null)
                {
                    fsOutput.Dispose();
                }
            }
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
