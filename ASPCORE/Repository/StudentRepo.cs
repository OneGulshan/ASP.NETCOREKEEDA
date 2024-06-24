using ASPCORE.Data;
using ASPCORE.Infrastructure;
using ASPCORE.Models;

namespace ASPCORE.Repository
{
    public class StudentRepo : IStudentRepo
    {
        private readonly DataContext _context;
        public StudentRepo(DataContext context)
        {
            _context = context;
        }
        public List<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public Student GetByID(int? Id)
        {
            var Student = _context.Students.Where(_ => _.Id == Id).FirstOrDefault();
            return Student;
        }

        public Student Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return student;
        }

        public Student Update(Student student)
        {
            var Student = _context.Students.Where(_ => _.Id == student.Id).FirstOrDefault();
            if (Student != null)
            {
                Student.Name = student.Name;
                Student.Enrolled = student.Enrolled;
                _context.Students.Update(Student);
                _context.SaveChanges();
            }
            return student;
        }

        public Student Delete(int? Id)
        {
            var Student = _context.Students.Find(Id);
            if (Student != null)
            {
                _context.Students.Remove(Student);
                _context.SaveChanges();
            }
            return Student;
        }
    }
}
