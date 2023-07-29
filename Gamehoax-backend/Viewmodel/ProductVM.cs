using Gamehoax_backend.Models;

namespace Gamehoax_backend.Viewmodel
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal Percent { get; set; }
        public int Rating { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }

    }
}
