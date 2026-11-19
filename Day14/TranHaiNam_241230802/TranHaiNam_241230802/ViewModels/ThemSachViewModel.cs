using System.ComponentModel.DataAnnotations;

namespace TranHaiNam_241230802.ViewModels;

public class ThemSachViewModel
{
    [Display(Name = "Thể loại")]
    [Required(ErrorMessage = "Vui lòng chọn thể loại.")]
    public int? MaTL { get; set; }

    [Display(Name = "Tên sách")]
    [Required(ErrorMessage = "Tên sách không được để trống.")]
    [StringLength(100, ErrorMessage = "Tên sách tối đa 100 ký tự.")]
    public string? TenSach { get; set; }

    [Display(Name = "Giá (nghìn đồng)")]
    [Required(ErrorMessage = "Vui lòng nhập giá.")]
    [Range(50, 1000, ErrorMessage = "Giá phải từ 50 đến 1000.")]
    public int? Gia { get; set; }

    [Display(Name = "Tên file ảnh")]
    [Required(ErrorMessage = "Vui lòng nhập tên file ảnh.")]
    [StringLength(100, ErrorMessage = "Tên file ảnh tối đa 100 ký tự.")]
    [RegularExpression(@"^[^\\/]+\.(jpg|jpeg|png|webp)$",
        ErrorMessage = "Ảnh phải có đuôi .jpg, .jpeg, .png hoặc .webp (chữ thường).")]
    public string? Anh { get; set; }
}
