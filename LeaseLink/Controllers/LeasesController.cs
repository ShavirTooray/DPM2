using LeaseLink.Data;
using LeaseLink.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseLink.Controllers
{
    [Authorize(Roles = "Manager,Executive")]
    public class LeasesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public LeasesController(ApplicationDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var leases = await _db.Leases.Include(l => l.Unit).ThenInclude(u => u.Property)
                                         .Include(l => l.Tenant).ToListAsync();
            return View(leases);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Units = await _db.Units.Where(u => u.IsVacant).Include(u => u.Property)
                              .Select(u => new { u.Id, Label = u.Property.Name + " / " + u.Number }).ToListAsync();
            ViewBag.Tenants = await _db.TenantProfiles.Select(t => new { t.Id, t.FullName }).ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int unitId, int tenantProfileId, DateOnly start, DateOnly end, decimal deposit)
        {
            _db.Leases.Add(new Lease { UnitId = unitId, TenantProfileId = tenantProfileId, Start = start, End = end, Deposit = deposit, Status = "Active" });
            var unit = await _db.Units.FindAsync(unitId);
            if (unit != null) unit.IsVacant = false;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
