using Microsoft.Build.Framework;

namespace Gamehoax_backend.Areas.Admin.ViewModels.BrandModels
{
    public class BrandModelCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int BrandId { get; set; }
    }
}
