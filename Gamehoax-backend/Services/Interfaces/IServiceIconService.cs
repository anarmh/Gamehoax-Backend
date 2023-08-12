using Gamehoax_backend.Models;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface IServiceIconService
    {
        Task<List<ServiceIcon>> GetAllAsync();
        Task<ServiceIcon> GetByIdAsync(int id);
    }
}
