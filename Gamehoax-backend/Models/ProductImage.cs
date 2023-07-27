namespace Gamehoax_backend.Models
{
    public class ProductImage:BaseEntity
    {
        public string Image { get; set; }
        public bool IsMain { get; set; }
        public bool IsHover { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
