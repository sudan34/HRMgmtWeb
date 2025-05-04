using HRMgmtWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace HRMgmtWeb.Services
{
    public interface ISuperAdminService
    {
        Task EnsureSuperAdminCreatedAsync();
    }

    public class SuperAdminService : ISuperAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SuperAdminConfig _superAdminConfig;

        public SuperAdminService(
            UserManager<ApplicationUser> userManager,
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
                superAdmin = new ApplicationUser
                {
                    UserName = _superAdminConfig.UserName,
                    Email = _superAdminConfig.Email,
                    EmailConfirmed = true,
                    FirstName = _superAdminConfig.FirstName,
                    LastName = _superAdminConfig.LastName
                };

                var result = await _userManager.CreateAsync(superAdmin, _superAdminConfig.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(superAdmin, "SuperAdmin");

                    // Add claims if needed
                    await _userManager.AddClaimsAsync(superAdmin, new[]
                    {
                        new Claim(ClaimTypes.GivenName, _superAdminConfig.FirstName),
                        new Claim(ClaimTypes.Surname, _superAdminConfig.LastName),
                        new Claim(ClaimTypes.Name, $"{_superAdminConfig.FirstName} {_superAdminConfig.LastName}")
                    });
                }
            }
            else
            {
                // Update existing super admin if needed
                if (superAdmin.FirstName != _superAdminConfig.FirstName ||
                    superAdmin.LastName != _superAdminConfig.LastName)
                {
                    superAdmin.FirstName = _superAdminConfig.FirstName;
                    superAdmin.LastName = _superAdminConfig.LastName;
                    await _userManager.UpdateAsync(superAdmin);
                }
            }
        }
    }
}