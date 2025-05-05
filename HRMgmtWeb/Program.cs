using HRMgmtWeb.Data;
using HRMgmtWeb.Data.Interfaces;
using HRMgmtWeb.Data.Repositories;
using HRMgmtWeb.Infrastructure;
using HRMgmtWeb.Models;
using HRMgmtWeb.Services;
using HRMgmtWeb.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add configuration binding
//builder.Services.Configure<SuperAdminConfig>(builder.Configuration.GetSection("SuperAdmin"));

// Database Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString,
        sqlOptions => sqlOptions.EnableRetryOnFailure()));

// Add Identity with proper configuration
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Configure authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SuperAdminPolicy", policy =>
        policy.RequireRole("SuperAdmin"));

    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("Admin", "SuperAdmin"));

    options.AddPolicy("HRPolicy", policy =>
        policy.RequireRole("HR", "Admin", "SuperAdmin"));

    options.AddPolicy("EmployeePolicy", policy =>
        policy.RequireRole("Employee"));
});

//builder.Services.Configure<SuperAdminConfig>(builder.Configuration.GetSection("SuperAdmin"));
// Add SuperAdmin service
//builder.Services.AddScoped<ISuperAdminService, SuperAdminService>();

// Session Configuration
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

// Register application services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();

// Application Services
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IMenuService, MenuService>();

// Uncomment when ready to use
// builder.Services.AddTransient<IStartupFilter, SuperAdminStartupFilter>();

var app = builder.Build();

// Initialize SuperAdmin
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        // Create roles if they don't exist
        string[] roleNames = { "SuperAdmin", "Admin", "HR", "Employee" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Create initial admin user if not exists
        var adminEmail = "admin@gmail.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "System",
                LastName = "Admin",
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(adminUser, "Admin@1234");
            if (createResult.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// IMPORTANT: Authentication must come before Authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

// Map controllers
// Map routes
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();