using ASPCORE.Models;
using Microsoft.AspNetCore.Identity;

namespace ASPCORE.Data
{
    public static class DbInitializer
    {
        //Hamara IServiceProvider, RoleManager ko add karne ka kaam karta hai
        public static async Task InitializerAsync(IServiceProvider serviceProvider, UserManager<ApplicationUser> _userManager)
        {
            //Here Creating a Role
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();//Here defining adding a role
            string[] roleNames = { "SuperAdmin", "CustomerCare" };
            IdentityResult result;
            foreach (var roleName in roleNames)//Here Adding Roles
            {
                var roleExists = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    result = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Here Creating Our Super Admin
            string Email = "superadmin@gmail.com";
            string password = "Super@123897";

            if (_userManager.FindByEmailAsync(Email).Result == null)//Here Checking user exist or not using his email id
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = Email;
                user.Email = Email;
                IdentityResult resultIdentity = _userManager.CreateAsync(user, password).Result;//Here Creating our user
                if (resultIdentity.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "SuperAdmin").Wait();//Here Assigning SuperAdmin role to our user
                }
            }
        }
    }
}
