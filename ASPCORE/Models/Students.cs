using System.ComponentModel.DataAnnotations;

namespace ASPCORE.Models
{
    public class Students
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public DateTime Enrolled { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } = new HashSet<StudentCourse>();//here set default null value of StudentCourse object type using by HashSet class
    }
}
