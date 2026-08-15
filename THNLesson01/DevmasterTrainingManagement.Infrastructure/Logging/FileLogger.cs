using System;
using System.IO;

namespace DevmasterTrainingManagement.Infrastructure.Logging
{
    public class FileLogger
    {
        private string _filePath = "log.txt";

        public void LogInfo(string message)
        {
            string logContent = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [INFO] {message}{Environment.NewLine}";
            File.AppendAllText(_filePath, logContent);
        }
    }
}