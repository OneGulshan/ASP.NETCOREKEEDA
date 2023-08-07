using System.Security.Claims;

namespace ASPCORE.ViewModels
{
    public class ClaimStoreViewModel
    {
        public static List<Claim> All = new()
        {
            new Claim("Create Role","Create Role"),
            new Claim("Edit Role","Edit Role"),
            new Claim("Delete Role","Delete Role")
        };
    }
}
