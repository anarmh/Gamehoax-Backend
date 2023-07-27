namespace Gamehoax_backend.Models
{
    public class Review:BaseEntity
    {
        public string Name { get; set; }
        public string Describe { get; set; }
        public string Email { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
