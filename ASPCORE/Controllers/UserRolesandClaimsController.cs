using ASPCORE.Data;
using ASPCORE.Models;
using ASPCORE.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASPCORE.Controllers
{
    public class UserRolesandClaimsController : Controller
    {
        private readonly DataContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRolesandClaimsController(RoleManager<IdentityRole> roleManager,
            DataContext context,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Edit(string Id)
        {
            UserRolesandClaimsViewModel userRolesandClaims = new();
            var user = _context.Users.Where(_ => _.Id == Id).SingleOrDefault();
            var userInRoles = _context.UserRoles.Where(_ => _.UserId == Id).Select(_ => _.RoleId).ToList();
            var userInClaims = _context.UserClaims.Where(_ => _.UserId == Id).Select(_ => _.ClaimValue).ToList();

            userRolesandClaims.Roles = await _roleManager.Roles.Select(_ => new SelectListItem()
            {
                Text = _.Name,
                Value = _.Id,
                Selected = userInRoles.Contains(_.Id)//Here our Roles created with selected value if selected using Contains id
            }).ToListAsync();
            userRolesandClaims.AppUser = user;

            userRolesandClaims.Claims = ClaimStoreViewModel.All.Select(_ => new SelectListItem()
            {
                Text = _.Type,
                Value = _.Value,
                Selected = userInClaims.Contains(_.Value)//Here our Cliams created with selected value if selected using Contains value
            }).ToList();

            return View(userRolesandClaims);
        }

        [HttpPost]
        public IActionResult Edit(UserRolesandClaimsViewModel model)
        {
            //User Roles
            var selectedRoleId = model.Roles.Where(_ => _.Selected).Select(_ => _.Value);
            var AlreadyExistRoleId = _context.UserRoles.Where(_ => _.UserId == model.AppUser.Id).Select(_ => _.RoleId).ToList();
            var toAddRoles = selectedRoleId.Except(AlreadyExistRoleId);
            var toRemoveRoles = AlreadyExistRoleId.Except(selectedRoleId);

            foreach (var item in toRemoveRoles)//Here Removing Roles from User in Identity
            {
                _context.UserRoles.Remove(new IdentityUserRole<string>
                {
                    RoleId = item,
                    UserId = model.AppUser.Id
                });
            }

            foreach (var item in toAddRoles)//Here Adding Role on User in Identity
            {
                _context.UserRoles.Add(new IdentityUserRole<string>
                {
                    RoleId = item,
                    UserId = model.AppUser.Id
                });
            }

            //User Claims
            var selectedClaimValue = model.Claims.Where(_ => _.Selected).Select(_ => _.Value);
            var AlreadyExistClaimValue = _context.UserClaims.Where(_ => _.UserId == model.AppUser.Id).Select(_ => _.Id.ToString()).ToList();
            var toAddClaims = selectedClaimValue.Except(AlreadyExistClaimValue);
            var toRemoveClaims = AlreadyExistClaimValue.Except(selectedClaimValue);

            foreach (var item in toRemoveClaims)//Here Removing Cliams from User in Identity
            {
                _context.UserClaims.Remove(new IdentityUserClaim<string>
                {
                    Id = Convert.ToInt32(item),
                    UserId = model.AppUser.Id
                });
            }

            foreach (var item in toAddClaims)//Here Adding Role on User in Identity
            {
                _context.UserClaims.Add(new IdentityUserClaim<string>
                {
                    UserId = model.AppUser.Id,
                    ClaimValue = item,
                    ClaimType = item
                });
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
