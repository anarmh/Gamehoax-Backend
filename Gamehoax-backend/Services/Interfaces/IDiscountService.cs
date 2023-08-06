using Gamehoax_backend.Models;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<List<Discount>> GetAllAsync();
    }
}
