using Gamehoax_backend.Areas.Admin.ViewModels.Categories;
using Gamehoax_backend.Models;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> GetCategoryById(int? id);
        Task<List<CategoryVM>> GetAllMappedDatasAsync();
        CategoryDetailVM GetMappedData(Category category);
        Task<int> GetCountAsync();
    }
}
