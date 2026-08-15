using System;
using System.Collections.Generic;
using System.Text;

namespace DevmasterTrainingManagement.Domain.Entities
{
    /// <summary>
    /// Class : HocVien
    /// Author : Tran Hai Nam
    /// </summary>
    public class LichSuChamSoc
    {
        public string MaChamSoc { get; set; } = string.Empty;
        public string MaHocVien { get; set; } = string.Empty;
        public DateTime NgayChamSoc { get; set; } = DateTime.Now;
        public string KenhLienHe { get; set; } = string.Empty; 
        public string NoiDung { get; set; } = string.Empty;
        public string KetQua { get; set; } = string.Empty;
        public DateTime? NgayHenTiepTheo { get; set; }
    }
}
