using Microsoft.AspNetCore.Mvc;

namespace ASPCORE.Controllers
{
    [Route("Error/{statusCode}")]
    public class ErrorController : Controller
    {
        public IActionResult Index(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMsg = "Not Found";
                    break;
                default:
                    break;
            }
            return View("NotFound");
        }
    }
}
