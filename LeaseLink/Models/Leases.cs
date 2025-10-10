using System.ComponentModel.DataAnnotations;

namespace LeaseLink.Models
{
    public class Lease
    {
        public int Id { get; set; }
        [Required] public int UnitId { get; set; }
        public Unit Unit { get; set; } = default!;
        [Required] public int TenantProfileId { get; set; }
        public TenantProfile Tenant { get; set; } = default!;
        public DateOnly Start { get; set; }
        public DateOnly End { get; set; }
        public decimal Deposit { get; set; }
        public string Status { get; set; } = "Active"; // Active/Expired/Pending
    }

    public class Invoice
    {
        public int Id { get; set; }
        [Required] public int LeaseId { get; set; }
        public Lease Lease { get; set; } = default!;
        public DateOnly Period { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = "Unpaid"; // Unpaid/Paid/Overdue
        public DateOnly DueDate { get; set; }
        public string? PaymentProofPath { get; set; }
    }
}
