using Gamehoax_backend.Models;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface IRatingService
    {
        Task<List<Rating>> GetAllAsync();
    }
}
