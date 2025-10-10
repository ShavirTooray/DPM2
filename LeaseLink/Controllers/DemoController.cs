using LeaseLink.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LeaseLink.Controllers
{
    [AllowAnonymous]
    public class DemoController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signIn;
        private readonly UserManager<ApplicationUser> _users;

        public DemoController(SignInManager<ApplicationUser> signIn, UserManager<ApplicationUser> users)
        {
            _signIn = signIn;
            _users = users;
        }

        // /Demo/LoginAs?role=Executive|Admin|Manager|Tenant
        public async Task<IActionResult> LoginAs(string role, string? returnUrl = null)
        {
            string email = role?.ToLowerInvariant() switch
            {
                "executive" => "exec@leaselink.local",
                "admin" => "admin@leaselink.local",
                "manager" => "manager@leaselink.local",
                "tenant" => "tenant@leaselink.local",
                _ => "tenant@leaselink.local"
            };

            var user = await _users.FindByEmailAsync(email);
            if (user is null) return NotFound("Seed users not created yet.");

            await _signIn.SignOutAsync();
            var result = await _signIn.PasswordSignInAsync(user, "P@ssw0rd!", isPersistent: true, lockoutOnFailure: false);
            if (!result.Succeeded) return BadRequest("Sign-in failed.");

            return Redirect(returnUrl ?? Url.Action("Index", "Home")!);
        }
    }
}
