using System.ComponentModel.DataAnnotations;

namespace LeaseLink.Models;

public class TenantApplication
{
    public int Id { get; set; }

    [Required]
    public int PropertyId { get; set; }

    [Required, StringLength(120)]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, Phone]
    public string Phone { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Message { get; set; }

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

    public Property? Property { get; set; }
}
