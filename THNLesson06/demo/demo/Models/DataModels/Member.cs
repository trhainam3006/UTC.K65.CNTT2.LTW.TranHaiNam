namespace demo.Models.DataModels
{
    public class Member
    {
        public string MemberId { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; } 
    }
}
