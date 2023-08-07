using ASPCORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPCORE.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            Person persons = new Person { PersonId = 1, PersonName = "Gulshan", Age = 27 };
            return View(persons);
        }
    }
}
