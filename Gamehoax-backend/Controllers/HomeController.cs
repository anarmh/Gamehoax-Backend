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
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public HomeController(ISliderService sliderService, IServiceIconService serviceIconService, ICategoryService categoryService, IProductService productService)
        {
            _sliderService = sliderService;
            _serviceIconService = serviceIconService;
            _categoryService = categoryService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            List<Slider> sliders= await _sliderService.GetAllAsync();
            List<ServiceIcon> serviceIcons=await _serviceIconService.GetAllAsync();
            List<Category> categories=await _categoryService.GetAllAsync();
            List<Product> products=await _productService.GetAllAsync();
            HomeVM model= new() 
            { 
                Sliders= sliders,
                ServiceIcons= serviceIcons,
                Categories= categories,
                Products= products
            };
            return View(model);
        }

    }
}