using System.ComponentModel.DataAnnotations;

namespace Gamehoax_backend.Areas.Admin.ViewModels.Abouts
{
    public class AboutEditVM
    {

        [Required]
        public string Icon { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        public string Image { get; set; }
    }
}
