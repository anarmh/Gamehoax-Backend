using System.ComponentModel.DataAnnotations;

namespace Gamehoax_backend.Areas.Admin.ViewModels.Discounts
{
    public class DiscountCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public byte Percent { get; set; }
    }
}
