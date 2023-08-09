using Gamehoax_backend.Areas.Admin.ViewModels.Categories;
using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamehoax_backend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<CategoryVM>> GetAllMappedDatasAsync()
        {
            List<CategoryVM> categoryList = new();
            List<Category> categories= await _context.Categories.ToListAsync();

            foreach (var category in categories)
            {
                categoryList.Add(new CategoryVM()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Image = category.Image,
                    Description= category.Description,
                    Title= category.Title,
                });
            }
            return categoryList;
        }

      

        public async Task<Category> GetCategoryById(int? id)
        {
            return await _context.Categories.Include(m => m.ProductCategories).FirstOrDefaultAsync(m=>m.Id==id);
        }

        public CategoryDetailVM GetMappedData(Category category)
        {

            CategoryDetailVM model = new()
            {
                Name = category.Name,
                Image = category.Image,
                Title= category.Title,
                Description = category.Description,
                CreateDate = category.CreateDate.ToString("dd-MM-yyyy"),
            };
            return model;
        }
    }
}
