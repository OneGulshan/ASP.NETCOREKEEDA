using ASPCORE.Infrastructure;
using ASPCORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPCORE.Controllers
{
    public class StudentRepositoryController : Controller
    {
        private readonly IStudentRepo _repo;
        public StudentRepositoryController(IStudentRepo repo)
        {
            _repo = repo;
        }

        public IActionResult Create(int id)
        {
            if (id > 0)
            {
                ViewBag.BT = "Update";
                return View(_repo.GetByID(id));
            }
            ViewBag.BT = "Create";
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (student.Id == 0)
            {
                _repo.Add(student);
                return RedirectToAction("Index");
            }
            else
            {
                _repo.Update(student);
                return RedirectToAction("Index");
            }
        }
        public IActionResult Index()
        {
            return View(_repo.GetAll());
        }
        public IActionResult Details(int id)
        {
            return View(_repo.GetByID(id));
        }
        public IActionResult Delete(int? id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
