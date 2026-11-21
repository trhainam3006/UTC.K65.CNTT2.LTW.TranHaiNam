using System.ComponentModel.DataAnnotations;

namespace _241230802_TranHaiNam_2026_De1.ViewModels;

public class TaiSanForm
{
    [Display(Name = "Loại tài sản")]
    [Required(ErrorMessage = "Hãy chọn loại tài sản.")]
    public int? MaLoai { get; set; }

    [Display(Name = "Tên tài sản")]
    [Required(ErrorMessage = "Hãy nhập tên tài sản.")]
    [StringLength(100, ErrorMessage = "Tên tài sản tối đa 100 ký tự.")]
    public string? TenTS { get; set; }

    [Display(Name = "Nguyên giá (triệu)")]
    [Required(ErrorMessage = "Hãy nhập nguyên giá.")]
    [Range(1, 2000, ErrorMessage = "Nguyên giá phải từ 1 đến 2000 triệu đồng.")]
    public long? NguyenGia { get; set; }

    [Display(Name = "Tên file ảnh")]
    [StringLength(100, ErrorMessage = "Tên file ảnh tối đa 100 ký tự.")]
    [RegularExpression(@"(?i)^[^/\\:\r\n]+\.(jpg|jpeg|png|webp)$",
        ErrorMessage = "Ảnh phải có đuôi .jpg, .jpeg, .png hoặc .webp.")]
    public string? Anh { get; set; }
}
