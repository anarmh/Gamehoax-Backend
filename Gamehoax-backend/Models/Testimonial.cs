namespace Gamehoax_backend.Models
{
    public class Testimonial:BaseEntity
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
}
