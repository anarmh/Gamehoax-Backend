namespace Gamehoax_backend.Models
{
    public class BrandModel:BaseEntity
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
