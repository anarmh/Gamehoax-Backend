using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
