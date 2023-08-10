using Gamehoax_backend.Models;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface ITagService
    {
        Task<List<Tag>> GetAllAsync();
        Task<Tag> GetByIdAsync(int id);
    }
}
