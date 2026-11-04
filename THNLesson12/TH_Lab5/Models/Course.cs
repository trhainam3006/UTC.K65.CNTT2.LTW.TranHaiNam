using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH_Lab5.Models;

public class Course
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CourseID { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = "";

    public int Credits { get; set; }
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
}
