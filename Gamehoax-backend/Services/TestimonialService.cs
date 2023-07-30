using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamehoax_backend.Services
{
    public class TestimonialService : ITestimonialService
    {
        private readonly AppDbContext _context;
        public TestimonialService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Testimonial>> GetAllAsync()
        {
            return await _context.Testimonials.ToListAsync();
        }
    }
}
