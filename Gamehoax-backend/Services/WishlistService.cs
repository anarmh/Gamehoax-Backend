using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Gamehoax_backend.Viewmodel.Cart;
using Gamehoax_backend.Viewmodel.Wishlist;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Gamehoax_backend.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly IProductService _productService;
        public WishlistService(IProductService productService, IHttpContextAccessor accessor,AppDbContext context)
        {
            _productService = productService;
            _accessor = accessor;
            _context = context;
        }
        public void AddProduct(List<WishlistVM> wishlist, Product product)
        {
            WishlistVM existProduct = wishlist.FirstOrDefault(m => m.ProductId == product.Id);

            if (existProduct == null)
            {
                wishlist.Add(new WishlistVM()
                {
                    ProductId = product.Id,
                    Count = 1
                });
            }
            else
            {
                existProduct.Count++;
            }
            _accessor.HttpContext.Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlist));
        }



        public List<WishlistVM> GetDatasFromCookie()
        {
            List<WishlistVM> wishlists;

            if (_accessor.HttpContext.Request.Cookies["wishlist"] != null)
            {
                wishlists = JsonConvert.DeserializeObject<List<WishlistVM>>(_accessor.HttpContext.Request.Cookies["wishlist"]);
            }
            else
            {
                wishlists = new List<WishlistVM>();
            }
            return wishlists;

        }


        public void DeleteData(int? id)
        {
            var wishlists = JsonConvert.DeserializeObject<List<WishlistVM>>(_accessor.HttpContext.Request.Cookies["wishlist"]);
            var deletedProduct = wishlists.FirstOrDefault(b => b.ProductId == id);
            wishlists.Remove(deletedProduct);
            _accessor.HttpContext.Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlists));

        }

        public int GetCount()
        {
            List<WishlistVM> wishlist;

            if (_accessor.HttpContext.Request.Cookies["wishlist"] != null)
            {
                wishlist = JsonConvert.DeserializeObject<List<WishlistVM>>(_accessor.HttpContext.Request.Cookies["wishlist"]);
            }
            else
            {
                wishlist = new List<WishlistVM>();
            }

            return wishlist.Sum(m => m.Count);
        }


        public async Task<Wishlist> GetByUserIdAsync(string userId)
        {
            var data = await _context.Wishlists.Include(c => c.WishlistProducts).FirstOrDefaultAsync(c => c.AppUserId == userId);
            return data;
        }

        public async Task<List<WishlistProduct>> GetAllByWishlistIdAsync(int? wishlistId)
        {
            return await _context.WishlistProducts.Where(c => c.WishlistId == wishlistId).ToListAsync();
        }
    }
}
