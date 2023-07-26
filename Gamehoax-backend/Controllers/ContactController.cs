using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
