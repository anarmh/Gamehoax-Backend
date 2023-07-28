using Gamehoax_backend.Helpers;
using Gamehoax_backend.Models;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetPaginateDatasAsync(int page,int take, string sortValue,string searchText,int? categoryId,int? tagId,int? value1,int? value2);
    }
}
