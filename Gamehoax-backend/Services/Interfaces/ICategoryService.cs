using Gamehoax_backend.Models;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
    }
}
