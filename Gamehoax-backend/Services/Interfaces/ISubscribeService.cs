using Gamehoax_backend.Models;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface ISubscribeService
    {
        Task<List<Subscribe>> GetAllAsync();
        Task<Subscribe> GetByIdAsync(int id);
        Task<List<Subscribe>> GetPaginatedDatasAsync(int page, int take);
        Task<int> GetCountAsync();
    }
}
