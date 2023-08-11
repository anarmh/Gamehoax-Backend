using Gamehoax_backend.Data;
using Gamehoax_backend.Helpers;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IContactService _contactService;
        public ContactController(AppDbContext context,IContactService contactService)
        {
            _context = context;
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            List<Contact> conTacts= await _contactService.GetAllAsync();
            return View(conTacts);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Contact dbcontact = await _contactService.GetByIdAsync((int)id);
                if (dbcontact is null) return NotFound();

                _context.Contacts.Remove(dbcontact);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
    }
}
