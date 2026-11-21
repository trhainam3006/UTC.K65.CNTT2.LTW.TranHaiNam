using System;
using System.Collections.Generic;

namespace _241230802_TranHaiNam_2026_De1.Models;

public partial class TaiSan
{
    public int MaTS { get; set; }

    public int MaLoai { get; set; }

    public string TenTS { get; set; } = null!;

    public decimal? NguyenGia { get; set; }

    public string? Anh { get; set; }

    public virtual LoaiTaiSan MaLoaiNavigation { get; set; } = null!;
}
