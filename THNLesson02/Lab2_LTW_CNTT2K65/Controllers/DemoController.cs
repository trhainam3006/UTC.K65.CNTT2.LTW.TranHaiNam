using Microsoft.AspNetCore.Mvc;

namespace Lab2_LTW_CNTT2K65.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
