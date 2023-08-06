using Gamehoax_backend.Models;

namespace Gamehoax_backend.Areas.Admin.ViewModels.Products
{
    public class ProductListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Model { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
