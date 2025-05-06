using HRMgmtWeb.Models.ViewModels;
using HRMgmtWeb.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMgmtWeb.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(
            IUserService userService,
            IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            var model = new List<UserListVM>();

            foreach (var user in users)
            {
                model.Add(new UserListVM
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = await _userService.GetUserRolesAsync(user)
                });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles = await _roleService.GetAllRolesAsync();
            var model = new UserCreateVM
            {
                AllRoles = roles
                //RoleOptions = new SelectList(roles, "Name", "Name")
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateVM model)
        {
            var roles = await _roleService.GetAllRolesAsync();
           // model.RoleOptions = new SelectList(roles, "Name", "Name");
           
            if (ModelState.IsValid)
            {
                if (model.SelectedRoles == null || !model.SelectedRoles.Any())
                {
                    ModelState.AddModelError("Roles", "At least one role must be selected");
                }
                else
                {
                    var result = await _userService.CreateUserAsync(model);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserEditVM
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Roles = await _userService.GetUserRolesAsync(user)
            };

            ViewBag.AllRoles = (await _roleService.GetAllRolesAsync()).Select(r => r.Name).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditVM model)
        {
            if (ModelState.IsValid)
            {
                var updateResult = await _userService.UpdateUserAsync(model);
                if (updateResult.Succeeded)
                {
                    var roleResult = await _userService.UpdateUserRolesAsync(model.Id, model.Roles);
                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    foreach (var error in roleResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ViewBag.AllRoles = (await _roleService.GetAllRolesAsync()).Select(r => r.Name).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }

    }
}
