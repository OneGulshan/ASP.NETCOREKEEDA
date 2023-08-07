using ASPCORE.Infrastructure;
using ASPCORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPCORE.Controllers
{
    public class StudentUnitController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public StudentUnitController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View(_unitOfWork.StudentRepo.GetAll());
        }

        public IActionResult Details(int id)
        {
            return View(_unitOfWork.StudentRepo.GetByID(id));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            _unitOfWork.StudentRepo.Add(student);
            _unitOfWork.Save();
            return RedirectToAction("Index", "StudentUnit");
        }

        public IActionResult Delete(int id)
        {
             return View(_unitOfWork.StudentRepo.GetByID(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _unitOfWork.StudentRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
