using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Gamehoax_backend.Viewmodel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Gamehoax_backend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IServiceIconService _serviceIconService;

        public HomeController(ISliderService sliderService, IServiceIconService serviceIconService)
        {
            _sliderService = sliderService;
            _serviceIconService = serviceIconService;
        }

        public async Task<IActionResult> Index()
        {
            List<Slider> sliders= await _sliderService.GetAllAsync();
            List<ServiceIcon> serviceIcons=await _serviceIconService.GetAllAsync();

            HomeVM model= new() 
            { 
                Sliders= sliders,
                ServiceIcons= serviceIcons,
            };
            return View(model);
        }

    }
}