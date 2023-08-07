using ASPCORE.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPCORE.ViewModels
{
    public class UserRolesandClaimsViewModel
    {
        public ApplicationUser? AppUser { get; set; }
        public List<SelectListItem>? Roles { get; set; }
        public List<SelectListItem>? Claims { get; set; }
    }
}
