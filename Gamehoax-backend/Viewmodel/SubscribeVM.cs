using System.ComponentModel.DataAnnotations;

namespace Gamehoax_backend.Viewmodel
{
    public class SubscribeVM
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
    }
}
