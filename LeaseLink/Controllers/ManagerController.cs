using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaseLink.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public ManagerController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // Landing page with the buttons
        [HttpGet]
        public IActionResult Index() => View();

        // ---------------- Dashboard View ----------------
        [HttpGet]
        public IActionResult Dashboard()
        {
            var vm = new ManagerDashboardVm
            {
                ActiveListings = 18,
                PendingApplications = 6,
                LeasesExpiringThisMonth = 4,
                OverdueReminders = 3,
                OpenMaintenanceTickets = 7
            };
            return View(vm);
        }

        // ---------------- Update Listings ----------------
        [HttpGet]
        public IActionResult Listings()
        {
            var vm = new ListingsVm
            {
                Items = new List<ListingItemVm>
                {
                    new() { Id = 1, Title = "Sea View Apartment", Address = "12 Beach Rd, CPT",  Rent = 14500, Status = "Active" },
                    new() { Id = 2, Title = "City Loft",          Address = "101 Bree St, CPT",  Rent = 16500, Status = "Active" },
                    new() { Id = 3, Title = "Garden Cottage",     Address = "42 Maple Ave, PTA",Rent =  9500, Status = "Paused" }
                },
                New = new ListingEditVm()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddListing(ListingEditVm model)
        {
            if (!ModelState.IsValid)
            {
                var vmBad = new ListingsVm { Items = new(), New = model };
                return View("Listings", vmBad);
            }

            // Save uploaded image if present
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var ext = Path.GetExtension(model.ImageFile.FileName).ToLowerInvariant();

                if (!allowed.Contains(ext))
                {
                    ModelState.AddModelError("New.ImageFile", "Only JPG, PNG, GIF or WEBP files are allowed.");
                    var vmErr = new ListingsVm { Items = new(), New = model };
                    return View("Listings", vmErr);
                }

                var folderPhysical = Path.Combine(_env.WebRootPath, "images", "properties");
                Directory.CreateDirectory(folderPhysical);

                var fileName = $"{Guid.NewGuid()}{ext}";
                var filePhysical = Path.Combine(folderPhysical, fileName);

                using (var stream = new FileStream(filePhysical, FileMode.Create))
                {
                    model.ImageFile.CopyTo(stream);
                }

                // Relative path you can store on your real entity later
                model.ImageUrl = $"/images/properties/{fileName}";
            }

            // TODO: persist the listing (Title, Address, Rent, ImageUrl, etc.) to the database
            TempData["Success"] = $"Listing “{model.Title}” added{(string.IsNullOrWhiteSpace(model.ImageUrl) ? "" : " with image")}.";

            return RedirectToAction(nameof(Listings));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleStatus(int id, string currentStatus)
        {
            // TODO: flip status of the DB record
            var newStatus = currentStatus == "Active" ? "Paused" : "Active";
            TempData["Success"] = $"Listing #{id} is now {newStatus}.";
            return RedirectToAction(nameof(Listings));
        }

        // ---------------- Manage Leases ----------------
        [HttpGet]
        public IActionResult Leases()
        {
            var items = new List<LeaseRowVm>
            {
                new() { Id=501, Tenant="A. Daniels", Property="Sea View Apartment", Ends=DateTime.UtcNow.AddMonths(2),  Status="Active" },
                new() { Id=502, Tenant="K. Mbatha",  Property="City Loft",          Ends=DateTime.UtcNow.AddDays(15),  Status="Notice Given" },
                new() { Id=503, Tenant="J. Smith",   Property="Garden Cottage",     Ends=DateTime.UtcNow.AddMonths(7), Status="Active" },
            };
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ApproveRenewal(int id)
        {
            // TODO: update lease record
            TempData["Success"] = $"Lease #{id}: renewal approved.";
            return RedirectToAction(nameof(Leases));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Terminate(int id)
        {
            // TODO: update lease record
            TempData["Success"] = $"Lease #{id}: terminated.";
            return RedirectToAction(nameof(Leases));
        }

        // ---------------- Respond to AI Escalations ----------------
        [HttpGet]
        public IActionResult AIEscalations()
        {
            var list = new List<AiEscalationVm>
            {
                new() { Id=9001, Summary="High water usage anomaly – Unit 4B", SuggestedAction="Check for silent leak", Priority="High" },
                new() { Id=9002, Summary="Repeated late rent – Tenant K.M.",  SuggestedAction="Send gentle reminder", Priority="Med" },
                new() { Id=9003, Summary="Elevator incident spikes",          SuggestedAction="Schedule inspection", Priority="Med" },
            };
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ApplyAiAction(int id)
        {
            // TODO: create a ticket / task
            TempData["Success"] = $"AI suggestion for item #{id} applied.";
            return RedirectToAction(nameof(AIEscalations));
        }

        // ---------------- Send Reminders ----------------
        [HttpGet]
        public IActionResult Reminders() => View(new ReminderVm());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reminders(ReminderVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            // TODO: actually send/schedule reminder
            TempData["Success"] = $"Reminder sent to: {vm.Target}.";
            return RedirectToAction(nameof(Reminders));
        }
    }

    // ---------------- ViewModels ----------------
    public class ManagerDashboardVm
    {
        public int ActiveListings { get; set; }
        public int PendingApplications { get; set; }
        public int LeasesExpiringThisMonth { get; set; }
        public int OverdueReminders { get; set; }
        public int OpenMaintenanceTickets { get; set; }
    }

    public class ListingsVm
    {
        public List<ListingItemVm> Items { get; set; } = new();
        public ListingEditVm New { get; set; } = new();
    }

    public class ListingItemVm
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Address { get; set; } = "";
        public decimal Rent { get; set; }
        public string Status { get; set; } = "Active"; // Active/Paused
        // Optional: ImageUrl if you want to preview in the table later
        public string? ImageUrl { get; set; }
    }

    public class ListingEditVm
    {
        [Required, StringLength(80)]
        public string Title { get; set; } = "";

        [Required, StringLength(120)]
        public string Address { get; set; } = "";

        [Range(0, 100000)]
        public decimal Rent { get; set; }

        [Display(Name = "Add Image")]
        public IFormFile? ImageFile { get; set; }

        // Populated when a file is saved
        public string? ImageUrl { get; set; }
    }

    public class LeaseRowVm
    {
        public int Id { get; set; }
        public string Tenant { get; set; } = "";
        public string Property { get; set; } = "";
        public DateTime Ends { get; set; }
        public string Status { get; set; } = "Active";
    }

    public class AiEscalationVm
    {
        public int Id { get; set; }
        public string Summary { get; set; } = "";
        public string SuggestedAction { get; set; } = "";
        public string Priority { get; set; } = "Low"; // Low/Med/High
    }

    public class ReminderVm
    {
        [Required] public string Target { get; set; } = "All Tenants";
        [Required, StringLength(120)] public string Subject { get; set; } = "";
        [Required, StringLength(1500)] public string Message { get; set; } = "";
        public DateTime? ScheduleFor { get; set; }
    }
}
