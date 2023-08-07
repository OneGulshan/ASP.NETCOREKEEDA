using ASPCORE.Data;
using ASPCORE.Models;
using ASPCORE.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCORE.Controllers
{
    public class RoleController : Controller
    {
        private readonly DataContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager,
            DataContext context,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _context = context;
        }
        public async Task<IActionResult> IndexRole()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            List<RoleStoreViewModel> vm = new List<RoleStoreViewModel>();
            foreach (var item in roles)
            {
                vm.Add(new RoleStoreViewModel { Id = item.Id, RoleName = item.Name });
            }
            return View(vm);
        }

        [Authorize(Policy = "CreateRolePolicy")]
        [HttpGet]
        public async Task<IActionResult> CreateRole(string id)
        {
            if (id != null)
            {
                ViewBag.BT = "Update";
                var role = await _roleManager.Roles.Where(_ => _.Id == id).FirstOrDefaultAsync();//ye hamara identity role hai jise hamein RoleStoreViewModel me convert karna hoga.
                if (role != null && role.Name != null)
                {
                    RoleStoreViewModel vm = new()
                    {
                        Id = role.Id,
                        RoleName = role.Name
                    };
                    return PartialView("_RolePartial", vm);
                }
                return View();
            }
            else
            {
                ViewBag.BT = "Create";
                RoleStoreViewModel vm = new();
                return PartialView("_RolePartial", vm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleStoreViewModel vm)//Create view using RoleStore model
        {
            if (vm.Id != "")
            {
                var role = await _roleManager.Roles.Where(_ => _.Id == vm.Id).FirstOrDefaultAsync();
                if (role != null)
                {
                    role.Name = vm.RoleName;
                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("IndexRole");
                    }
                    return View(result);
                }
                return View();
            }
            else
            {
                var roleExist = await _roleManager.RoleExistsAsync(vm.RoleName);//RoleExistsAsync is the bool type
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(vm.RoleName));
                }
                return RedirectToAction(nameof(IndexRole));
            }
        }

        public async Task<IActionResult> DeleteRole(RoleStoreViewModel vm)
        {
            var role = await _roleManager.Roles.Where(_ => _.Id == vm.Id).FirstOrDefaultAsync();
            role.Name = vm.RoleName;
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("IndexRole");
            }
            return View();
        }
    }
}

//Ham Logon ko Role Add Karne ke liye Role Manager class ki zarurat hoti hai
