using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeaseLink.Controllers
{
    [Authorize(Roles = "Tenant")]
    public class TenantController : Controller
    {
        public IActionResult Index() => View();
    }
}
