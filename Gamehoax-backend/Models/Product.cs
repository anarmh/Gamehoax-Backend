namespace Gamehoax_backend.Models
{
    public class Product:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int SKU { get; set; }
        public int RateCount { get; set; }
        public string Feature { get; set; }
        public decimal Weight { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<WishlistProduct> WishlistProducts { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }
        public int BrandModelId { get; set; }
        public BrandModel BrandModel { get; set; }
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }
        public int RatingId { get; set; }
        public Rating Rating { get; set; }
       
    }
}
