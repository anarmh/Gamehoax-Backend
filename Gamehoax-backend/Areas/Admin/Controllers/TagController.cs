using Gamehoax_backend.Areas.Admin.ViewModels.Brands;
using Gamehoax_backend.Areas.Admin.ViewModels.Tags;
using Gamehoax_backend.Data;
using Gamehoax_backend.Helpers;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gamehoax_backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ITagService _tagService;
        public TagController(ITagService tagService,AppDbContext context)
        {
            _tagService = tagService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Tag> tags= await _tagService.GetAllAsync();
            return View(tags);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Tag tag = await _tagService.GetByIdAsync((int)id);
                if (tag == null) return NotFound();
                return View(tag);
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
        public async Task<IActionResult> Create(TagCreateVM tag)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(tag);
                }

                Tag newTag = new()
                {
                  
                    Name = tag.Name,

                };
                await _context.Tags.AddAsync(newTag);
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
                Tag dbTag = await _tagService.GetByIdAsync((int)id);
                if (dbTag == null) return NotFound();
              
                _context.Tags.Remove(dbTag);
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
                Tag dbTag = await _tagService.GetByIdAsync((int)id);
                if (dbTag == null) return NotFound();

                TagEditVM model = new()
                {
                    
                    Name = dbTag.Name,
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
        public async Task<IActionResult> Edit(int? id, TagEditVM tag)
        {
            try
            {
                if (id == null) return BadRequest();
                Tag dbTag = await _tagService.GetByIdAsync((int)id);
                if (dbTag == null) return NotFound();

               

                dbTag.Name = tag.Name;
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
