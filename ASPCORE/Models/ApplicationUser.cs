using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ASPCORE.Models
{    
    public class ApplicationUser : IdentityUser
    {
        [Key]
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Address { get; set; } = "";
        public Gender? Gender { get; set; }
    }
    public enum Gender
    {
        Male,
        Female,
        Others
    }
}
