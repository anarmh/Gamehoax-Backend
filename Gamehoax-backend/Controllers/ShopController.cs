using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
