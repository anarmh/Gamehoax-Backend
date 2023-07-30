using Gamehoax_backend.Models;

namespace Gamehoax_backend.Viewmodel
{
    public class AboutVM
    {
        public List<About> Abouts { get; set; }
        public List<ServiceIcon> ServiceIcons { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public List<Brand> Brands { get; set; }
    }
}
