using System.ComponentModel.DataAnnotations;

namespace Gamehoax_backend.Areas.Admin.ViewModels.Products
{
    public class ProductCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Feature { get; set; }
        [Required]
        public string Weight { get; set; }
        [Required]
        public int SKU { get; set; }
        [Required]
        public int Popularity { get; set; }
        [Required]
        public int DiscountId { get; set; }
        [Required]
        public int RatingId { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public int BrandModelId { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public List<int> CategoryIds { get; set; } = new();
        [Required(ErrorMessage = "Don`t be empty")]
        public List<int> TagIds { get; set; } = new();
        [Required(ErrorMessage = "Don`t be empty")]
        public List<IFormFile> Photos { get; set; }
    }
}
