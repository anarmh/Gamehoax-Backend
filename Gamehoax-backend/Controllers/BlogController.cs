using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Gamehoax_backend.Viewmodel;
using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        public BlogController(IBlogService blogService, ICategoryService categoryService, ITagService tagService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        public async Task<IActionResult> Index()
        {
            List<Blog> blogs= await _blogService.GetAllAsync();
            List<Category> categories= await _categoryService.GetAllAsync();
            List<Tag> tags= await _tagService.GetAllAsync();

            BlogVM model = new()
            {
                Blogs = blogs,
                Categories = categories,
                Tags = tags
            };
            return View(model);
        }
    }
}
