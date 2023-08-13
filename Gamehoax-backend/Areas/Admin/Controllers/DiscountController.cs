using Gamehoax_backend.Areas.Admin.ViewModels.Discounts;
using Gamehoax_backend.Areas.Admin.ViewModels.Tags;
using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DiscountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService discountService, AppDbContext context)
        {
            _discountService = discountService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Discount> discounts=await _discountService.GetAllAsync();
            return View(discounts);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Discount discount = await _discountService.GetByIdAsync((int)id);
                if (discount == null) return NotFound();
                return View(discount);
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
        public async Task<IActionResult> Create(DiscountCreateVM discount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(discount);
                }

                Discount newDiscount= new()
                {

                    Name = discount.Name,
                    Percent= discount.Percent,
                };
                await _context.Discounts.AddAsync(newDiscount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

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
                Discount dbDiscount = await _discountService.GetByIdAsync((int)id);
                if (dbDiscount == null) return NotFound();

                _context.Discounts.Remove(dbDiscount);
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
                Discount dbDiscount = await _discountService.GetByIdAsync((int)id);
                if (dbDiscount == null) return NotFound();

                DiscountEditVM model = new()
                {
                    Percent= dbDiscount.Percent,
                    Name = dbDiscount.Name,
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
        public async Task<IActionResult> Edit(int? id, DiscountEditVM discount)
        {
            try
            {
                if (id == null) return BadRequest();
                Discount dbDiscount = await _discountService.GetByIdAsync((int)id);
                if (dbDiscount == null) return NotFound();



                dbDiscount.Name = discount.Name;
                dbDiscount.Percent = discount.Percent;
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
