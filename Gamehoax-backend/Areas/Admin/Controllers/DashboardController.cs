using Gamehoax_backend.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {

            return View();
        } 
    }
}
