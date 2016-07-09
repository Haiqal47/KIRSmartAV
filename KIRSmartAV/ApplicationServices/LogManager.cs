using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace KIRSmartAV.ApplicationServices
{
    public class LogManager
    {
        private string _logClass = "";

        public static LogManager GetClassLogger()
        {
            var tempTrace = new StackTrace();
            var tempMethod = tempTrace.GetFrame(1).GetMethod();

            return new LogManager(tempMethod.ReflectedType.Name);
        }

        public LogManager(string className)
        {
            _logClass = "(" + className + ")";
        }

        public void Error(string message)
        {
            WriteEntry(message, "[ERROR] ");
        }

        public void Error(Exception ex)
        {
            WriteEntry("Exception occured.\r\n" + ex.ToString(), "[ERROR] ");
        }

        public void Error(string message, Exception ex)
        {
            WriteEntry(message + "\r\n" + ex.ToString(), "[ERROR] ");
        }

        public void Warning(string message)
        {
            WriteEntry(message, "[WARNING]");
        }

        public void Info(string message)
        {
            WriteEntry(message, "[INFO]  ");
        }

        public void Debug(string message)
        {
            WriteEntry(message, "[DEBUG] ");
        }

        private void WriteEntry(string message, string level)
        {
            Trace.WriteLine(string.Format(AioHelpers.LogFormatString, DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), level, _logClass, message));
        }
    }
}
