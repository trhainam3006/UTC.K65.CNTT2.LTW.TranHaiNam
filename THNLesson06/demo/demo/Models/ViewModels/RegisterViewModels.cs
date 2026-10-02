using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace demo.Models.ViewModels
{
    public class RegisterViewModels
    {
        public int Id { get; set; }

        [DisplayName("Tên đăng nhập")]
        [Required(ErrorMessage ="Phải điền đủ tên đăng nhập")]
        [StringLength(20,MinimumLength =3,ErrorMessage ="Độ dài từ 3->20")]
        public string userName { get; set; }

        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage ="Mật khẩu không đc để trống")]
        [DataType(DataType.Password)]
        public string userPassWord { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email không đc để trống")]
        [DataType(DataType.EmailAddress)]
        public string userEmail { get; set; }

        [DisplayName("Phone")]
        [RegularExpression(@"^0\d{9,12}$",ErrorMessage = "Số ĐT không đc để trống")]
        public string userPhone { get; set; }

        [DisplayName("BirthDay")]
        public DateTime Birthday { get; set; }
    }

}
