using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamehoax_backend.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Include(m=>m.ProductImages).
                                            Include(m=>m.BrandModel).
                                            Include(m=>m.Discount).
                                            Include(m=>m.Rating).
                                            Include(m => m.Reviews).
                                            Include(m=>m.ProductCategories).
                                            ThenInclude(m=>m.Category).
                                            Include(m=>m.ProductTags).
                                            ThenInclude(m=>m.Tag).
                                            Include(m=>m.CartProducts).
                                            ThenInclude(m=>m.Cart).
                                            Include(m=>m.WishlistProducts).
                                            ThenInclude(m=>m.Wishlist).
                                            ToListAsync();
                                           

        }

        public async Task<List<Product>> GetPaginateDatasAsync(int page, int take, string sortValue, string searchText, int? categoryId, int? tagId, int? value1, int? value2)
        {
            List<Product> products= await _context.Products.
                Include(m=>m.ProductImages).
                Include(m=>m.Rating).
                Include(m=>m.Discount).
                Include(m=>m.ProductCategories).
                ThenInclude(m=>m.Category).
                Include(m=>m.ProductTags).
                ThenInclude(m=>m.Tag).
                Skip((page*take)-take).
                Take(take).ToListAsync();

            if (sortValue != null)
            {
                if (sortValue == "1")
                {
                    products= await _context.Products.Include(m=>m.ProductImages).Include(m=>m.Rating).Include(m=>m.Discount).Skip((page * take) - take).Take(take).ToListAsync();
                }
                if(sortValue == "2")
                {
                    products= await _context.Products.Include(m=>m.ProductImages).Include(m=>m.Rating).Include(m=>m.Discount).OrderByDescending(m=>m.Discount.Percent).Skip((page * take) - take).Take(take).ToListAsync();
                }
                if (sortValue == "3")
                {
                    products= await _context.Products.Include(m => m.ProductImages).Include(m => m.Rating).Include(m => m.Discount).OrderByDescending(m => m.Rating.RatingCount).Skip((page * take) - take).Take(take).ToListAsync();
                }
                if (sortValue == "4")
                {
                    products = await _context.Products.Include(m => m.ProductImages).Include(m => m.Rating).Include(m => m.Discount).OrderBy(m => m.Price).Skip((page * take) - take).Take(take).ToListAsync();
                }
                if (sortValue == "4")
                {
                    products = await _context.Products.Include(m => m.ProductImages).Include(m => m.Rating).Include(m => m.Discount).OrderByDescending(m => m.Price).Skip((page * take) - take).Take(take).ToListAsync();
                }
            }

            if (searchText != null)
            {
                products = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p=>p.Rating)
                .Include(p=>p.Discount)
                .OrderByDescending(p => p.Id)
                .Where(p => p.Title.ToLower().Trim().Contains(searchText.ToLower().Trim()))
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();
            }

            if (categoryId != null)
            {
                products = await _context.Categories
                 .Where(p => p.Id == categoryId)
                 .Include(p=>p.ProductCategories)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.ProductImages)
                .SelectMany(p => p.ProductCategories.Select(pc => pc.Product))
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();
            }


            if (tagId != null)
            {
                products = await _context.Tags
                 .Where(p => p.Id == tagId)
                 .Include(p => p.ProductTags)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.ProductImages)
                .SelectMany(p => p.ProductTags.Select(pc => pc.Product))
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();
            }

            if (value1 != null && value2 != null)
            {
                products = await _context.Products
               .Include(p => p.ProductImages)
               .Where(p => p.Price >= value1 && p.Price <= value2)
               .Skip((page * take) - take)
               .Take(take)
               .ToListAsync();

            }


            return products;

        }
    }
}
