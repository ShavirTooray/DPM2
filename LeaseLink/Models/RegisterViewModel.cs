using System.ComponentModel.DataAnnotations;

namespace LeaseLink.Models
{
    public class RegisterViewModel
    {
        // Optional display name (we’ll store as a claim)
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }

        [Required] public string Role { get; set; } = "Tenant";

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), Display(Name = "Confirm Password"),
         Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
