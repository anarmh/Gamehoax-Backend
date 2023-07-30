using Gamehoax_backend.Models;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface IAboutService
    {
        Task<List<About>> GetAllAsync();
    }
}
