using HRMgmtWeb.Models;
using HRMgmtWeb.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HRMgmtWeb.Services.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> CreateUserAsync(UserCreateVM model);
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<IdentityResult> UpdateUserAsync(UserEditVM model);
        Task<IdentityResult> DeleteUserAsync(string id);
        Task<List<string>> GetUserRolesAsync(ApplicationUser user);
        Task<IdentityResult> UpdateUserRolesAsync(string userId, List<string> roles);
    }
}
