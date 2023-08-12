using Gamehoax_backend.Areas.Admin.ViewModels.Abouts;
using Gamehoax_backend.Areas.Admin.ViewModels.Blogs;
using Gamehoax_backend.Data;
using Gamehoax_backend.Helpers;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IBlogService _blogService;
        private readonly IWebHostEnvironment _env;
        public BlogController(AppDbContext context, IBlogService blogService, IWebHostEnvironment env)
        {
            _context = context;
            _blogService = blogService;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Blog> blogs= await _blogService.GetAllAsync();
            return View(blogs);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            try
            {
                if (id == null) return BadRequest();
                Blog dbBlog = await _blogService.GetByIdAsync((int)id);
                if (dbBlog == null) return NotFound();
                return View(dbBlog);
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
                Blog blog = await _blogService.GetByIdAsync((int)id);
                if (blog == null) return NotFound();
                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/blog", blog.Image);
                FileHelper.DeleteFile(path);
                _context.Blogs.Remove(blog);
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
        public async Task<IActionResult> Create(BlogCreateVM blog)
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return View(blog);
                }

                if (!blog.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(blog);
                }

                if (!blog.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(blog);

                }

                string fileName = Guid.NewGuid().ToString() + " " + blog.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/blog", fileName);
                await FileHelper.SaveFileAsync(newPath, blog.Photo);

                Blog newBlog = new()
                {
                    Image = fileName,
                    Icon = blog.Icon,
                    Title = blog.Title,
                    Description = blog.Description,
                };
                await _context.Blogs.AddAsync(newBlog);
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
                Blog dbBlog = await _blogService.GetByIdAsync((int)id);
                if (dbBlog == null) return NotFound();

                BlogEditVM model = new()
                {
                    Image = dbBlog.Image,
                    Icon = dbBlog.Icon,
                    Title = dbBlog.Title,
                    Description = dbBlog.Description,
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
        public async Task<IActionResult> Edit(int? id, BlogEditVM blog)
        {
            try
            {
                if (id == null) return BadRequest();
                Blog dbBlog = await _blogService.GetByIdAsync((int)id);
                if (dbBlog == null) return NotFound();

                BlogEditVM model = new()
                {
                    Image = dbBlog.Image,
                    Icon = dbBlog.Icon,
                    Title = dbBlog.Title,
                    Description = dbBlog.Description,
                };
                if (blog.Photo != null)
                {
                    if (!blog.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!blog.Photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/blog", dbBlog.Image);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + blog.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/blog", fileName);
                    await FileHelper.SaveFileAsync(newPath, blog.Photo);
                    dbBlog.Image = fileName;
                }
                else
                {
                    Blog newBlog = new()
                    {
                        Image = dbBlog.Image,

                    };
                }

                dbBlog.Icon = blog.Icon;
                dbBlog.Title = blog.Title;
                dbBlog.Description = blog.Description;

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
