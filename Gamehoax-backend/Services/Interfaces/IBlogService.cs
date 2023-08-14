using Gamehoax_backend.Models;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(int id);
        Task<List<Blog>> GetPaginateDatasAsync(int page, int take, string searchtext);
        Task<int> GetBlogsCountBySearchTextAsync(string searchText);
        Task<int> GetCountAsync();
        Task<List<Blog>> GetAllBySearchText(string searchText, int page = 1, int take = 5);
    }
}
