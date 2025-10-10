using LeaseLink.Data;
using LeaseLink.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LeaseLink.Controllers
{
    [Authorize(Roles = "Tenant,Executive")]
    public class MaintenanceController : Controller
    {
        private readonly ApplicationDbContext _db;
        public MaintenanceController(ApplicationDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var list = await _db.MaintenanceRequests.Include(m => m.Unit).ThenInclude(u => u.Property)
                                                    .OrderByDescending(m => m.CreatedUtc).ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Units = await _db.Units.Include(u => u.Property)
                            .Select(u => new { u.Id, Label = u.Property.Name + " / " + u.Number }).ToListAsync();
            return View(new CreateVm());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var req = new MaintenanceRequest
            {
                TenantProfileId = 0, // link later when you add TenantProfile per user
                UnitId = vm.UnitId,
                Category = vm.Category,
                Urgency = vm.Urgency,
                Description = vm.Description,
                Status = "Open"
            };
            _db.MaintenanceRequests.Add(req);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public class CreateVm
        {
            [Required] public int UnitId { get; set; }
            [Required] public string Category { get; set; } = "General";
            [Required] public string Urgency { get; set; } = "Normal";
            [Required, MaxLength(600)] public string Description { get; set; } = "";
        }
    }
}
