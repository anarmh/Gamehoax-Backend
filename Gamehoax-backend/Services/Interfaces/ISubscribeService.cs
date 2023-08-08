using Gamehoax_backend.Models;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface ISubscribeService
    {
        Task<List<Subscribe>> GetAllAsync();
    }
}
