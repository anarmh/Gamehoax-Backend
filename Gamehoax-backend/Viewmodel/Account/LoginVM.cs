using System.ComponentModel.DataAnnotations;

namespace Gamehoax_backend.Viewmodel.Account
{
    public class LoginVM
    {

        [Required]
        public string EmailOrUsername { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
