using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamehoax_backend.Services
{
    public class ServiceIconService : IServiceIconService
    {
        private readonly AppDbContext _context;
        public ServiceIconService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ServiceIcon>> GetAllAsync()
        {
            return await _context.ServiceIcons.ToListAsync();
        }
    }
}
