using System;
using System.Collections.Generic;

namespace _241230802_TranHaiNam_2023.Models;

public partial class LoaiHang
{
    public int MaLoai { get; set; }

    public string TenLoai { get; set; } = null!;

    public virtual ICollection<HangHoa> HangHoa { get; set; } = new List<HangHoa>();
}
