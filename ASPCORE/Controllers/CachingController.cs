using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ASPCORE.Controllers
{
    public class CachingController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        public CachingController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public IActionResult Index()
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
    }
}
