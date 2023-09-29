using Microsoft.AspNetCore.Mvc;

namespace ASPCORE.Controllers
{
    public class SignalRClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
