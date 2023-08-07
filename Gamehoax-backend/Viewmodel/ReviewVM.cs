using System.ComponentModel.DataAnnotations;

namespace Gamehoax_backend.Viewmodel
{
    public class ReviewVM
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Describe { get; set; }
        [Required]
        public int RatingId { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
