using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ASPCORE.ViewModels
{
    public class StudentViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        [MaxLength(50, ErrorMessage = "Name is Required Field & Less than 50 Characters !!")]
        public string Name { get; set; } = "";
        public DateTime Enrolled { get; set; }
        public IList<SelectListItem>? Courses { get; set; }
    }
}
//[EmailAddress] <- EmailAddress DataAnnotation For cheacking email address formate by default
