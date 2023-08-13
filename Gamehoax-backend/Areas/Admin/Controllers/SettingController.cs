using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISettingService _settingService;

        public SettingController(AppDbContext context, ISettingService settingService)
        {
            _context = context;
            _settingService = settingService;
        }

        public async Task<IActionResult> Index()
        {
            List<Setting> settings =await  _settingService.GetAllAsync();
            return View(settings);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                Setting dbSetting = await _settingService.GetByIdAsync((int)id);
                Setting model = new()
                {
                    Value = dbSetting.Value,
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
        public async Task<IActionResult> Edit(int? id, Setting updatedSetting)
        {
            try
            {
                if (id == null) return BadRequest();
                Setting dbSetting = await _settingService.GetByIdAsync((int)id);
                if (dbSetting is null) return NotFound();

                Setting model = new()
                {
                    Value = dbSetting.Value,
                };

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                dbSetting.Value = updatedSetting.Value;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                @ViewBag.error = ex.Message;
                return View();
            }
        }
    }
}
