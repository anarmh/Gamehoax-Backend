namespace Gamehoax_backend.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
