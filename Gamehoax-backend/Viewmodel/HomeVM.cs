﻿using Gamehoax_backend.Models;

namespace Gamehoax_backend.Viewmodel
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<ServiceIcon> ServiceIcons { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Brand> Brands { get; set; }
    }
}
