using System.ComponentModel.DataAnnotations;

namespace _241230802_TranHaiNam_2023.Models;

public class ThemHangHoa
{
    [Display(Name = "Loại hàng")]
    [Required(ErrorMessage = "Hãy chọn loại hàng.")]
    public int? MaLoai { get; set; }

    [Display(Name = "Tên hàng")]
    [Required(ErrorMessage = "Hãy nhập tên hàng.")]
    [StringLength(50, ErrorMessage = "Tên hàng tối đa 50 ký tự.")]
    public string? TenHang { get; set; }

    [Display(Name = "Giá")]
    [Required(ErrorMessage = "Hãy nhập giá.")]
    [Range(100, 5000, ErrorMessage = "Giá phải từ 100 đến 5000.")]
    public int? Gia { get; set; }

    [Display(Name = "Ảnh")]
    [Required(ErrorMessage = "Hãy nhập tên file ảnh.")]
    [StringLength(100, ErrorMessage = "Tên file ảnh tối đa 100 ký tự.")]
    [RegularExpression(@"(?i)^[^/\\]+\.(jpg|png|gif|tiff)$",
        ErrorMessage = "Ảnh phải có đuôi .jpg, .png, .gif hoặc .tiff.")]
    public string? Anh { get; set; }
}
