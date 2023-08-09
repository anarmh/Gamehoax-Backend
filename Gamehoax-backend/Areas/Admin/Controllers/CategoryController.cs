using Gamehoax_backend.Areas.Admin.ViewModels.Categories;
using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ICategoryService _categoryService;
        public CategoryController(AppDbContext context, IWebHostEnvironment env, ICategoryService categoryService)
        {
            _context = context;
            _env = env;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            List<CategoryVM> categoryVMs= await _categoryService.GetAllMappedDatasAsync();
            return View(categoryVMs);
        }

        public async Task<IActionResult> Detail(int? id)
        {

            try
            {
                if (id == null) return BadRequest();
                Category dbCategory = await _categoryService.GetCategoryById(id);
                if (dbCategory == null) return NotFound();
                return View(_categoryService.GetMappedData(dbCategory));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
    }
}
