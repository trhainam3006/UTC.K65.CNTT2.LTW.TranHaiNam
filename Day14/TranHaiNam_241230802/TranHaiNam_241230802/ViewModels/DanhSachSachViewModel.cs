using TranHaiNam_241230802.Models;

namespace TranHaiNam_241230802.ViewModels;

public class DanhSachSachViewModel
{
    public string TieuDe { get; set; } = "Trần Hải Nam";
    public List<Sach> DanhSach { get; set; } = new();
}
