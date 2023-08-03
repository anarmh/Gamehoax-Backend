using Gamehoax_backend.Models;
using Gamehoax_backend.Services;
using Gamehoax_backend.Services.Interfaces;
using Gamehoax_backend.Viewmodel.Cart;
using Gamehoax_backend.Viewmodel.Wishlist;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gamehoax_backend.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IWishlistService _wishlistService;
        private readonly IProductService _productService;
        public WishlistController(IWishlistService wishlistService, IProductService productService, IHttpContextAccessor accessor)
        {
            _wishlistService = wishlistService;
            _productService = productService;
            _accessor = accessor;
        }

        public  async Task<IActionResult> Index()
        {

            List<WishlistDetailVM> wishlistList = new();

            if (_accessor.HttpContext.Request.Cookies["wishlist"] != null)
            {

                List<WishlistVM> wishlistDatas = JsonConvert.DeserializeObject<List<WishlistVM>>(_accessor.HttpContext.Request.Cookies["wishlist"]);

                foreach (var item in wishlistDatas)
                {
                    var dbProduct = await _productService.GetByIdAsync(item.ProductId);

                    if (dbProduct != null)
                    {
                        WishlistDetailVM wishlistDetail = new()
                        {
                            Id = dbProduct.Id,
                            Name = dbProduct.Name,
                            Image = dbProduct.ProductImages.Where(m => m.IsMain).FirstOrDefault().Image,
                            Count = item.Count,
                            Price = dbProduct.Price,
                        };
                        wishlistList.Add(wishlistDetail);
                    }

                }

            }
            return View(wishlistList);
        }





        [HttpPost]
        public async Task<IActionResult> AddToWishlist(int? id)
        {

            if (id is null) return BadRequest();

            Product product = await _productService.GetByIdAsync(id);
            if (product is null) return NotFound();

            List<WishlistVM> basket = _wishlistService.GetDatasFromCookie();

            _wishlistService.AddProduct(basket, product);

            return Ok(basket.Sum(m => m.Count));

        }


        [HttpPost]
        public IActionResult DeleteDataFromWishlist(int? id)
        {
            if (id is null) return BadRequest();

            _wishlistService.DeleteData((int)id);
            List<WishlistVM> baskets = _wishlistService.GetDatasFromCookie();
            return Ok(baskets.Count);
        }
    }
}
