using Gamehoax_backend.Models;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<Slider>> GetAllAsync();
    }
}
