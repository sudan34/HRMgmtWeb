using HRMgmtWeb.Models;

namespace HRMgmtWeb.Services.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<Menu>> GetMenuItemsForUserAsync(string role);
    }
}
