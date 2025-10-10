using LeaseLink.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseLink.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PropertiesController(ApplicationDbContext db) => _db = db;

        public async Task<IActionResult> Index(string? city)
        {
            var units = _db.Units.Include(u => u.Property).Where(u => u.IsVacant);
            if (!string.IsNullOrWhiteSpace(city)) units = units.Where(u => u.Property.City == city);
            return View(await units.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var unit = await _db.Units.Include(u => u.Property).FirstOrDefaultAsync(u => u.Id == id);
            if (unit == null) return NotFound();
            return View(unit);
        }
    }
}
