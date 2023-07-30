using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Gamehoax_backend.Viewmodel;
using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;
        private readonly IServiceIconService _serviceIconService;
        private readonly ITestimonialService _testimonialService;
        private readonly IBrandService _brandService;
        public AboutController(IAboutService aboutService, IServiceIconService serviceIconService,ITestimonialService testimonialService, IBrandService brandService)
        {
            _aboutService = aboutService;
            _serviceIconService = serviceIconService;
            _testimonialService = testimonialService;
            _brandService = brandService;
        }
        public async Task<IActionResult> Index()
        {
            List<About> abouts=await _aboutService.GetAllAsync();
            List<ServiceIcon> serviceIcons=await _serviceIconService.GetAllAsync();
            List<Testimonial> testimonials=await _testimonialService.GetAllAsync();
            List<Brand> brands=await _brandService.GetAllAsync();

            AboutVM model = new()
            {
                Abouts= abouts,
                ServiceIcons= serviceIcons,
                Testimonials= testimonials,
                Brands= brands
            };

            return View(model);
        }
    }
}
