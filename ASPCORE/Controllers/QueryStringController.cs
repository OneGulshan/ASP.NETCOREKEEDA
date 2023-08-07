using Microsoft.AspNetCore.Mvc;

namespace ASPCORE.Controllers
{
    [Route("[controller]")]
    public class QueryStringController : Controller
    {
        [Route("[action]")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetString(string first)
        {
            return View();
        }
    }
}
