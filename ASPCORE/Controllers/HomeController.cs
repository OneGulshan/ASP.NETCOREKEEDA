using ASPCORE.Infrastructure;
using ASPCORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace ASPCORE.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITransientService _ts1;
        private readonly ITransientService _ts2;
        private readonly IScopedService _scs1;
        private readonly IScopedService _scs2;
        private readonly ISingletonService _sis1;
        private readonly ISingletonService _sis2;
        private readonly IMemoryCache _memoryCache;
        public HomeController(ILogger<HomeController> logger, ITransientService ts1, ITransientService ts2, IScopedService scs1, IScopedService scs2, ISingletonService sis1, ISingletonService sis2
            , IMemoryCache memoryCache)
        {
            _logger = logger;
            _ts1 = ts1;
            _ts2 = ts2;
            _scs1 = scs1;
            _scs2 = scs2;
            _sis1 = sis1;
            _sis2 = sis2;
            _memoryCache = memoryCache;
        }

        [Route("[action]")]
        public IActionResult DependencyIndex()
        {
            ViewBag.ts1 = _ts1.GetTaskID().ToString();
            ViewBag.ts2 = _ts2.GetTaskID().ToString();
            ViewBag.scs1 = _scs1.GetTaskID().ToString();
            ViewBag.scs2 = _scs2.GetTaskID().ToString();
            ViewBag.sis1 = _sis1.GetTaskID().ToString();
            ViewBag.sis2 = _sis2.GetTaskID().ToString();
            return View();
        }

        [Route("[action]")]
        public IActionResult CachingIndex()
        {
            DateTime CurrentTime;
            bool value = _memoryCache.TryGetValue("CachedTime", out CurrentTime);
            if (!value)
            {
                CurrentTime = DateTime.Now;
                var cachedEntryOption = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(10));
                _memoryCache.Set("CachedTime", CurrentTime, cachedEntryOption);
            }
            return View(CurrentTime);
        }

        [Route("[action]")]
        public IActionResult QueryStringIndex()
        {
            return View();
        }
        [Route("[action]")]
        public IActionResult GetString(string first)
        {
            return View();
        }
        [Route("[action]")]
        public IActionResult PersonIndex()
        {
            Person persons = new() { PersonId = 1, PersonName = "Gulshan", Age = 27 };
            return View(persons);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}