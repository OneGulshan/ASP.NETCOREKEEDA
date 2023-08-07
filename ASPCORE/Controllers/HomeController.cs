using ASPCORE.Infrastructure;
using ASPCORE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASPCORE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITransientService _ts1;
        private readonly ITransientService _ts2;
        private readonly IScopedService _scs1;
        private readonly IScopedService _scs2;
        private readonly ISingletonService _sis1;
        private readonly ISingletonService _sis2;

        public HomeController(ILogger<HomeController> logger, ITransientService ts1, ITransientService ts2, IScopedService scs1, IScopedService scs2, ISingletonService sis1, ISingletonService sis2)
        {
            _logger = logger;
            _ts1 = ts1;
            _ts2 = ts2;
            _scs1 = scs1;
            _scs2 = scs2;
            _sis1 = sis1;
            _sis2 = sis2;
        }

        public IActionResult Index()
        {
            ViewBag.ts1 = _ts1.GetTaskID().ToString();
            ViewBag.ts2 = _ts2.GetTaskID().ToString();
            ViewBag.scs1 = _scs1.GetTaskID().ToString();
            ViewBag.scs2 = _scs2.GetTaskID().ToString();
            ViewBag.sis1 = _sis1.GetTaskID().ToString();
            ViewBag.sis2 = _sis2.GetTaskID().ToString();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}