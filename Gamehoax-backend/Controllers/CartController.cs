using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
