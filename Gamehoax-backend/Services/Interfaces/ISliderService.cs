using Gamehoax_backend.Areas.Admin.ViewModels.Sliders;
using Gamehoax_backend.Models;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<Slider>> GetAllAsync();
        Task<Slider> GetByIdAsync(int id);
        Task<int> GetCountAsync();
        Task<bool> ChangeStatusAsync(Slider slider);
    }
}
