using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamehoax_backend.Services
{
    public class BrandModelService : IBrandModelService
    {
        private readonly AppDbContext _context;

        public BrandModelService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BrandModel>> GetAllAsync()
        {
            return await _context.BrandModels.Include(m=>m.Brand).ToListAsync();
        }

        public async Task<BrandModel> GetByIdAsync(int id)
        {
            return await _context.BrandModels.Include(m => m.Brand).FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
