namespace Gamehoax_backend.Models
{
    public class Discount:BaseEntity
    {
        public string Name { get; set; }
        public byte Percent { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
