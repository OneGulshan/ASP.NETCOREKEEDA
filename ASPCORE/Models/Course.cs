using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPCORE.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Code { get; set; } = "";
        public ICollection<StudentCourse>? StudentCourses { get; set; } = new HashSet<StudentCourse>();
    }
}
