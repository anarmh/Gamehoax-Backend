using Gamehoax_backend.Models;

namespace Gamehoax_backend.Areas.Admin.ViewModels.Products
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public int SKU { get; set; }
        public string Model { get; set; }
        public string DiscountName { get; set; }
        public int RatingCount { get; set; }
        public string Feature { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }
        public ICollection<ProductImage> Images { get; set; }
    }
}
