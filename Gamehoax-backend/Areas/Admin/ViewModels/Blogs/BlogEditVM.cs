using System.ComponentModel.DataAnnotations;

namespace Gamehoax_backend.Areas.Admin.ViewModels.Blogs
{
    public class BlogEditVM
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
