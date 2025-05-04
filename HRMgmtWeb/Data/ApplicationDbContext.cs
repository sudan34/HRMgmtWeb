using HRMgmtWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRMgmtWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>  // Inherit from IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This line is CRUCIAL for Identity

            // Configure your custom entities
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Name).IsRequired();
                entity.HasIndex(d => d.Code).IsUnique();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasOne(e => e.Department)
                    .WithMany(d => d.Employees)
                    .HasForeignKey(e => e.DepartmentId);
            });

            // Optional: Customize Identity table names
            modelBuilder.Entity<IdentityUser>(b => b.ToTable("Users"));
            modelBuilder.Entity<IdentityUserClaim<string>>(b => b.ToTable("UserClaims"));
            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.ToTable("UserLogins");
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey }); // Composite key
            });
            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.ToTable("UserTokens");
                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            });
            modelBuilder.Entity<IdentityRole>(b => b.ToTable("Roles"));
            modelBuilder.Entity<IdentityRoleClaim<string>>(b => b.ToTable("RoleClaims"));
            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.ToTable("UserRoles");
                b.HasKey(r => new { r.UserId, r.RoleId });
            });
        }
    }
}