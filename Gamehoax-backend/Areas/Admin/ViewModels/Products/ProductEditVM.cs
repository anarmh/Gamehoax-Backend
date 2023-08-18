using Gamehoax_backend.Models;
using System.ComponentModel.DataAnnotations;

namespace Gamehoax_backend.Areas.Admin.ViewModels.Products
{
    public class ProductEditVM
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
        [Required]
        public int SKU { get; set; }
        [Required]
        public int Popularity { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Price { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Weight { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Feature { get; set; }
        [Required]
        public List<IFormFile> Photos { get; set; }
       
        public List<ProductImage> Images { get; set; }
        public int BrandModelId { get; set; }
        public int DiscountId { get; set; }
        public int RatingId { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public List<int> CategoryIds { get; set; } = new();
        public ICollection<ProductTag> ProductTags { get; set; }
        public List<int> TagIds { get; set; } = new();

    }
}
