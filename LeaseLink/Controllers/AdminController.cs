using System.ComponentModel.DataAnnotations;
using LeaseLink.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeaseLink.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // Landing page with buttons
        public IActionResult Index() => View();

        // -------- View Dashboard --------
        [HttpGet]
        public IActionResult Dashboard()
        {
            // Demo numbers – replace with real queries later
            var vm = new AdminDashboardViewModel
            {
                TotalTenants = 124,
                TotalManagers = 7,
                TotalProperties = 56,
                OpenEscalations = 3,
                PendingApplications = 9,
                OccupancyRatePercent = 92
            };
            return View(vm);
        }

        // -------- Send Broadcasts --------
        [HttpGet]
        public IActionResult Broadcasts() => View(new BroadcastViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Broadcasts(BroadcastViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            // TODO: send via email/SMS/notifications here
            TempData["Success"] = $"Broadcast sent to all {vm.Audience} users.";
            return RedirectToAction(nameof(Broadcasts));
        }

        // -------- Review Escalations --------
        [HttpGet]
        public IActionResult Escalations()
        {
            // Demo dataset – replace with DB pull
            var items = new List<EscalationViewModel>
            {
                new() { Id = 101, Title = "Water leak – Unit 3A", Severity = "High",  CreatedOn = DateTime.UtcNow.AddHours(-5), Status = "Open" },
                new() { Id = 102, Title = "Gate access offline",    Severity = "Med",  CreatedOn = DateTime.UtcNow.AddHours(-12), Status = "Investigating" },
                new() { Id = 103, Title = "Noise complaint",        Severity = "Low",  CreatedOn = DateTime.UtcNow.AddDays(-1),   Status = "Open" },
            };
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Resolve(int id)
        {
            // TODO: update status in DB
            TempData["Success"] = $"Escalation #{id} marked RESOLVED.";
            return RedirectToAction(nameof(Escalations));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Dismiss(int id)
        {
            // TODO: update status in DB
            TempData["Success"] = $"Escalation #{id} DISMISSED.";
            return RedirectToAction(nameof(Escalations));
        }

        // -------- Generate Reports --------
        [HttpGet]
        public IActionResult Reports() => View(new ReportRequestViewModel
        {
            From = DateTime.UtcNow.AddDays(-30).Date,
            To = DateTime.UtcNow.Date,
            Type = "Occupancy"
        });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reports(ReportRequestViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            // TODO: generate real report – here’s a mock summary
            vm.GeneratedAt = DateTime.UtcNow;
            vm.Summary = vm.Type switch
            {
                "Occupancy" => "Average occupancy: 92% across 56 properties.",
                "Financials" => "Total rent billed: R 2,140,000; Collected: R 2,020,000.",
                "Maintenance" => "23 tickets opened, 19 closed. SLA met: 87%.",
                _ => "No summary available for the selected report type."
            };

            return View(vm);
        }
    }

    // ---------- View models (kept here for simplicity) ----------

    public class AdminDashboardViewModel
    {
        public int TotalTenants { get; set; }
        public int TotalManagers { get; set; }
        public int TotalProperties { get; set; }
        public int OpenEscalations { get; set; }
        public int PendingApplications { get; set; }
        public int OccupancyRatePercent { get; set; }
    }

    public class BroadcastViewModel
    {
        [Required] public string Audience { get; set; } = "All";
        [Required, StringLength(120)] public string Subject { get; set; } = string.Empty;
        [Required, StringLength(2000)] public string Message { get; set; } = string.Empty;
        public bool Urgent { get; set; }
    }

    public class EscalationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Severity { get; set; } = "Low"; // Low/Med/High
        public DateTime CreatedOn { get; set; }
        public string Status { get; set; } = "Open";  // Open/Investigating/Resolved
    }

    public class ReportRequestViewModel
    {
        [Required, DataType(DataType.Date)]
        public DateTime From { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime To { get; set; }

        [Required]
        public string Type { get; set; } = "Occupancy";  // Occupancy/Financials/Maintenance

        // Output
        public DateTime? GeneratedAt { get; set; }
        public string? Summary { get; set; }
    }
}
