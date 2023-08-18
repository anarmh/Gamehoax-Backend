namespace Gamehoax_backend.Areas.Admin.ViewModels.Reviews
{
    public class ReviewVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Describe { get; set; }
        public int RatingId { get; set; }
        public string ProductName { get; set; }
        public string UserId { get; set; }
    }
}
