using CinemaWebAppOriginal.Areas.Admin.Models.UserManagement;
using CinemaWebAppOriginal.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebAppOriginal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;


        public UserManagementController(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole<Guid>> _roleManager)
        {
            this.userManager = _userManager;
            this.roleManager = _roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = userManager.Users.ToList();
            List<UserViewModel> userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return View(userViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(Guid userId, string role) // assign role to user
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user == null) // User not found error handling
            {
                return NotFound();
            }

            if (!await roleManager.RoleExistsAsync(role)) // Role not found error handling
            {
                return NotFound();
            }

            var result = await userManager.AddToRoleAsync(user, role);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(Guid userId, string role) // remove role from user
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user == null) // User not found error handling
            {
                return NotFound();
            }
            if (!await roleManager.RoleExistsAsync(role)) // Role not found error handling
            {
                return NotFound();
            }

            var result = await userManager.RemoveFromRoleAsync(user, role);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest(result.Errors);
        }

        public async Task<IActionResult> DeleteUser(Guid userId) // delete user
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user == null) // User not found error handling
            {
                return NotFound();
            }

            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest(result.Errors);
        }



    }
}
