using Microsoft.AspNetCore.Identity;

namespace HRMgmtWeb.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<IdentityRole>> GetAllRolesAsync();
        Task<IdentityResult> CreateRoleAsync(string roleName);
        Task<IdentityRole> GetRoleByIdAsync(string id);
        Task<IdentityResult> UpdateRoleAsync(string id, string roleName);
        Task<IdentityResult> DeleteRoleAsync(string id);
    }
}
