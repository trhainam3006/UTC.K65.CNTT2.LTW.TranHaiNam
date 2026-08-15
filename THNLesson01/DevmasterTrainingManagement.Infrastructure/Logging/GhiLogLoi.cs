using System;
using System.IO;

namespace DevmasterTrainingManagement.Infrastructure.Logging
{
    public class GhiLogLoi
    {
        private static string path = "log.txt";

      
        public static void GhiLoi(string thongBao, Exception ex)
        {
            string noiDung = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] LỖI: {thongBao} - Chi tiết: {ex.Message}{Environment.NewLine}";
            File.AppendAllText(path, noiDung);
        }
    }
}