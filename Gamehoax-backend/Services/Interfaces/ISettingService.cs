using Gamehoax_backend.Models;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface ISettingService
    {
        Dictionary<string, string> GetAll();
        Task<List<Setting>> GetAllAsync();
        Task<Setting> GetByIdAsync(int id);
    }
}
