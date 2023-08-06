using System.ComponentModel.DataAnnotations;

namespace Gamehoax_backend.Viewmodel
{
    public class ReviewVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public string Describe { get; set; }
    }
}
