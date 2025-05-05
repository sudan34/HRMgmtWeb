using HRMgmtWeb.Data;
using HRMgmtWeb.Models;
using HRMgmtWeb.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMgmtWeb.Services
{
    public class MenuService : IMenuService
    {
        private readonly ApplicationDbContext _context;

        public MenuService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Menu>> GetMenuItemsForUserAsync(string role)
        {
            return await _context.Menus
                .Where(m => m.IsActive &&
                           (string.IsNullOrEmpty(m.RoleRequired) || m.RoleRequired == role) &&
                           m.ParentId == null)
                .Include(m => m.Children
                    .Where(c => c.IsActive &&
                               (string.IsNullOrEmpty(c.RoleRequired) || c.RoleRequired == role))
                    .OrderBy(c => c.Order))
                .OrderBy(m => m.Order)
                .ToListAsync();
        }
    }
}