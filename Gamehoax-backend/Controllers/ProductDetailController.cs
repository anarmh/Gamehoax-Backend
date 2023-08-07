using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Gamehoax_backend.Viewmodel;
using Gamehoax_backend.Viewmodel.Wishlist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

namespace Gamehoax_backend.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<AppUser> _userManager;

        public ProductDetailController(IProductService productService, IHttpContextAccessor accessor,AppDbContext context,UserManager<AppUser> userManager)
        {
            _productService = productService;
            _accessor = accessor;
            _context=context;
            _userManager=userManager;
        }

        public async Task<IActionResult> Index(int? id)
        {

            if (_accessor.HttpContext.Request.Cookies["wishlist"] != null)
            {
                var myCookie = JsonConvert.DeserializeObject<List<WishlistVM>>(_accessor.HttpContext.Request.Cookies["wishlist"]);
                ViewBag.cookie = myCookie;
            }
            try
            {
                if (id is null) return BadRequest();

                Product product= await _productService.GetAllDataById(id);

                if (product == null) return NotFound();

                List<Product> products= await _productService.GetAllAsync();

                ProductDetailVM model = new()
                {
                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    ActualPrice=product.Price,
                    DiscountPrice=product.Price-(product.Price*product.Discount.Percent)/100,
                    ProductCategories = product.ProductCategories.ToList(),
                    ProductTags = product.ProductTags.ToList(),
                    RateCount=product.Rating.RatingCount,
                    Weight=product.Weight,
                    Images=product.ProductImages.Where(m=>m.IsMain).ToList(),
                    Percent=product.Discount.Percent,
                    BrandName=product.BrandModel.Brand.Name,
                    ModelName=product.BrandModel.Name,
                    Feature=product.Feature,
                    Name=product.Name,
                    Reviews=product.Reviews,
                    Products=products,
                };

                return View(model);
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.Message;
                return View();
            }
         
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> PostComment(ProductDetailVM model,int? id,int? ratingId,string userId)
        {
            if (id == null || userId == null) return BadRequest();
            if (!ModelState.IsValid) return RedirectToAction(nameof(Index), new { id,ratingId});

            Review review = new()
            {
                Describe = model.ReviewVM.Describe,
                AppUserId= userId,
                ProductId=(int)id,
                RatingId=(int)ratingId,
            };
            

            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id,ratingId});
        }*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> PostComment(ReviewVM vm)
        {

            if (!ModelState.IsValid) return RedirectToAction(nameof(Index));

            Review review = new()
            {
                Describe = vm.Describe,
                AppUserId = vm.UserId,
                ProductId = vm.Id,
                RatingId = vm.RatingId
            };


            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = review.ProductId });
        }
    }
}
