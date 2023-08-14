using Gamehoax_backend.Areas.Admin.ViewModels.Products;
using Gamehoax_backend.Data;
using Gamehoax_backend.Helpers;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Gamehoax_backend.Viewmodel;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Gamehoax_backend.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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

        public async Task<Product> GetAllDataById(int? id)
        {

            return await _context.Products.Include(m => m.ProductImages).
                                            Include(m => m.BrandModel).
                                            ThenInclude(m=>m.Brand).
                                            Include(m => m.Discount).
                                            Include(m => m.Rating).
                                            Include(m => m.Reviews).
                                            Include(m => m.ProductCategories).
                                            ThenInclude(m => m.Category).
                                            Include(m => m.ProductTags).
                                            ThenInclude(m => m.Tag).
                                            Include(m => m.CartProducts).
                                            ThenInclude(m => m.Cart).
                                            Include(m => m.WishlistProducts).
                                            ThenInclude(m => m.Wishlist).
                                            FirstOrDefaultAsync(m=>m.Id==id);
        }

        public async Task<int> GetCountAsync() => await _context.Products.CountAsync();
        

        public List<ProductVM> GetMappedDatas(List<Product> products)
        {
           
            List<ProductVM> mappedDatas = new();
            foreach (var product in products)
            {
                
                ProductVM productList = new()
                {
                    Id = product.Id,
                    Title = product.Title,
                    Price = product.Price,
                    ProductImages = product.ProductImages.ToList(),
                    Rating = product.Rating.RatingCount,
                   
                  
                };
                mappedDatas.Add(productList);
            }
            return mappedDatas;
        }

        public async Task<List<Product>> GetPaginateDatasAsync(int page, int take,string sortValue, string searchText,int? categoryId, int? tagId,int? value1,int? value2)
        {
            List<Product> products= await _context.Products.
                Include(m=>m.ProductImages).
                Include(m=>m.Rating).
                Include(m=>m.Discount).
                Include(m=>m.BrandModel).
                Include(m=>m.ProductCategories).
                ThenInclude(m=>m.Category).
                Include(m=>m.ProductTags).
                ThenInclude(m=>m.Tag).
                Skip((page-1)*take).
                Take(take).ToListAsync();




            if (sortValue != null)
            {
                if (sortValue == "1")
                {
                    products = await _context.Products.Include(m => m.ProductImages).Include(m => m.Rating).Skip((page - 1) * take).Take(take).ToListAsync();
                }
                if (sortValue == "2")
                {
                    products = await _context.Products.Include(m => m.ProductImages).Include(m => m.Rating).OrderByDescending(m => m.CreateDate).Skip((page - 1) * take).Take(take).ToListAsync();
                }
                if (sortValue == "3")
                {
                    products = await _context.Products.Include(m => m.ProductImages).Include(m => m.Rating).OrderByDescending(m => m.Rating.RatingCount).Skip((page - 1) * take).Take(take).ToListAsync();
                }
                if (sortValue == "4")
                {
                    products = await _context.Products.Include(m => m.ProductImages).Include(m => m.Rating).OrderBy(m => m.Price).Skip((page - 1) * take).Take(take).ToListAsync();
                }
                if (sortValue == "5")
                {
                    products = await _context.Products.Include(m => m.ProductImages).Include(m => m.Rating).OrderByDescending(m => m.Price).Skip((page - 1) * take).Take(take).ToListAsync();
                }
            }

            if (searchText != null)
            {
                products = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Rating)
                .Include(p => p.Discount)
                .OrderByDescending(p => p.Id)
                .Where(p => p.Title.ToLower().Trim().Contains(searchText.ToLower().Trim()))
                .Skip((page-1) * take)
                .Take(take)
                .ToListAsync();
            }

            if (categoryId != null)
            {
                products = await _context.ProductCategories
                 .Include(p => p.Product)
                 .ThenInclude(p => p.ProductImages)
                 .Include(p=>p.Product.Rating)
                 .Where(p => p.Category.Id == categoryId)
                 .Select(pc => pc.Product)
                 .Skip((page -1) * take)
                 .Take(take)
                 .ToListAsync();
            }


            if (tagId != null)
            {
                products = await _context.ProductTags
                
                .Include(p => p.Product)
                .ThenInclude(p => p.ProductImages)
                .Include(p => p.Product.Rating)
                .Where(p => p.Tag.Id == tagId)
                .Select(pc => pc.Product)
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();
            }

            if (value1 != null && value2 != null)
            {
                products = await _context.Products
               .Include(p => p.ProductImages)
               .Include(p => p.ProductTags)
               .ThenInclude(p => p.Tag)
               .Include(p => p.ProductCategories)
               .ThenInclude(p => p.Category)
               .Include(p => p.Rating)
               .Where(p => p.Price >= value1 && p.Price <= value2)
               .Skip((page-1) * take)
               .Take(take)
               .ToListAsync();

            }


            return products;

        }

        public async Task<List<ProductVM>> GetProductsByCategoryIdAsync(int? id, int page = 1, int take = 9)
        {
            List<ProductVM> model = new();
            List<Product> products = await _context.ProductCategories.Include(p => p.Product)
                                                                     .ThenInclude(p => p.ProductImages)
                                                                     .Include (p => p.Product.Rating)
                                                                     .Where(pc => pc.Category.Id == id)
                                                                     .Select(pc => pc.Product)
                                                                     .Skip((page-1) * take)
                                                                     .Take(take)
                                                                     .ToListAsync();
            foreach (var product in products)
            {
                model.Add(new ProductVM
                {
                    Id = product.Id,
                    Price = product.Price,
                    Title = product.Title,
                    ProductImages = product.ProductImages,
                    Rating = product.Rating.RatingCount,
                   
                });
            }
            return model;
        }

        public async Task<List<ProductVM>> GetProductsByTagIdAsync(int? id, int page = 1, int take = 9)
        {

            List<ProductVM> model = new();
            List<Product> products = await _context.ProductTags.Include(p => p.Product)
                                                                     .ThenInclude(p => p.ProductImages)
                                                                     .Include(p => p.Product.Rating)
                                                                     .Where(pc => pc.Tag.Id == id)
                                                                     .Select(pc => pc.Product)
                                                                     .Skip((page -1) * take)
                                                                     .Take(take)
                                                                     .ToListAsync();
            foreach (var product in products)
            {
                model.Add(new ProductVM
                {
                    Id = product.Id,
                    Price = product.Price,
                    Title = product.Title,
                    ProductImages = product.ProductImages,
                    Rating = product.Rating.RatingCount,

                });
            }
            return model;
        }

        public async Task<int> GetProductsCountByCategoryAsync(int? id)
        {
            return await _context.ProductCategories
                 .Include(p => p.Product)
                 .Where(pc => pc.Category.Id == id)
                 .Select(pc => pc.Product)
                 .CountAsync();
        }

        public async Task<int> GetProductsCountByRangeAsync(int? value1, int? value2)
        {
            return await _context.Products.Where(p => p.Price >= value1 && p.Price <= value2)
                                .Include(p => p.ProductImages)
                                .Include(p => p.Rating)
                                .Include(p => p.Discount)
                                .CountAsync();
        }

        public async Task<int> GetProductsCountBySearchTextAsync(string searchText)
        {
            return await _context.Products.Where(p => p.Title.ToLower().Trim().Contains(searchText.ToLower().Trim()))
                                .Include(p => p.ProductImages)
                                .Include(p=>p.Rating.RatingCount)
                                .CountAsync();
        }

        public async Task<int> GetProductsCountBySortTextAsync(string sortValue)
        {

            int count = 0;
            if (sortValue == "1")
            {
                return await _context.Products.Include(m => m.ProductImages).Include(p=>p.Rating).CountAsync();
            };
            if (sortValue == "2")
            {
                count = await _context.Products.Include(m => m.ProductImages).OrderByDescending(p => p.Popularity).CountAsync();

            };
            if (sortValue == "3")
            {
                count = await _context.Products.Include(m => m.ProductImages).Include(m=>m.Rating).OrderByDescending(p => p.Rating.RatingCount).CountAsync();

            };
            if (sortValue == "4")
            {
                count = await _context.Products.Include(m => m.ProductImages).Include(m=>m.Rating).OrderBy(p => p.Price).CountAsync();

            };
            if (sortValue == "5")
            {
                count = await _context.Products.Include(m => m.ProductImages).Include(m=>m.Rating).OrderByDescending(p => p.Price).CountAsync();

            };
           

            return count;
        }

        public async Task<int> GetProductsCountByTagAsync(int? id)
        {

            return await _context.ProductTags
                 .Include(p => p.Product)
                 .Where(pc => pc.Tag.Id == id)
                .Select(pc => pc.Product)
                 .CountAsync();
        }

        public async Task<List<Product>> GetAllBySearchText(string searchText,int page=1, int take = 9)
        {
            var products = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Rating)
                .OrderByDescending(p => p.Id)
                .Where(p => p.Title.ToLower().Trim().Contains(searchText.ToLower().Trim()))
                .Skip((page-1)*take)
                .Take(take)
                .ToListAsync();

            var ratingCounts = products.Select(p => p.Rating.RatingCount).ToList();

            return products;
        }

        public async Task<Product> GetByIdAsync(int? id)
        {
            return await _context.Products.Include(m => m.ProductImages)
                                          .Include(p => p.Rating)
                                          .Include(p => p.BrandModel)
                                          .Include(p => p.Discount)
                                          .Include(p => p.ProductCategories)
                                          .ThenInclude(p=>p.Category)
                                          .Include(p => p.ProductTags)
                                          .ThenInclude(p=>p.Tag)
                                          .Include(p => p.Reviews)
                                          .FirstOrDefaultAsync(m => m.Id == id);
        }

       
    }
}
