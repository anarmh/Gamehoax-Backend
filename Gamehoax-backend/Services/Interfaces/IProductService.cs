using Gamehoax_backend.Helpers;
using Gamehoax_backend.Models;
using Gamehoax_backend.Viewmodel;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetPaginateDatasAsync(int page,int take,string sortValue);
        List<ProductVM> GetMappedDatas(List<Product> products);
        Task<Product> GetAllDataById(int? id);
        Task<int> GetCountAsync();
        Task<int> GetProductsCountBySearchTextAsync(string searchText);
        Task<int> GetProductsCountBySortTextAsync(string sortValue);
        Task<int> GetProductsCountByRangeAsync(int? value1, int? value2);
        Task<int> GetProductsCountByCategoryAsync(int? id);
        Task<int> GetProductsCountByTagAsync(int? id);
        Task<List<ProductVM>> GetProductsByCategoryIdAsync(int? id, int page = 1, int take = 3);
        Task<List<ProductVM>> GetProductsByTagIdAsync(int? id, int page = 1, int take = 3);
    }
}
