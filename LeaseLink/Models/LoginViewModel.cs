using System.ComponentModel.DataAnnotations;

namespace LeaseLink.Models
{
    public class LoginViewModel
    {
        [Required] public string Role { get; set; } = "Admin";

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
