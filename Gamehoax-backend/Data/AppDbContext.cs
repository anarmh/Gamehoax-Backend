using Gamehoax_backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gamehoax_backend.Data
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<ServiceIcon> ServiceIcons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandModel> BrandModels { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistProduct> WishlistProducts{ get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Blog> Blogs { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Rating>()
            .HasMany(e => e.Reviews)
            .WithOne(e => e.Rating)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Slider>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<ServiceIcon>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Product>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Category>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Brand>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Tag>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Discount>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<ProductImage>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Rating>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Wishlist>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Cart>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Subscribe>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<BrandModel>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Testimonial>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Review>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Blog>().HasQueryFilter(m => !m.SoftDelete);

        }
    }
}
