using Gamehoax_backend.Areas.Admin.ViewModels.Categories;
using Gamehoax_backend.Areas.Admin.ViewModels.Sliders;
using Gamehoax_backend.Data;
using Gamehoax_backend.Helpers;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gamehoax_backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ISliderService _sliderService;

        public SliderController(IWebHostEnvironment env, ISliderService sliderService,AppDbContext context)
        {
            _env = env;
            _sliderService = sliderService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders= await _sliderService.GetAllAsync();
            return View(sliders);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Slider dbSlider = await _sliderService.GetByIdAsync((int)id);

            if (dbSlider is null) return NotFound();
            return View(dbSlider);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM slider)
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return View(slider);
                }

                if (!slider.Image.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Image", "File type must be image");
                    return View(slider);
                }

                if (!slider.Image.CheckFileSize(2000))
                {
                    ModelState.AddModelError("Image", "Image size must be max 2000kb");
                    return View(slider);

                }

                string fileName = Guid.NewGuid().ToString() + " " + slider.Image.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/slider", fileName);
                await FileHelper.SaveFileAsync(newPath, slider.Image);

                Slider newSlider = new()
                {
                    Image = fileName,
                 
                };
                await _context.Sliders.AddAsync(newSlider);
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
                Slider slider = await _sliderService.GetByIdAsync((int)id);
                if (slider == null) return NotFound();
                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/slider", slider.Image);
                FileHelper.DeleteFile(path);
                _context.Sliders.Remove(slider);
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
                Slider dbSlider = await _sliderService.GetByIdAsync((int)id);
                if (dbSlider == null) return NotFound();

                SliderEditVM model = new()
                {
                    Image = dbSlider.Image,
                   
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
        public async Task<IActionResult> Edit(int? id, SliderEditVM slider)
        {
            try
            {
                if (id == null) return BadRequest();
                Slider dbSlider = await _sliderService.GetByIdAsync((int)id);
                if (dbSlider == null) return NotFound();

                SliderEditVM model = new()
                {
                    Image = dbSlider.Image,
                   
                };
                if (slider.NewImage != null)
                {
                    if (!slider.NewImage.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("NewIamge", "File type must be image");
                        return View(model);
                    }
                    if (!slider.NewImage.CheckFileSize(2000))
                    {
                        ModelState.AddModelError("NewIamge", "Image size must be max 2000kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/slider", dbSlider.Image);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + slider.NewImage.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/slider", fileName);
                    await FileHelper.SaveFileAsync(newPath, slider.NewImage);
                    dbSlider.Image = fileName;
                }
                else
                {
                    Slider newSlider = new()
                    {
                        Image = dbSlider.Image
                    };
                }


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
        public async Task<IActionResult> ChangeStatus(int? id)
        {
            if (id == null) return BadRequest();

            Slider slider = await _sliderService.GetByIdAsync((int)id);

            if (slider is null) return NotFound();

            return Ok(await _sliderService.ChangeStatusAsync(slider));
        }
    }
}
