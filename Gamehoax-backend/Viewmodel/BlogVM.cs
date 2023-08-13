using Gamehoax_backend.Helpers;
using Gamehoax_backend.Models;

namespace Gamehoax_backend.Viewmodel
{
    public class BlogVM
    {
        public List<Blog> Blogs { get; set; }
        public List<Category> Categories { get; set; }
        public List<Tag> Tags { get; set; }
        public Paginate<Blog> PaginateDatas { get; set; }
    }
}
