using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPCORE.Data;
using ASPCORE.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using ASPCORE.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ASPCORE.Controllers
{
    [Authorize(Roles = "Student, Admin, Administrator")]//More than one Role we can assign to a single Controller, For Action level Authorization providing Contoller level providation also mandatory
    public class StudentController : Controller
    {
        private readonly DataContext _context;

        public StudentController(DataContext context)
        {
            _context = context;
        }

        //[Authorize(Roles = "Administrator")]//Action level Authorization, Now Studen and Admin can't accessible Index method
        // GET: Student
        public async Task<IActionResult> Index()
        {
            return _context.Students != null ?
                        View(await _context.Students.ToListAsync()) :
                        Problem("Entity set 'DataContext.Students'  is null.");
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(_ => _.StudentCourses).ThenInclude(_ => _.Course).FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            @ViewBag.BT = "Create";
            var courses = _context.Courses.Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Id.ToString()
            }).ToList();
            StudentViewModel vm = new StudentViewModel();
            vm.Courses = courses;
            return View(vm);
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id, Name, Enrolled, Courses")] StudentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    Name = vm.Name,
                    Enrolled = vm.Enrolled
                };

                var StudentCourses = vm.Courses?.Where(_ => _.Selected).Select(_ => _.Value).ToList();//here retrive selected courses
                if (StudentCourses != null)
                {
                    foreach (var item in StudentCourses)
                    {
                        student.StudentCourses?.Add(new StudentCourse()//here not directly insert courses id in StudentCourses table using student table navigate prop StudentCourses for inserting id values of courses, here also store Student model Id in StudentCourses StudentId property
                        {
                            CourseId = int.Parse(item)
                        });
                    }
                    _context.Students.Add(student);
                    _context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            @ViewBag.BT = "Update";

            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(_ => _.StudentCourses).Where(_ => _.Id == id).FirstOrDefaultAsync();
            var selectedIds = student?.StudentCourses?.Select(_ => _.CourseId).ToList();
            if (selectedIds != null)
            {
                var items = _context.Courses.Select(_ => new SelectListItem()
                {
                    Text = _.Title,
                    Value = _.Id.ToString(),
                    Selected = selectedIds.Contains(_.Id)
                }).ToList();
                StudentViewModel vm = new StudentViewModel();
                vm.Name = student.Name;
                vm.Enrolled = student.Enrolled;
                vm.Courses = items;
                return View(vm);
            }
            return View();
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StudentViewModel vm)
        {
            var student = _context.Students.Find(vm.Id);
            student.Name = vm.Name;
            student.Enrolled = vm.Enrolled;
            var studentById = _context.Students.Include(_ => _.StudentCourses).FirstOrDefault(_ => _.Id == vm.Id);
            var existingIds = studentById.StudentCourses.Select(_ => _.CourseId).ToList();
            var selectedIds = vm.Courses.Where(_ => _.Selected).Select(_ => _.Value).Select(int.Parse).ToList(); //here new selected Course id's selecting
            var toAdd = selectedIds.Except(existingIds);
            var toRemove = existingIds.Except(selectedIds);
            student.StudentCourses = student.StudentCourses.Where(_ => !toRemove.Contains(_.CourseId)).ToList();

            foreach (var item in toAdd)
            {
                student.StudentCourses.Add(new StudentCourse()
                {
                    CourseId = item
                });
            }

            _context.Students.Update(student);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'DataContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
