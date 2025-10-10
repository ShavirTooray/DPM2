using LeaseLink.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;            

namespace LeaseLink.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // ✅ Create the in-memory store (no cast, no temp variable)
            await ctx.Database.EnsureCreatedAsync();

            string[] roles = { "Admin", "Manager", "Tenant", "Executive" };
            foreach (var r in roles)
                if (!await roleMgr.RoleExistsAsync(r))
                    await roleMgr.CreateAsync(new IdentityRole(r));

            var exec = await userMgr.FindByEmailAsync("exec@leaselink.local");
            if (exec == null)
            {
                exec = new ApplicationUser
                {
                    UserName = "exec@leaselink.local",
                    Email = "exec@leaselink.local",
                    EmailConfirmed = true
                };
                await userMgr.CreateAsync(exec, "P@ssw0rd!");
                await userMgr.AddToRolesAsync(exec, roles);
            }

            if (!ctx.Properties.Any())
            {
                var p = new Property
                {
                    Name = "Sunset Villas",
                    Address = "12 Main Road",
                    City = "Cape Town",
                    Province = "Western Cape",
                    Units = new List<Unit>
                    {
                        new Unit { Number = "A1", MonthlyRent = 9500m, IsVacant = true, Description = "2 bed garden unit" },
                        new Unit { Number = "B2", MonthlyRent = 11500m, IsVacant = true, Description = "3 bed upstairs" }
                    }
                };
                ctx.Properties.Add(p);
                await ctx.SaveChangesAsync();

                // Admin
                var admin = await userMgr.FindByEmailAsync("admin@leaselink.local");
                if (admin == null)
                {
                    admin = new ApplicationUser { UserName = "admin@leaselink.local", Email = "admin@leaselink.local", EmailConfirmed = true };
                    await userMgr.CreateAsync(admin, "P@ssw0rd!");
                    await userMgr.AddToRoleAsync(admin, "Admin");
                }

                // Manager
                var manager = await userMgr.FindByEmailAsync("manager@leaselink.local");
                if (manager == null)
                {
                    manager = new ApplicationUser { UserName = "manager@leaselink.local", Email = "manager@leaselink.local", EmailConfirmed = true };
                    await userMgr.CreateAsync(manager, "P@ssw0rd!");
                    await userMgr.AddToRoleAsync(manager, "Manager");
                }

                // Tenant
                var tenantUser = await userMgr.FindByEmailAsync("tenant@leaselink.local");
                if (tenantUser == null)
                {
                    tenantUser = new ApplicationUser { UserName = "tenant@leaselink.local", Email = "tenant@leaselink.local", EmailConfirmed = true };
                    await userMgr.CreateAsync(tenantUser, "P@ssw0rd!");
                    await userMgr.AddToRoleAsync(tenantUser, "Tenant");
                }

            }
        }
    }
}
