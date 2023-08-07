using ASPCORE.GenericInfrastructure;
using ASPCORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPCORE.Controllers
{
    public class StudentGenericController : Controller
    {
        private readonly IRepo<Student> _repo;//Here IRepo is type of Generic so we required to assign a class to it for using facility of Generic Repository Abstract methods simply.

        public StudentGenericController(IRepo<Student> repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View(_repo.GetAll());
        }

        public IActionResult Details(int id)
        {
            return View(_repo.GetById(id));
        }

        public IActionResult Delete(int id)
        {
            return View(_repo.GetById(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
