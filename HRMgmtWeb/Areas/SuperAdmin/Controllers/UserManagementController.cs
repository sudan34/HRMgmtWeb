using HRMgmtWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMgmtWeb.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    //[Authorize(Roles = "SuperAdmin")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagementController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return (IActionResult)View(users);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            // Remove all existing roles
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            // Add new role
            await _userManager.AddToRoleAsync(user, role);

            return (IActionResult)RedirectToAction(nameof(Index));
        }
    }
}
