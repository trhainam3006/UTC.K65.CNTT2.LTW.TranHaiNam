using System;
using System.Collections.Generic;
using System.Text;

namespace DevmasterTrainingManagement.Domain.Entities
{
    /// <summary>
    /// Class : LopHoc
    /// Author : Tran Hai Nam
    /// </summary>
    public class LopHoc
    {
        public string MaLop { get; set; } = string.Empty;
        public string TenLop { get; set; } = string.Empty;
        public string MaKhoaHoc { get; set; } = string.Empty;
        public DateTime NgayKhaiGiang { get; set; }
        public string LichHoc { get; set; } = string.Empty; 
        public int SiSoToiDa { get; set; }
        public string TrangThaiLop { get; set; } = "Sap co lop hoc";
    }
}
