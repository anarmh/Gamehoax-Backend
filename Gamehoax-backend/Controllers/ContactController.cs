using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Viewmodel;
using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostComment(ContactVM model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            Contact contact = new()
            {
                Subject = model.Subject,
                Name = model.Name,
                Email = model.Email,
                Message = model.Message,
            };
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
