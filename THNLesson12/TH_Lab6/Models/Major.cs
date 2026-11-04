using System.ComponentModel.DataAnnotations;

namespace TH_Lab6.Models;

public class Major
{
    public int MajorID { get; set; }

    [Required]
    [StringLength(100)]
    public string MajorName { get; set; } = "";

    public virtual ICollection<Learner> Learners { get; set; } = new HashSet<Learner>();
}
