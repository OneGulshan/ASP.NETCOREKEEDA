using ASPCORE.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPCORE.Controllers
{
    public class CascadeDropdownListController : Controller
    {
        private readonly DataContext _context;
        public CascadeDropdownListController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Countries = new SelectList(_context.Country, "Id", "Name");
            return View();
        }

        public JsonResult GetStates(int CountryId)
        {
            var StateList = _context.State.Where(_=>_.CountryId == CountryId);
            return Json(new SelectList(StateList, "Id", "Name"));
        }
    }
}
