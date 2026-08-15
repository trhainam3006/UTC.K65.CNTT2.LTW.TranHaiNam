using System;
using System.Collections.Generic;
using System.Text;

namespace DevmasterTrainingManagement.Domain.Entities
{
    /// <summary>
    /// Class : KhoaHoc
    /// Author : Tran Hai Nam
    /// </summary>
    public class KhoaHoc
    {
        public string MaKhoaHoc { get; set; } = string.Empty;
        public string TenKhoaHoc { get; set; } = string.Empty;
        public decimal HocPhi { get; set; }
        public int ThoiLuongGiangDay { get; set; }
        public string MoTa { get; set; } = string.Empty;
        public bool TrangThaiActive { get; set; } = true;
    }
}
