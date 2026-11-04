using System.ComponentModel.DataAnnotations;

namespace TH_Lab6.Models;

public class Learner
{
    public int LearnerID { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập họ")]
    [StringLength(100)]
    [Display(Name = "Họ")]
    public string LastName { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng nhập tên")]
    [StringLength(100)]
    [Display(Name = "Tên")]
    public string FirstMidName { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng chọn ngày nhập học")]
    [DataType(DataType.Date)]
    [Display(Name = "Ngày nhập học")]
    public DateTime EnrollmentDate { get; set; } = DateTime.Today;

    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn ngành học")]
    [Display(Name = "Ngành học")]
    public int MajorID { get; set; }

    public virtual Major? Major { get; set; }
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
}
