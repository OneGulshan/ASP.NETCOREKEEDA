using ASPCORE.Data;
using ASPCORE.Models;
using ASPCORE.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPCORE.Controllers
{
    public class TestController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webhostenvironment;
        private readonly RoleManager<IdentityRole> _roleManager;
        public TestController(DataContext context,
            IWebHostEnvironment webhostenvironment,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _webhostenvironment = webhostenvironment;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View(_context.Test.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Courses = new SelectList(_context.Courses, "Id", "Title"); //Here bind Courses using ViewBag with making Courses SelectList Type for making DropDownList
            return View();
        }

        [HttpPost]
        public IActionResult Create(AppUserViewModel vm)
        {
            CookieOptions cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(1);//Here Cookies Saved for a day
            Response.Cookies.Append("Profile", vm.ProfileImage.FileName, cookies);
            ViewBag.Saved = "Cookie Saved";

            string stringFileName = UploadFile(vm);
            var test = new Test
            {
                ProfileImage = stringFileName
            };
            _context.Test.Add(test);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult ReadCookies()
        {
            ViewBag.Profile = Request.Cookies["Profile"]?.ToString();
            return View("Create");
        }

        private string UploadFile(AppUserViewModel vm)
        {
            string fileName = null;
            if (vm.ProfileImage != null)
            {
                string uploadDir = Path.Combine(_webhostenvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + vm.ProfileImage.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.ProfileImage.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        public IActionResult CourseCreate()
        {
            var Course = new List<CourseViewModel>()
            {
                new CourseViewModel{Id=1, Name="IT"},
                new CourseViewModel{Id=2, Name="CS"}
            };
            ViewBag.CourseList = new SelectList(Course, "Id", "Name", 2);
            return View();
        }
    }
}
