using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Controllers
{
    public class WishlistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
