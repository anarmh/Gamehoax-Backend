using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamehoax_backend.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly AppDbContext _context;
        public DiscountService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Discount>> GetAllAsync()
        {
            return await _context.Discounts.ToListAsync();
        }
    }
}
