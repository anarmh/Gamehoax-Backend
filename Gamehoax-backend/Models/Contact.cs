namespace Gamehoax_backend.Models
{
    public class Contact:BaseEntity
    {
        public string Subject { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
