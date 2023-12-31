﻿using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Gamehoax_backend.Viewmodel;
using Gamehoax_backend.Viewmodel.Wishlist;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Gamehoax_backend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IServiceIconService _serviceIconService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly ITestimonialService _testimonialService;
        private readonly IBlogService _blogService;
        private readonly IBrandService _brandService;
        private readonly IHttpContextAccessor _accessor;
        private readonly AppDbContext _context;
        public HomeController(ISliderService sliderService, 
                              IServiceIconService serviceIconService,
                              ICategoryService categoryService, 
                              IProductService productService,
                              ITestimonialService testimonialService,
                              IBlogService blogService,
                              IBrandService brandService,
                              IHttpContextAccessor accessor,
                              AppDbContext context)
        {
            _sliderService = sliderService;
            _serviceIconService = serviceIconService;
            _categoryService = categoryService;
            _productService = productService;
            _testimonialService= testimonialService;
            _blogService = blogService;
            _brandService= brandService;
            _accessor= accessor;
            _context= context;
        }

        public async Task<IActionResult> Index()
        {

            if (_accessor.HttpContext.Request.Cookies["wishlist"] != null)
            {
                var myCookie = JsonConvert.DeserializeObject<List<WishlistVM>>(_accessor.HttpContext.Request.Cookies["wishlist"]);
                ViewBag.Cookie = myCookie;
            }
            List<Slider> sliders= await _sliderService.GetAllAsync();
            List<ServiceIcon> serviceIcons=await _serviceIconService.GetAllAsync();
            List<Category> categories=await _categoryService.GetAllAsync();
            List<Product> products=await _productService.GetAllAsync();
            List<Testimonial> testimonials=await _testimonialService.GetAllAsync();
            List<Blog> blogs=await _blogService.GetAllAsync();
            List<Brand> brands=await _brandService.GetAllAsync();

            HomeVM model= new() 
            { 
                Sliders= sliders,
                ServiceIcons= serviceIcons,
                Categories= categories,
                Products= products,
                Testimonials= testimonials,
                Blogs=blogs,
                Brands= brands,
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostSubscribe(SubscribeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Index");
                }

                var existSubscribe = await _context.Subscribes.FirstOrDefaultAsync(m=>m.Email==model.Email);

                if(existSubscribe != null)
                {
                    ModelState.AddModelError("Email", "Email already exist");
                    return RedirectToAction("Index");
                }
                Subscribe subscribe = new()
                {
                    Email = model.Email,
                };
                await _context.Subscribes.AddAsync(subscribe);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
           

        }
    }
}