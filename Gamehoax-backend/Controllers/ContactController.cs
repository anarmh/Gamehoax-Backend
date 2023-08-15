using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Gamehoax_backend.Viewmodel;
using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IServiceIconService _service;
        public ContactController(AppDbContext context,IServiceIconService service)
        {
            _context = context;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ServiceIcon> serviceIcons=await _service.GetAllAsync();

            ContactVM model = new()
            {
                ServiceIcons = serviceIcons
            };
            return View(model);
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
