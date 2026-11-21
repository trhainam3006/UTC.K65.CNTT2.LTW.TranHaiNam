using System;
using System.Collections.Generic;

namespace _241230802_TranHaiNam_2026_De1.Models;

public partial class LoaiTaiSan
{
    public int MaLoai { get; set; }

    public string TenLoai { get; set; } = null!;

    public virtual ICollection<TaiSan> TaiSan { get; set; } = new List<TaiSan>();
}
