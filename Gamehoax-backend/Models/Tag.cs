namespace Gamehoax_backend.Models
{
    public class Tag:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }
    }
}
