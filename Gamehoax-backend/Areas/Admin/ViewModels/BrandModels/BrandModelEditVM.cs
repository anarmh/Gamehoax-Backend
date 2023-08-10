using Microsoft.Build.Framework;

namespace Gamehoax_backend.Areas.Admin.ViewModels.BrandModels
{
    public class BrandModelEditVM
    {
        [Required]
        public int BrandId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
