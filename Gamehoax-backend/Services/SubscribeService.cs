using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamehoax_backend.Services
{
    public class SubscribeService : ISubscribeService
    {
        private readonly AppDbContext _context;
        public SubscribeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Subscribe>> GetAllAsync()
        {
            return await _context.Subscribes.ToListAsync();
        }

        public async Task<Subscribe> GetByIdAsync(int id)
        {
            return await _context.Subscribes.FirstOrDefaultAsync(m=>m.Id== id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Subscribes.CountAsync();
        }

        public async Task<List<Subscribe>> GetPaginatedDatasAsync(int page, int take)
        {
            return await _context.Subscribes.Skip((page - 1) * take).Take(take).ToListAsync();
        }
    }
}
