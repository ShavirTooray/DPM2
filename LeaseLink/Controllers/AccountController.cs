using System.Security.Claims;
using LeaseLink.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LeaseLink.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> um, SignInManager<IdentityUser> sm)
        {
            _userManager = um;
            _signInManager = sm;
        }

        // ---------- Login ----------
        [HttpGet, AllowAnonymous]
        public IActionResult Login() => View(new LoginViewModel());

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm, string? returnUrl = null)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = await _userManager.FindByEmailAsync(vm.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(vm);
            }

            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(vm);
            }

            // Ensure role is assigned (in case of a new user)
            if (!await _userManager.IsInRoleAsync(user, vm.Role))
            {
                // You can remove this “auto-fix” if you don’t want it:
                await _userManager.AddToRoleAsync(user, vm.Role);
            }

            // Redirect by selected role
            return vm.Role switch
            {
                "Admin" => RedirectToAction("Index", "Admin"),
                "Manager" => RedirectToAction("Index", "Manager"),
                "HigherManagement" => RedirectToAction("Index", "HigherManagement"),
                "Tenant" => RedirectToAction("Index", "Tenant"),
                _ => RedirectToAction("Index", "Home")
            };
        }

        // ---------- Register ----------
        [HttpGet, AllowAnonymous]
        public IActionResult Register() => View(new RegisterViewModel());

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var existing = await _userManager.FindByEmailAsync(vm.Email);
            if (existing != null)
            {
                ModelState.AddModelError(nameof(vm.Email), "Email already registered.");
                return View(vm);
            }

            var user = new IdentityUser { UserName = vm.Email, Email = vm.Email, EmailConfirmed = true };
            var create = await _userManager.CreateAsync(user, vm.Password);
            if (!create.Succeeded)
            {
                foreach (var e in create.Errors) ModelState.AddModelError(string.Empty, e.Description);
                return View(vm);
            }

            // Save name as a claim (optional)
            if (!string.IsNullOrWhiteSpace(vm.FullName))
            {
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, vm.FullName));
            }

            // Default Tenant if unknown
            var role = string.IsNullOrWhiteSpace(vm.Role) ? "Tenant" : vm.Role;
            await _userManager.AddToRoleAsync(user, role);

            await _signInManager.SignInAsync(user, isPersistent: false);

            // Redirect by chosen role
            return role switch
            {
                "Admin" => RedirectToAction("Index", "Admin"),
                "Manager" => RedirectToAction("Index", "Manager"),
                "HigherManagement" => RedirectToAction("Index", "HigherManagement"),
                "Tenant" => RedirectToAction("Index", "Tenant"),
                _ => RedirectToAction("Index", "Home")
            };
        }

        // ---------- Logout ----------
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // Hard-coded forgot password landing (no functionality)
        [HttpGet, AllowAnonymous]
        public IActionResult ForgotPassword() => View();
    }
}
