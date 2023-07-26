using Microsoft.EntityFrameworkCore;

namespace Gamehoax_backend.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
