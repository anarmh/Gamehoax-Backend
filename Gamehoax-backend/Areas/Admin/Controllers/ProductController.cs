using Gamehoax_backend.Areas.Admin.ViewModels.Products;
using Gamehoax_backend.Data;
using Gamehoax_backend.Helpers;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gamehoax_backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IProductService _productService;
        private readonly IBrandModelService _brandModelService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly IDiscountService _discountService;
       
        public ProductController(AppDbContext context,
                                 IWebHostEnvironment env,
                                 IProductService productService,
                                 IBrandModelService brandModelService,
                                 ICategoryService categoryService,
                                 ITagService tagService,
                                 IDiscountService discountService
                               
                               )
        {
            _context = context;
            _env = env;
            _productService = productService;
            _brandModelService = brandModelService;
            _categoryService = categoryService;
            _tagService = tagService;
            _discountService = discountService;
            
        }
        public async Task<IActionResult> Index(int page=1,int take=5)
        {
            List<Product> datas = await _productService.GetPaginateDatasAsync(page,take,null,null,null,null,null,null);
            List<ProductListVM> mappedDatas= GetMappedDatas(datas);
            int pageCount = await GetPageCountAsync(take);
            ViewBag.take = take;
            Paginate<ProductListVM> paginatedDatas = new(mappedDatas, page, pageCount);
            return View(paginatedDatas);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            var productCount = await _productService.GetCountAsync();
            return (int)Math.Ceiling((decimal)productCount / take);
        }


        private List<ProductListVM> GetMappedDatas(List<Product> products)
        {
            List<ProductListVM> mappedDatas = new();
            foreach (var product in products)
            {
                var categories = product.ProductCategories.Select(pt => pt.Category).ToList();
                ProductListVM productList = new()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Image = product.ProductImages.FirstOrDefault(m=>m.IsMain).Image,
                    Weight=product.Weight,
                    Model=product.BrandModel.Name,
                    Categories= categories
                };
                mappedDatas.Add(productList);
            }
            return mappedDatas;
        }

        private async Task<SelectList> GetCategoriesAsync()
        {
            IEnumerable<Category> categories = await _categoryService.GetAllAsync();
            return new SelectList(categories, "Id", "Name");
        }
        private async Task<SelectList> GetTagsAsync()
        {
            IEnumerable<Tag> tags = await _tagService.GetAllAsync();
            return new SelectList(tags, "Id", "Name");
        }

        private async Task<SelectList> GetBrandModelsAsync()
        {
            List<BrandModel> brandModels = await _brandModelService.GetAllAsync();
            return new SelectList(brandModels, "Id", "Name");
        }
        private async Task<SelectList> GetDiscountsAsync()
        {
            List<Discount> discounts = await _discountService.GetAllAsync();
            return new SelectList(discounts, "Id", "Name");
        }
      

        [HttpGet]
        public async Task<IActionResult> Create() 
        {
            ViewBag.categories = await GetCategoriesAsync();
            ViewBag.tags = await GetTagsAsync();
            ViewBag.brandModels = await GetBrandModelsAsync();
            ViewBag.discounts = await GetDiscountsAsync();
            return View();
        }
    }
}
