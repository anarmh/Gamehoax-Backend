using Gamehoax_backend.Areas.Admin.ViewModels.BrandModels;
using Gamehoax_backend.Areas.Admin.ViewModels.Tags;
using Gamehoax_backend.Data;
using Gamehoax_backend.Helpers;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gamehoax_backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandModelController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IBrandModelService _brandModelService;
        private readonly IBrandService _brandService;
        public BrandModelController(IBrandModelService brandModelService, AppDbContext context, IBrandService brandService)
        {
            _brandModelService = brandModelService;
            _context = context;
            _brandService = brandService;
        }
        public async Task<IActionResult> Index()
        {
            List<BrandModel> brandModels= await _brandModelService.GetAllAsync();
            return View(brandModels);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                BrandModel brandModel = await _brandModelService.GetByIdAsync((int)id);
                if (brandModel == null) return NotFound();
                return View(brandModel);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }



        private async Task<SelectList> GetBrandsAsync()
        {
            List<Brand> brands = await _brandService.GetAllAsync();
            return new SelectList(brands, "Id", "Name");
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.brands=await GetBrandsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandModelCreateVM model)
        {

            try
            {
                ViewBag.brands = await GetBrandsAsync();
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                BrandModel newBrandModel = new()
                {

                    Name = model.Name,
                    BrandId= model.BrandId,

                };
                await _context.BrandModels.AddAsync(newBrandModel);
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
                BrandModel dbBrandModel = await _brandModelService.GetByIdAsync((int)id);
                if (dbBrandModel == null) return NotFound();

                _context.BrandModels.Remove(dbBrandModel);
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
                ViewBag.brands = await GetBrandsAsync();
                if (id == null) return BadRequest();
                BrandModel dbBrandModel = await _brandModelService.GetByIdAsync((int)id);
                if (dbBrandModel == null) return NotFound();

                BrandModelEditVM model = new()
                {
                    BrandId= dbBrandModel.BrandId,
                    Name = dbBrandModel.Name,
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
        public async Task<IActionResult> Edit(int? id, BrandModelEditVM request)
        {
            try
            {
                if (id == null) return BadRequest();
                BrandModel dbBrandModel = await _brandModelService.GetByIdAsync((int)id);
                if (dbBrandModel == null) return NotFound();

              

                dbBrandModel.Name = request.Name;
                dbBrandModel.BrandId = request.BrandId;
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
