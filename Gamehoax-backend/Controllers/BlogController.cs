using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
