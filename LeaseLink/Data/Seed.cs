using System;
using System.Threading.Tasks;
using LeaseLink.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LeaseLink.Data
{
    public static class Seed
    {
        public static async Task RunAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Ensure DB & schema
            await ctx.Database.MigrateAsync();

            // Roles
            var roles = new[] { "Admin", "Manager", "HigherManagement", "Tenant" };
            foreach (var r in roles)
                if (!await roleManager.RoleExistsAsync(r))
                    await roleManager.CreateAsync(new IdentityRole(r));

            // Demo users (password: Pass123!)
            await CreateUserIfMissing(userManager, "admin@leaselink.com", "Admin");
            await CreateUserIfMissing(userManager, "manager@leaselink.com", "Manager");
            await CreateUserIfMissing(userManager, "exec@leaselink.com", "HigherManagement");
            await CreateUserIfMissing(userManager, "tenant@leaselink.com", "Tenant");

            // Properties (UPSERT: update if exists, insert if missing)
            await UpsertProperty(ctx, new Property
            {
                Title = "Garden Cottage",
                Address = "42 Maple Ave, Pretoria",
                MonthlyRent = 9500,
                Bedrooms = 2,
                Bathrooms = 1,
                ImageUrl = "/images/properties/garden.jpg",
                Description = "Quiet cul-de-sac, private garden, pet friendly."
            });

            await UpsertProperty(ctx, new Property
            {
                Title = "Modern 2-Bed Apartment",
                Address = "12 Beach Rd, Cape Town",
                MonthlyRent = 14500,
                Bedrooms = 2,
                Bathrooms = 1,
                ImageUrl = "/images/properties/modern2bed.jpg",
                Description = "Sea views, safe parking, close to MyCiTi."
            });

            await UpsertProperty(ctx, new Property
            {
                Title = "City Loft",
                Address = "101 Bree St, Cape Town CBD",
                MonthlyRent = 16500,
                Bedrooms = 1,
                Bathrooms = 1,
                ImageUrl = "/images/properties/loft.jpg",
                Description = "Open-plan loft with balcony, secure block, fiber ready."
            });

            await UpsertProperty(ctx, new Property
            {
                Title = "Family Home",
                Address = "5 Acacia St, Johannesburg",
                MonthlyRent = 21000,
                Bedrooms = 3,
                Bathrooms = 2,
                ImageUrl = "/images/properties/family.jpg",
                Description = "Spacious yard, pets allowed, near schools."
            });

            // NEW #5
            await UpsertProperty(ctx, new Property
            {
                Title = "Seaside Studio",
                Address = "7 Marine Dr, Durban",
                MonthlyRent = 8200,
                Bedrooms = 1,
                Bathrooms = 1,
                ImageUrl = "/images/properties/studio.jpg",
                Description = "Compact studio with ocean glimpses, walk to promenade."
            });

            // NEW #6
            await UpsertProperty(ctx, new Property
            {
                Title = "Suburban Townhouse",
                Address = "18 Willow Lane, Centurion",
                MonthlyRent = 17500,
                Bedrooms = 3,
                Bathrooms = 2,
                ImageUrl = "/images/properties/townhouse.jpg",
                Description = "Lock-up-and-go townhouse, double garage, complex pool."
            });
        }

        // Helpers ---------------------------------------------------------

        private static async Task CreateUserIfMissing(UserManager<IdentityUser> um, string email, string role)
        {
            var u = await um.FindByEmailAsync(email);
            if (u == null)
            {
                u = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
                await um.CreateAsync(u, "Pass123!");
                await um.AddToRoleAsync(u, role);
            }
            else if (!await um.IsInRoleAsync(u, role))
            {
                await um.AddToRoleAsync(u, role);
            }
        }

        // UPSERT by natural key (Title + Address)
        private static async Task UpsertProperty(AppDbContext ctx, Property p)
        {
            var existing = await ctx.Properties
                .FirstOrDefaultAsync(x => x.Title == p.Title && x.Address == p.Address);

            if (existing == null)
            {
                ctx.Properties.Add(p);
            }
            else
            {
                existing.MonthlyRent = p.MonthlyRent;
                existing.Bedrooms = p.Bedrooms;
                existing.Bathrooms = p.Bathrooms;
                existing.Description = p.Description;
                existing.ImageUrl = p.ImageUrl; // ensure local path takes effect
            }

            await ctx.SaveChangesAsync();
        }
    }
}
