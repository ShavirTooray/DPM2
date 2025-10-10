using LeaseLink.Data;
using LeaseLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LeaseLink.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ApplicationsController(ApplicationDbContext db) => _db = db;

        public async Task<IActionResult> Create(int unitId)
        {
            var unit = await _db.Units.Include(u => u.Property).FirstOrDefaultAsync(x => x.Id == unitId);
            if (unit == null || !unit.IsVacant) return NotFound();
            return View(new ApplicationVm
            {
                UnitId = unitId,
                UnitSummary = $"{unit.Property.Name} - {unit.Number} (R{unit.MonthlyRent})"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            if (!await _db.Units.AnyAsync(u => u.Id == vm.UnitId)) return NotFound();

            _db.RentalApplications.Add(new RentalApplication
            {
                FullName = vm.FullName,
                Email = vm.Email,
                Phone = vm.Phone,
                UnitId = vm.UnitId,
                Notes = vm.Notes,
                Status = "Submitted"
            });
            await _db.SaveChangesAsync();
            TempData["Msg"] = "Application submitted.";
            return RedirectToAction("Index", "Properties");
        }

        public class ApplicationVm
        {
            [Required] public int UnitId { get; set; }
            public string UnitSummary { get; set; } = "";
            [Required, MaxLength(80)] public string FullName { get; set; } = "";
            [Required, EmailAddress] public string Email { get; set; } = "";
            [MaxLength(120)] public string? Phone { get; set; }
            [MaxLength(500)] public string? Notes { get; set; }
        }
    }
}
