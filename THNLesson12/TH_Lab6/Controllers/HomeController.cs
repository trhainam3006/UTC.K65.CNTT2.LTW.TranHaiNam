using Microsoft.AspNetCore.Mvc;

namespace TH_Lab6.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => RedirectToAction("Index", "Learner");

    public IActionResult Error()
    {
        return View();
    }
}
