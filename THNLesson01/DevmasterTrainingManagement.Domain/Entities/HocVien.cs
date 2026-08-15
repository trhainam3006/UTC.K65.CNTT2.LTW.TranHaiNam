using System;
using System.Collections.Generic;
using System.Text;

namespace DevmasterTrainingManagement.Domain.Entities
{
    /// <summary>
    /// Class : HocVien
    /// Author : Tran Hai Nam
    /// </summary>
    public class HocVien
    {
        public string MaHocVien { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public DateTime NgaySinh { get; set; }
        public string SoDienThoai { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        public DateTime NgayDangKy { get; set; } = DateTime.Now;
    }
}
