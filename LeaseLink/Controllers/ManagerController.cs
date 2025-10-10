using LeaseLink.Data;
using LeaseLink.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseLink.Controllers
{
    [Authorize(Roles = "Manager,Executive")]
    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ManagerController(ApplicationDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var apps = await _db.RentalApplications
                .Include(a => a.Unit).ThenInclude(u => u.Property)
                .OrderByDescending(a => a.Id).ToListAsync();
            return View(apps);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var app = await _db.RentalApplications.Include(a => a.Unit).FirstOrDefaultAsync(a => a.Id == id);
            if (app == null) return NotFound();
            app.Status = "Approved";
            app.Unit.IsVacant = false;
            await _db.SaveChangesAsync();
            TempData["Msg"] = "Approved. Now create a Lease.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            var app = await _db.RentalApplications.FindAsync(id);
            if (app == null) return NotFound();
            app.Status = "Rejected";
            await _db.SaveChangesAsync();
            TempData["Msg"] = "Rejected.";
            return RedirectToAction(nameof(Index));
        }
    }
}
