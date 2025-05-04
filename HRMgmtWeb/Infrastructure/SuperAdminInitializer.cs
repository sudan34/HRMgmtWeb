using HRMgmtWeb.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HRMgmtWeb.Infrastructure
{
    public static class SuperAdminInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using var scope = serviceProvider.CreateScope();

            // Initialize SuperAdmin via service
            var superAdminService = scope.ServiceProvider.GetRequiredService<ISuperAdminService>();
            await superAdminService.EnsureSuperAdminCreatedAsync();

            // Initialize other roles
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] otherRoles = { "Admin", "Manager", "User" }; // Exclude SuperAdmin (already handled)

            foreach (var role in otherRoles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}