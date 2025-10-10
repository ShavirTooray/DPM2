using LeaseLink.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseLink.Controllers
{
    [Authorize(Roles = "Tenant,Executive")]
    public class InvoicesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public InvoicesController(ApplicationDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            // For MVP simplicity, list all invoices (later filter by logged-in tenant)
            var invoices = await _db.Invoices.Include(i => i.Lease).ThenInclude(l => l.Unit).ThenInclude(u => u.Property)
                                             .OrderByDescending(i => i.Period).ToListAsync();
            return View(invoices);
        }
    }
}
