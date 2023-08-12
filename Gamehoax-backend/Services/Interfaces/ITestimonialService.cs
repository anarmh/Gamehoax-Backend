using Gamehoax_backend.Models;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface ITestimonialService
    {
        Task<List<Testimonial>> GetAllAsync();
        Task<Testimonial> GetByIdAsync(int id);
    }
}
