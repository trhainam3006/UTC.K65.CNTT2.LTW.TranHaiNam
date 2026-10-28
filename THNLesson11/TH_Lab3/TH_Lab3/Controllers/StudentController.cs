
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TH_Lab3.Models;

namespace TH_Lab3.Controllers
{
    public class StudentController : Controller
    {
        private static List<Student> listStudents = new List<Student>();

        public IActionResult Index()
        {
            return View(listStudents);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {
                s.Id = listStudents.Count == 0
                    ? 1
                    : listStudents.Max(x => x.Id) + 1;

                listStudents.Add(s);

                return RedirectToAction("Index");
            }

            LoadData();
            return View(s);
        }

        private void LoadData()
        {
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender))
                .Cast<Gender>().ToList();

            ViewBag.AllBranches = new List<SelectListItem>
            {
                new SelectListItem { Text = "IT", Value = "1" },
                new SelectListItem { Text = "BE", Value = "2" },
                new SelectListItem { Text = "CE", Value = "3" },
                new SelectListItem { Text = "EE", Value = "4" }
            };
        }
    }
}
