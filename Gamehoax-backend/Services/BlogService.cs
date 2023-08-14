using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamehoax_backend.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;

        public BlogService(AppDbContext context)
        {
           _context= context;
        }

        public async Task<List<Blog>> GetAllAsync()
        {
            return await _context.Blogs.ToListAsync();
        }

        public async Task<List<Blog>> GetAllBySearchText(string searchText, int page = 1, int take = 5)
        {
            return await _context.Blogs.OrderByDescending(m=>m.Id)
                                       .Where(p => p.Title.ToLower().Trim().Contains(searchText.ToLower().Trim()))
                                       .Skip((page - 1) * take)
                                       .Take(take)
                                       .ToListAsync();
        }

        public async Task<int> GetBlogsCountBySearchTextAsync(string searchText)
        {
            return await _context.Blogs.Where(m => m.Title.ToLower().Trim().Contains(searchText.ToLower().Trim())).CountAsync();
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
           return await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<int> GetCountAsync()=> await _context.Blogs.CountAsync();
       

        public async Task<List<Blog>> GetPaginateDatasAsync(int page, int take, string searchtext)
        {
            List<Blog> blogs=await _context.Blogs.Skip((page-1)*take).Take(take).ToListAsync();

            if(searchtext!=null)
            {
                blogs=await _context.Blogs.OrderByDescending(m=>m.Id).
                                           Where(m=>m.Title.ToLower().Trim().Contains(searchtext.ToLower().Trim())).
                                           Skip((page - 1) * take).
                                           Take(take).ToListAsync();
            }
            return blogs;
        }
    }
}
