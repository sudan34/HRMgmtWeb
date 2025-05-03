using HRMgmtWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace HRMgmtWeb.Services
{
    public interface ISuperAdminService
    {
        Task EnsureSuperAdminCreatedAsync();
    }
    public class SuperAdminService : ISuperAdminService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SuperAdminConfig _superAdminConfig;

        public SuperAdminService(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<SuperAdminConfig> superAdminConfig)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _superAdminConfig = superAdminConfig.Value;
        }

        public async Task EnsureSuperAdminCreatedAsync()
        {
            // Create SuperAdmin role if not exists
            if (!await _roleManager.RoleExistsAsync("SuperAdmin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }

            // Check if super admin user exists
            var superAdmin = await _userManager.FindByEmailAsync(_superAdminConfig.Email);

            if (superAdmin == null)
            {
                superAdmin = new IdentityUser
                {
                    UserName = _superAdminConfig.UserName,
                    Email = _superAdminConfig.Email,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(superAdmin, _superAdminConfig.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(superAdmin, "SuperAdmin");

                    // Add claims if needed
                    await _userManager.AddClaimsAsync(superAdmin, new[]
                    {
                        new System.Security.Claims.Claim("FirstName", _superAdminConfig.FirstName),
                        new System.Security.Claims.Claim("LastName", _superAdminConfig.LastName),
                        new System.Security.Claims.Claim("FullName", $"{_superAdminConfig.FirstName} {_superAdminConfig.LastName}")
                    });
                }
            }

        }
    }
}
