using System.ComponentModel.DataAnnotations;

namespace LeaseLink.Models
{
    public class TenantProfile
    {
        public int Id { get; set; }
        [Required] public string UserId { get; set; } = ""; // Identity user link
        [Required, MaxLength(80)] public string FullName { get; set; } = "";
        [MaxLength(120)] public string? Phone { get; set; }
    }

    public class RentalApplication
    {
        public int Id { get; set; }
        [Required, MaxLength(80)] public string FullName { get; set; } = "";
        [Required, EmailAddress] public string Email { get; set; } = "";
        [MaxLength(120)] public string? Phone { get; set; }
        [Required] public int UnitId { get; set; }
        public Unit Unit { get; set; } = default!;
        [MaxLength(500)] public string? Notes { get; set; }
        [MaxLength(30)] public string Status { get; set; } = "Submitted"; // Submitted/Approved/Rejected
    }
}
