using LeaseLink.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaseLink.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // ✅ DbSets only — do NOT declare a "Database" property here
        public DbSet<Property> Properties => Set<Property>();
        public DbSet<Unit> Units => Set<Unit>();
        public DbSet<TenantProfile> TenantProfiles => Set<TenantProfile>();
        public DbSet<RentalApplication> RentalApplications => Set<RentalApplication>();
        public DbSet<Lease> Leases => Set<Lease>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<MaintenanceRequest> MaintenanceRequests => Set<MaintenanceRequest>();

        // ✅ This method must NOT be async
        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);

            b.Entity<Unit>()
                .HasOne(u => u.Property)
                .WithMany(p => p.Units)
                .HasForeignKey(u => u.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            b.Entity<RentalApplication>()
                .HasOne(a => a.Unit)
                .WithMany()
                .HasForeignKey(a => a.UnitId);

            b.Entity<Lease>()
                .HasOne(l => l.Unit).WithMany().HasForeignKey(l => l.UnitId);
            b.Entity<Lease>()
                .HasOne(l => l.Tenant).WithMany().HasForeignKey(l => l.TenantProfileId);

            b.Entity<Invoice>()
                .HasOne(i => i.Lease).WithMany().HasForeignKey(i => i.LeaseId);

            b.Entity<MaintenanceRequest>()
                .HasOne(m => m.Unit).WithMany().HasForeignKey(m => m.UnitId);
            b.Entity<MaintenanceRequest>()
                .HasOne(m => m.Tenant).WithMany().HasForeignKey(m => m.TenantProfileId);
        }
    }
}
