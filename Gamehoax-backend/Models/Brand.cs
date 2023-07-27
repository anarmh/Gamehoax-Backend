namespace Gamehoax_backend.Models
{
    public class Brand:BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public ICollection<BrandModel> BrandModels { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
