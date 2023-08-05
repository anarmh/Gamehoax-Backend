using System.ComponentModel.DataAnnotations;

namespace Gamehoax_backend.Viewmodel.Account
{
    public class ForgotPasswordVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
