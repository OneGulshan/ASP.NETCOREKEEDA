using System.ComponentModel.DataAnnotations;

namespace ASPCORE.ViewModels
{
    public class RoleStoreViewModel
    {
        [Key]
        public string Id { get; set; } = "";
        public string RoleName { get; set; } = "";
    }
}
