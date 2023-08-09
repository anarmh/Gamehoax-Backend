using Gamehoax_backend.Models;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<AppUser>> GetAllAsync();
        Task<AppUser> GetByIdAsync(string userId);
        Task DeleteAsync(AppUser user);
        Task<int> GetCountAsync();


        Task<List<AppUser>> GetPaginatedDatasAsync(int page, int take);
    }
}
