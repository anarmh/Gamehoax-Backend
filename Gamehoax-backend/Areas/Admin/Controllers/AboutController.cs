using Gamehoax_backend.Areas.Admin.ViewModels.Abouts;
using Gamehoax_backend.Areas.Admin.ViewModels.Categories;
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
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAboutService _aboutService;
        private readonly IWebHostEnvironment _env;
        public AboutController(AppDbContext context,IAboutService aboutService, IWebHostEnvironment env)
        {
            _context = context;
            _aboutService = aboutService;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<About> abouts= await _aboutService.GetAllAsync();
            return View(abouts);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            try
            {
                if (id == null) return BadRequest();
                About dbAbout = await _aboutService.GetByIdAsync((int)id);
                if (dbAbout == null) return NotFound();
                return View(dbAbout);
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
        public async Task<IActionResult> Create(AboutCreateVM about)
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return View(about);
                }

                if (!about.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(about);
                }

                if (!about.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(about);

                }

                string fileName = Guid.NewGuid().ToString() + " " + about.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/blog", fileName);
                await FileHelper.SaveFileAsync(newPath, about.Photo);

                About newAbout = new()
                {
                    Image = fileName,
                    Icon = about.Icon,
                    Title = about.Title,
                    Description = about.Description,
                };
                await _context.Abouts.AddAsync(newAbout);
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
                About about = await _aboutService.GetByIdAsync((int)id);
                if (about == null) return NotFound();
                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/blog", about.Image);
                FileHelper.DeleteFile(path);
                _context.Abouts.Remove(about);
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
                About dbAbout = await _aboutService.GetByIdAsync((int)id);
                if (dbAbout == null) return NotFound();

                AboutEditVM model = new()
                {
                    Image = dbAbout.Image,
                    Icon = dbAbout.Icon,
                    Title = dbAbout.Title,
                    Description = dbAbout.Description,
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
        public async Task<IActionResult> Edit(int? id, AboutEditVM about)
        {
            try
            {
                if (id == null) return BadRequest();
                About dbAbout = await _aboutService.GetByIdAsync((int)id);
                if (dbAbout == null) return NotFound();

                AboutEditVM model = new()
                {
                    Image = dbAbout.Image,
                    Icon = dbAbout.Icon,
                    Title = dbAbout.Title,
                    Description = dbAbout.Description,
                };
                if (about.Photo != null)
                {
                    if (!about.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!about.Photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/blog", dbAbout.Image);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + about.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/blog", fileName);
                    await FileHelper.SaveFileAsync(newPath, about.Photo);
                    dbAbout.Image = fileName;
                }
                else
                {
                    About newAbout = new()
                    {
                        Image = dbAbout.Image,

                    };
                }

                dbAbout.Icon = about.Icon;
                dbAbout.Title = about.Title;
                dbAbout.Description = about.Description;

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
