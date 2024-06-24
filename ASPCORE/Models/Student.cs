using System.ComponentModel.DataAnnotations;

namespace ASPCORE.Models
{
    public class Student//Here One two many relationship
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public DateTime Enrolled { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } = new HashSet<StudentCourse>();//here set default null value of StudentCourse object type using by HashSet class
    }
}
