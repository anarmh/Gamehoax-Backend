using Gamehoax_backend.Models;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface IReviewService
    {
        Task<List<Review>> GetAllAsync();
        Task<Review> GetByIdAsync(int id);
        Task<List<Review>> GetPaginatedDatasAsync(int page, int take);
        Task<int> GetCountAsync();
    }
}
