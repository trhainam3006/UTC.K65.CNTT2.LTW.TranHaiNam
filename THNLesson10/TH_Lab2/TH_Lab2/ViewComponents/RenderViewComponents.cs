using Microsoft.AspNetCore.Mvc;
using TH_Lab2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TH_Lab2.ViewComponents
{
    public class RenderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<MenuItem> MenuItems = new List<MenuItem>()
            {
                new MenuItem() { Id = 1, Name = "Branches", Link = "Branches/List" },
                new MenuItem() { Id = 2, Name = "Students", Link = "Students/List" },
                new MenuItem() { Id = 3, Name = "Subjects", Link = "Subjects/List" },
                new MenuItem() { Id = 4, Name = "Courses", Link = "Courses/List" }
            };
            return View("RenderLeftMenu", MenuItems);
        }
    }
}