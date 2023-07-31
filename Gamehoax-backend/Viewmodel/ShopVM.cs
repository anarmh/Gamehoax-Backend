using Gamehoax_backend.Helpers;
using Gamehoax_backend.Models;

namespace Gamehoax_backend.Viewmodel
{
    public class ShopVM
    {
       public List<Product> Products { get; set; }
       public List<Category> Categories { get; set; }
       public List<Tag> Tags { get; set; }
       public Paginate<ProductVM> PaginateDatas { get; set; }
        public int CountProducts { get; set; }
    }
}
