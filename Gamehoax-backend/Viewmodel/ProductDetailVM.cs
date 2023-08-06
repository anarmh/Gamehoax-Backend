using Gamehoax_backend.Models;

namespace Gamehoax_backend.Viewmodel
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal ActualPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public int RateCount { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<WishlistProduct> WishlistProducts { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }
        public int SKU { get; set; }
        public string ModelName { get; set; }
        public string BrandName { get; set; }
        public byte Percent { get; set; }
        public string Feature { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public ReviewVM ReviewVM { get; set; }
    }
}
