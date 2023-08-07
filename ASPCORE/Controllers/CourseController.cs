using ASPCORE.Data;
using ASPCORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPCORE.Controllers
{
    //[Route("[controller]")]//Attribute Routing using Tokens
    ////[Route("Course")]
    public class CourseController : Controller
    {
        private readonly DataContext _context;
        public CourseController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get Section of Course Model
        /// </summary>
        /// <returns></returns>

        //[Route("[action]")]
        public IActionResult Create(int id1)
        {
            ViewBag.BT = "Create";

            if (id1 > 0)
            {
                ViewBag.BT = "Update";
                var Course = _context.Courses.Where(x => x.Id == id1).FirstOrDefault();
                return View(Course);
            }
            return View();
        }
        //[Route("")]
        //[Route("[action]")]
        //[Route("~/")]

        ////[Route("")]
        ////[Route("Index")]
        ////[Route("~/")]//In Controller Multiple Action Methods/EndPoint Found, so for Calling Perticulr endpoint used ~ tild simble here

        ////[Route("")]
        ////[Route("Home")]
        ////[Route("Home/Index")]

        public IActionResult Index(string sortOrder)
        {
            ViewData["TitleSort"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";

            var courses = _context.Courses.ToList();
            switch (sortOrder)
            {
                case "title_desc":
                    courses = courses.OrderByDescending(s => s.Title).ToList();
                    break;
                default:
                    courses = courses.OrderBy(s => s.Title).ToList();
                    break;
            }


            return View(courses);
        }

        public IActionResult Delete(int id)
        {
            var Course = _context.Courses.Where(x => x.Id == id).FirstOrDefault();
            if (Course != null)
            {
                _context.Courses.Remove(Course);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //[Route("Course/Details/{id?}")]//Here id is option parameter
        public IActionResult Details(int? id)
        {
            var Course = _context.Courses.Where(x => x.Id == id).FirstOrDefault();

            if (id == null || Course == null)
            {
                return View("NotFound", id);
            }

            //ViewData["course"] = Course;
            @ViewData["Title"] = "Details Section";
            ViewBag.Course = Course;
            ViewData["Title"] = "Detailed Section";
            return View(Course);
        }

        /// <summary>
        /// Post Section of Course Model
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            if (course.Id > 0)
            {
                _context.Courses.Update(course);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            _context.Courses.Add(course);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
