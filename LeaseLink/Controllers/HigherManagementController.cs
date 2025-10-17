using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeaseLink.Controllers
{
    [Authorize(Roles = "HigherManagement")]
    public class HigherManagementController : Controller
    {
        public IActionResult Index() => View();
    }
}
