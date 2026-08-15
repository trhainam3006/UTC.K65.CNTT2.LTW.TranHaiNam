using System;
using System.Collections.Generic;
using System.Text;

namespace DevmasterTrainingManagement.Domain.Entities
{
    /// <summary>
    /// Class : HocVien
    /// Author : Tran Hai Nam
    /// </summary>
    public class DangKyKhoaHoc
    {
        public string MaDangKy { get; set; } = string.Empty;
        public string MaHocVien { get; set; } = string.Empty;
        public string MaLop { get; set; } = string.Empty;
        public DateTime NgayDangKy { get; set; } = DateTime.Now;
        public decimal HocPhiCanNop { get; set; }
        public decimal SoTienDaNop { get; set; }

        public decimal SoTienConThieu => HocPhiCanNop - SoTienDaNop;
        public bool TrangThaiThanhToanDu => SoTienDaNop >= HocPhiCanNop;
    }
}