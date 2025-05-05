using HRMgmtWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRMgmtWeb.Components
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IMenuService _menuService;

        public MenuViewComponent(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var role = UserClaimsPrincipal.FindFirst(ClaimTypes.Role)?.Value;
            var menuItems = await _menuService.GetMenuItemsForUserAsync(role);
            return View(menuItems);
        }
    }
}