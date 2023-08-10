using Gamehoax_backend.Models;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface IBrandModelService
    {
        Task<List<BrandModel>> GetAllAsync();
        Task<BrandModel> GetByIdAsync(int id);
    }
}
