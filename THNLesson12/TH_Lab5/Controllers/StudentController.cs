using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TH_Lab5.Models;

namespace TH_Lab5.Controllers;

// Lab3 validation demonstration; data is in-memory, not saved to SQL Server.
public class StudentController : Controller
{
    private static readonly List<Student> Students = new();
    private static readonly object SyncRoot = new();

    [HttpGet]
    public IActionResult Index()
    {
        lock (SyncRoot) return View(Students.OrderBy(s => s.Id).ToList());
    }

    [HttpGet]
    public IActionResult Create()
    {
        LoadChoices();
        return View(new Student());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Student student)
    {
        if (ModelState.IsValid)
        {
            // For a classroom-only demo, never keep the submitted password.
            student.Password = null;
            lock (SyncRoot)
            {
                student.Id = Students.Count == 0 ? 1 : Students.Max(s => s.Id) + 1;
                Students.Add(student);
            }
            TempData["Success"] = "Validation thành công. Đã thêm sinh viên (lưu tạm trong bộ nhớ).";
            return RedirectToAction(nameof(Index));
        }
        LoadChoices();
        return View(student);
    }

    private void LoadChoices()
    {
        ViewBag.AllBranches = Enum.GetValues<Branch>()
            .Select(b => new SelectListItem { Text = b.ToString(), Value = b.ToString() })
            .ToList();
        ViewBag.AllGenders = Enum.GetValues<Gender>().ToList();
    }
}
