using Microsoft.AspNetCore.Mvc;

namespace ASPCORE.Areas.Admin.Controllers
{
    [Area("Admin")]//Without Area defining Controller can't accessible
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
