using ASPCORE.Models;

namespace ASPCORE.Infrastructure
{
    public interface IStudentRepo
    {
        Student Add(Student student);
        List<Student> GetAll();
        Student GetByID(int? Id);
        Student Update(Student student);
        Student Delete(int? Id);
    }
}
