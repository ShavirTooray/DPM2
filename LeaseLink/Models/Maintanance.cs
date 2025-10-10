using System.ComponentModel.DataAnnotations;

namespace LeaseLink.Models
{
    public class MaintenanceRequest
    {
        public int Id { get; set; }
        [Required] public int TenantProfileId { get; set; }
        public TenantProfile Tenant { get; set; } = default!;
        [Required] public int UnitId { get; set; }
        public Unit Unit { get; set; } = default!;
        [Required, MaxLength(60)] public string Category { get; set; } = "General";
        [Required, MaxLength(30)] public string Urgency { get; set; } = "Normal";
        [Required, MaxLength(600)] public string Description { get; set; } = "";
        public string Status { get; set; } = "Open";
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    }
}
