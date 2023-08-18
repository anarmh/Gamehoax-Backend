using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gamehoax_backend.Services
{
    public class UserService:IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        public UserService(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task DeleteAsync(AppUser user)
        {
            await _userManager.DeleteAsync(user);
        }

        public async Task<List<AppUser>> GetAllAsync()
        {
            return await _userManager.Users.Include(m=>m.Carts).Include(m=>m.Wishlists).Include(m=>m.Reviews).ToListAsync();
        }

        public async Task<AppUser> GetByIdAsync(string userId)
        {
            var data =  await _userManager.Users.Include(m=>m.Carts).Include(m=>m.Wishlists).Include(m=>m.Reviews).FirstOrDefaultAsync(m=>m.Id==userId);
            return data;
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<List<AppUser>> GetPaginatedDatasAsync(int page, int take)
        {
            return await _context.Users.Include(m=>m.Carts).Include(m=>m.Wishlists).Include(m=>m.Reviews).Skip((page * take) - take).Take(take).ToListAsync();

        }
    }
}
