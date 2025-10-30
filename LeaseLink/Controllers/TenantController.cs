using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LeaseLink.Controllers
{
    [Authorize(Roles = "Tenant")]
    public class TenantController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View(); // dashboard buttons

        // ---------- Landing ----------
        [HttpGet]
        public IActionResult Landing()
        {
            var model = new TenantLandingVm
            {
                WelcomeMessage = "Welcome back to your LeaseLink tenant portal!",
                CurrentLease = "Sea View Apartment, Cape Town",
                NextPaymentDue = DateTime.UtcNow.AddDays(5),
                OutstandingBalance = 14500,
                Announcements = new()
                {
                    new() { Date = DateTime.UtcNow.AddDays(-1), Title = "Water outage (Wed 10:00–12:00)" },
                    new() { Date = DateTime.UtcNow.AddDays(-3), Title = "Lobby paint refresh completed" }
                },
                OpenTickets = new()
                {
                    new() { Id = 1207, Summary = "Kitchen tap leak", Status = "In Progress" },
                    new() { Id = 1215, Summary = "Garage remote intermittent", Status = "Open" }
                },
                RecentPayments = new()
                {
                    new() { Date = DateTime.UtcNow.AddMonths(-1), Amount = 14500, Method="Card" },
                    new() { Date = DateTime.UtcNow.AddMonths(-2), Amount = 14500, Method="EFT" }
                }
            };
            return View(model);
        }

        // ---------- Maintenance ----------
        [HttpGet]
        public IActionResult Maintenance() => View(new MaintenanceRequestVm());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Maintenance(MaintenanceRequestVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            // TODO: persist; optional file save to /wwwroot/images/maintenance
            TempData["Success"] = $"Request submitted for {vm.Category}. We’ll get back to you soon!";
            return RedirectToAction(nameof(Maintenance));
        }

        // ---------- Payment ----------
        [HttpGet]
        public IActionResult Payment()
        {
            var vm = new PaymentVm
            {
                AmountDue = 14500,
                DueDate = DateTime.UtcNow.AddDays(5),
                TenantName = "John Tenant"
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Payment(PaymentVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            // TODO: integrate payment
            TempData["Success"] = $"Payment of R{vm.AmountPaid:N2} received. Thank you!";
            return RedirectToAction(nameof(Payment));
        }

        // ---------- Settings ----------
        [HttpGet]
        public IActionResult Settings()
        {
            var model = new TenantSettingsVm
            {
                Name = "John Tenant",
                Email = "tenant@leaselink.com",
                Phone = "081 234 5678",
                NotificationsEnabled = true
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Settings(TenantSettingsVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            // TODO: save to DB
            TempData["Success"] = "Profile settings updated successfully!";
            return RedirectToAction(nameof(Settings));
        }
    }

    // ---------- Landing models ----------
    public class TenantAnnouncementVm
    {
        public DateTime Date { get; set; }
        public string Title { get; set; } = "";
        public string? Body { get; set; }
    }

    public class TenantTicketVm
    {
        public int Id { get; set; }
        public string Summary { get; set; } = "";
        public string Status { get; set; } = "Open";
    }

    public class TenantPaymentRowVm
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; } = "EFT";
    }

    public class TenantLandingVm
    {
        public string WelcomeMessage { get; set; } = "";
        public string CurrentLease { get; set; } = "";
        public DateTime NextPaymentDue { get; set; }
        public decimal OutstandingBalance { get; set; }
        public List<TenantAnnouncementVm> Announcements { get; set; } = new();
        public List<TenantTicketVm> OpenTickets { get; set; } = new();
        public List<TenantPaymentRowVm> RecentPayments { get; set; } = new();
    }

    // ---------- Feature models ----------
    public class MaintenanceRequestVm
    {
        [Required] public string Category { get; set; } = "";
        [Required] public string Description { get; set; } = "";
        [Display(Name = "Upload Image (optional)")]
        public IFormFile? Photo { get; set; }
    }

    public class PaymentVm
    {
        public string TenantName { get; set; } = "";
        public DateTime DueDate { get; set; }
        public decimal AmountDue { get; set; }

        [Range(1, 100000)]
        [Display(Name = "Amount Paying")]
        public decimal AmountPaid { get; set; }
    }

    public class TenantSettingsVm
    {
        [Required] public string Name { get; set; } = "";
        [Required, EmailAddress] public string Email { get; set; } = "";
        public string? Phone { get; set; }
        public bool NotificationsEnabled { get; set; }
    }
}
