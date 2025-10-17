using LeaseLink.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseLink.Controllers;

public class HomeController(AppDbContext context) : Controller
{
    private readonly AppDbContext _ctx = context;

    // Welcome page
    public IActionResult Index() => View();

    // Browse as a tenant
    public async Task<IActionResult> Browse()
    {
        var props = await _ctx.Properties.OrderBy(p => p.MonthlyRent).ToListAsync();
        return View(props);
    }
}
