namespace Gamehoax_backend.Models
{
    public class Wishlist:BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<WishlistProduct> WishlistProducts { get; set; }
    }
}
