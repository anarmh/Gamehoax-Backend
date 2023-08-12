using Gamehoax_backend.Areas.Admin.ViewModels.ServiceIcons;
using Gamehoax_backend.Areas.Admin.ViewModels.Tags;
using Gamehoax_backend.Data;
using Gamehoax_backend.Helpers;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IServiceIconService _serviceIconService;
        
        public ServiceController(AppDbContext context, IServiceIconService serviceIconService)
        {
            _context = context;
            _serviceIconService = serviceIconService;
            
        }
        public async Task<IActionResult> Index()
        {
            List<ServiceIcon> serviceIcons= await _serviceIconService.GetAllAsync();
            return View(serviceIcons);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            try
            {
                if (id == null) return BadRequest();
                ServiceIcon dbServiceIcon = await _serviceIconService.GetByIdAsync((int)id);
                if (dbServiceIcon == null) return NotFound();
                return View(dbServiceIcon);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                ServiceIcon dbServiceIcon = await _serviceIconService.GetByIdAsync((int)id);
                if (dbServiceIcon == null) return NotFound();

                _context.ServiceIcons.Remove(dbServiceIcon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceIconCreateVM service)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(service);
                }

                ServiceIcon newService = new()
                {

                    Icon = service.Icon,
                    Title= service.Title,
                    Description= service.Description,
                };
                await _context.ServiceIcons.AddAsync(newService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }

        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                ServiceIcon dbService = await _serviceIconService.GetByIdAsync((int)id);
                if (dbService == null) return NotFound();

                ServiceIconEditVM model = new()
                {

                    Icon = dbService.Icon,
                    Title= dbService.Title,
                    Description= dbService.Description,
                };
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ServiceIconEditVM service)
        {
            try
            {
                if (id == null) return BadRequest();
                ServiceIcon dbService = await _serviceIconService.GetByIdAsync((int)id);
                if (dbService == null) return NotFound();



                dbService.Icon = service.Icon;
                dbService.Title = service.Title;
                dbService.Description = service.Description;
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
