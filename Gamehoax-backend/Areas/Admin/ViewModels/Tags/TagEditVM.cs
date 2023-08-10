using Microsoft.Build.Framework;

namespace Gamehoax_backend.Areas.Admin.ViewModels.Tags
{
    public class TagEditVM
    {
        [Required]
        public string Name { get; set; }
    }
}
