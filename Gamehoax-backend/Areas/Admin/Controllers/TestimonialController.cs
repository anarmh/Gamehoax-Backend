using Gamehoax_backend.Areas.Admin.ViewModels.Categories;
using Gamehoax_backend.Areas.Admin.ViewModels.Testimonials;
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
    public class TestimonialController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ITestimonialService _testimonialService;
        public TestimonialController(AppDbContext context, IWebHostEnvironment env, ITestimonialService testimonialService)
        {
            _context = context;
            _env = env;
            _testimonialService = testimonialService;
        }
        public async Task<IActionResult> Index()
        {
            List<Testimonial> testimonials=await _testimonialService.GetAllAsync();
            return View(testimonials);
        }

        public async Task<IActionResult> Detail(int? id)
        {

            try
            {
                if (id == null) return BadRequest();
                Testimonial dbTestimonial = await _testimonialService.GetByIdAsync((int)id);
                if (dbTestimonial == null) return NotFound();
                return View(dbTestimonial);
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
        public async Task<IActionResult> Create(TestimonialCreateVM testimonial)
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return View(testimonial);
                }

                if (!testimonial.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(testimonial);
                }

                if (!testimonial.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(testimonial);

                }

                string fileName = Guid.NewGuid().ToString() + " " + testimonial.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/testmonial", fileName);
                await FileHelper.SaveFileAsync(newPath, testimonial.Photo);

                Testimonial newTestimonial = new()
                {
                    Image = fileName,
                    Name = testimonial.Name,
                    Position = testimonial.Position,
                    Description = testimonial.Description,
                };
                await _context.Testimonials.AddAsync(newTestimonial);
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
                Testimonial testimonial = await _testimonialService.GetByIdAsync((int)id);
                if (testimonial == null) return NotFound();
                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/testmonial", testimonial.Image);
                FileHelper.DeleteFile(path);
                _context.Testimonials.Remove(testimonial);
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
                Testimonial dbTestimonial = await _testimonialService.GetByIdAsync((int)id);
                if (dbTestimonial == null) return NotFound();

                TestimonialEditVM model = new()
                {
                    Image = dbTestimonial.Image,
                    Name = dbTestimonial.Name,
                    Position = dbTestimonial.Position,
                    Description = dbTestimonial.Description,
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
        public async Task<IActionResult> Edit(int? id, TestimonialEditVM testimonial)
        {
            try
            {
                if (id == null) return BadRequest();
                Testimonial dbTestimonial = await _testimonialService.GetByIdAsync((int)id);
                if (dbTestimonial == null) return NotFound();

                TestimonialEditVM model = new()
                {
                    Image = dbTestimonial.Image,
                    Name = dbTestimonial.Name,
                    Position = dbTestimonial.Position,
                    Description = dbTestimonial.Description,
                };
                if (testimonial.Photo != null)
                {
                    if (!testimonial.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!testimonial.Photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/testmonial", dbTestimonial.Image);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + testimonial.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/testmonial", fileName);
                    await FileHelper.SaveFileAsync(newPath, testimonial.Photo);
                    dbTestimonial.Image = fileName;
                }
                else
                {
                    Testimonial newTestimonial = new()
                    {
                        Image = dbTestimonial.Image,

                    };
                }

                dbTestimonial.Name = testimonial.Name;
                dbTestimonial.Position =    testimonial.Position;
                dbTestimonial.Description = testimonial.Description;

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
