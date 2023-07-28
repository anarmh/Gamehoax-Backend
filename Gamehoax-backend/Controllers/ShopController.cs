using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Gamehoax_backend.Viewmodel;
using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        public ShopController(IProductService productService, ICategoryService categoryService, ITagService tagService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        public async Task<IActionResult> Index(int page=1, int take=3, string sortValue=null, string searchText=null, int? categoryId=null, int? tagId=null,int? value1=null,int? value2=null)
        {
            List<Product> products= await _productService.GetAllAsync();
            List<Category> categories= await _categoryService.GetAllAsync();
            List<Tag> tags= await _tagService.GetAllAsync();
            
            ShopVM model = new()
            {
                Products= products,
                Categories= categories,
                Tags= tags
            };
            return View(model);
        }
    }
}
