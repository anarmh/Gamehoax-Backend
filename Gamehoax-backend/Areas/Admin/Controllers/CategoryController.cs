using Gamehoax_backend.Areas.Admin.ViewModels.Categories;
using Gamehoax_backend.Data;
using Gamehoax_backend.Helpers;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Gamehoax_backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM category)
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return View(category);
                }

                if (!category.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(category);
                }

                if (!category.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(category);

                }

                string fileName = Guid.NewGuid().ToString() + " " + category.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/category", fileName);
                await FileHelper.SaveFileAsync(newPath, category.Photo);

                Category newCategory = new()
                {
                    Image = fileName,
                    Name = category.Name,
                    Title= category.Title,
                    Description= category.Description,
                };
                await _context.Categories.AddAsync(newCategory);
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
                Category category = await _categoryService.GetCategoryById(id);
                if (category == null) return NotFound();
                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/category", category.Image);
                FileHelper.DeleteFile(path);
                _context.Categories.Remove(category);
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
                Category dbCategory = await _categoryService.GetCategoryById(id);
                if (dbCategory == null) return NotFound();

                CategoryEditVM model = new()
                {
                    Image = dbCategory.Image,
                    Name = dbCategory.Name,
                    Title= dbCategory.Title,
                    Description= dbCategory.Description,
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
        public async Task<IActionResult> Edit(int? id, CategoryEditVM category)
        {
            try
            {
                if (id == null) return BadRequest();
                Category dbCategory = await _categoryService.GetCategoryById(id);
                if (dbCategory == null) return NotFound();

                CategoryEditVM model = new()
                {
                    Image = dbCategory.Image,
                    Name = dbCategory.Name, 
                    Title= dbCategory.Title,
                    Description= dbCategory.Description,
                };
                if (category.Photo != null)
                {
                    if (!category.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!category.Photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/category", dbCategory.Image);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + category.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/category", fileName);
                    await FileHelper.SaveFileAsync(newPath, category.Photo);
                    dbCategory.Image = fileName;
                }
                else
                {
                    Category newCategory = new()
                    {
                        Image = dbCategory.Image,

                    };
                }

                dbCategory.Name = category.Name;
                dbCategory.Title = category.Title;
                dbCategory.Description = category.Description;

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
