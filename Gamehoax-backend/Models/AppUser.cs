using Microsoft.AspNetCore.Identity;

namespace Gamehoax_backend.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<Wishlist> Wishlists { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
