using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamehoax_backend.Services
{
    public class AboutService : IAboutService
    {
        private readonly AppDbContext _context;
        public AboutService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<About>> GetAllAsync()
        {
            return await _context.Abouts.ToListAsync();
        }

        public async Task<About> GetByIdAsync(int id)
        {
          return await _context.Abouts.FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
