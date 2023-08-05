using System.ComponentModel.DataAnnotations;

namespace Gamehoax_backend.Viewmodel.Account
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "The FullName is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "The Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "The repeat password is required")]
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string RepeatPassword { get; set; }
    }
}
