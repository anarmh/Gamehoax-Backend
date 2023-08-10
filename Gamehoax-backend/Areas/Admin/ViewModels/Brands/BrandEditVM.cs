using System.ComponentModel.DataAnnotations;

namespace Gamehoax_backend.Areas.Admin.ViewModels.Brands
{
    public class BrandEditVM
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
        public string Image { get; set; }
    }
}

