using ASPCORE.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPCORE.ViewModels
{
    public class AppUserViewModel
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Address { get; set; } = "";
        public Gender? Gender { get; set; }
        public int CourseId { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public string Email { get; set; } = "";
        public string UserName { get; set; } = "";
    }
}
