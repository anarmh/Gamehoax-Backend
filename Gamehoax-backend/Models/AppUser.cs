using Microsoft.AspNetCore.Identity;

namespace Gamehoax_backend.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
