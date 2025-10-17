using LeaseLink.Data;
using LeaseLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseLink.Controllers;

public class ApplicationsController(AppDbContext context) : Controller
{
    private readonly AppDbContext _ctx = context;

    [HttpGet]
    public async Task<IActionResult> Create(int propertyId)
    {
        var prop = await _ctx.Properties.FirstOrDefaultAsync(p => p.Id == propertyId);
        if (prop is null) return NotFound();
        ViewBag.Property = prop;
        return View(new TenantApplication { PropertyId = propertyId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TenantApplication model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Property = await _ctx.Properties.FindAsync(model.PropertyId);
            return View(model);
        }
        _ctx.TenantApplications.Add(model);
        await _ctx.SaveChangesAsync();
        return RedirectToAction(nameof(Success));
    }

    public IActionResult Success() => View();
}
