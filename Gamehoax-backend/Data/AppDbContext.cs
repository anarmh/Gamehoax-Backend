using Gamehoax_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Gamehoax_backend.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<ServiceIcon> ServiceIcons { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Slider>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<ServiceIcon>().HasQueryFilter(m => !m.SoftDelete);
        }
    }
}
