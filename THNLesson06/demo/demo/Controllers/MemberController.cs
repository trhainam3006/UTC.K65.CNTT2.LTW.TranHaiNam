using Microsoft.AspNetCore.Mvc;
using demo.Models.DataModels;
using demo.Models.ViewModels;

namespace demo.Controllers
{
    public class MemberController:Controller
    {
        public static readonly List<Member> members = new List<Member>();
        public IActionResult Index()
        {
            return View(members);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(RegisterViewModels register)
        {
            if (ModelState.IsValid)
            {
                Member m = new Member
                {
                    MemberId = Guid.NewGuid().ToString(),
                    UserName=register.userName,
                    PassWord=register.userPassWord,
                    Email=register.userEmail,
                    PhoneNumber = int.Parse(register.userPhone),
                    Birthday=register.Birthday,
                };
                members.Add(m);
                return RedirectToAction("Index");
            }
            else
            {
                return View(register);
            }
        }
    }
}
