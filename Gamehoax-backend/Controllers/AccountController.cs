using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignUp()
        {
            return View();
        }



        public IActionResult SignIn()
        {
            return View();
        }



        public IActionResult ForgotPassword()
        {
            return View();
        }

    }
}
