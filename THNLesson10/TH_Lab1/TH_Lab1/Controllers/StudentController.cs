using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TH_Lab1.Models;

public class StudentController : Controller
{
    // Danh sách sinh viên dùng chung (dạng static để không bị mất dữ liệu khi chuyển request)
    private static List<Student> listStudents = new List<Student>()
        {
            new Student() { Id = 101, Name = "Hải Nam", Branch = Branch.IT, Gender = Gender.Male, IsRegular = true, Address = "A1-2018", Email = "nam@g.com" },
            new Student() { Id = 102, Name = "Minh Tú", Branch = Branch.BE, Gender = Gender.Female, IsRegular = true, Address = "A1-2019", Email = "tu@g.com" },
            new Student() { Id = 103, Name = "Hoàng Phong", Branch = Branch.CE, Gender = Gender.Male, IsRegular = false, Address = "A1-2020", Email = "phong@g.com" },
            new Student() { Id = 104, Name = "Xuân Mai", Branch = Branch.EE, Gender = Gender.Female, IsRegular = false, Address = "A1-2021", Email = "mai@g.com" }
        };

    // Action hiển thị danh sách sinh viên
    [Route("Admin/Student/List", Name = "StudentList")] // Cấu hình Route theo yêu cầu mục 8
    public IActionResult Index()
    {
        return View(listStudents);
    }

    // Action [HttpGet] hiển thị Form thêm mới
    [HttpGet]
    [Route("Admin/Student/Add", Name = "StudentAdd")] // Cấu hình Route theo yêu cầu mục 8
    public IActionResult Create()
    {
        // Lấy danh sách các giá trị Gender
        ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();

        // Lấy danh sách các giá trị Branch hiển thị trên select-option
        ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value = "1" },
                new SelectListItem { Text = "BE", Value = "2" },
                new SelectListItem { Text = "CE", Value = "3" },
                new SelectListItem { Text = "EE", Value = "4" }
            };

        return View();
    }

    // Action [HttpPost] xử lý thêm mới sinh viên
    [HttpPost]
    [Route("Admin/Student/Add")]
    public IActionResult Create(Student s)
    {
        s.Id = listStudents.Last().Id + 1;
        listStudents.Add(s);

        // Sau khi thêm mới, chuyển hướng về trang danh sách sinh viên
        return RedirectToAction("Index");
    }
}