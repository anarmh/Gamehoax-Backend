using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamehoax_backend.Services
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _context;
        public ReviewService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews.Include(m=>m.Product).Include(m=>m.Rating).Include(m=>m.AppUser).ToListAsync();
        }

        public async Task<Review> GetByIdAsync(int id)
        {
           return await _context.Reviews.Include(m => m.Product).Include(m => m.Rating).Include(m=>m.AppUser).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<int> GetCountAsync()
        {
           return await _context.Reviews.CountAsync();
        }

        public async Task<List<Review>> GetPaginatedDatasAsync(int page, int take)
        {
           return await _context.Reviews.Include(m=>m.AppUser).Include(m=>m.Rating).Include(m=>m.Product).Skip((page-1)*take).Take(take).ToListAsync();
        }
    }
}
