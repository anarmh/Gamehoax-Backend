using Gamehoax_backend.Models;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAllAsync();
    }
}
