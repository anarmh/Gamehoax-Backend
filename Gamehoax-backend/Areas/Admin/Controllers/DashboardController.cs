using Gamehoax_backend.Areas.Admin.ViewModels.Dashboards;
using Gamehoax_backend.Helpers;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class DashboardController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IContactService _contactService;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISubscribeService _subscribeService;
        private readonly IReviewService _reviewService;
        private readonly IWishlistService _wishlistService;
        private readonly ICartService _cartService;
        public DashboardController(IUserService userService,
                                   UserManager<AppUser> userManager,
                                   IContactService contactService,
                                   IProductService productService,
                                   ICategoryService categoryService,
                                   ISubscribeService subscribeService,
                                   IReviewService reviewService,
                                   IWishlistService wishlistService,
                                   ICartService cartService)
        {
            _userService = userService; 
            _userManager = userManager;
            _contactService = contactService;
            _productService = productService;
            _categoryService = categoryService;
            _subscribeService = subscribeService;
            _reviewService= reviewService;
            _wishlistService= wishlistService;
            _cartService= cartService;
        }
        public async Task<IActionResult> Index()
        {
            int userCount=await _userService.GetCountAsync();
            int productCount=await _productService.GetCountAsync();
            int categoryCount = await _categoryService.GetCountAsync();
            int wishlistCount = await _wishlistService.GetCountAsync();
            int cartCount= await _cartService.GetCountAsync();

            ViewBag.superAdminCount = await GetSuperAdminCount();
            ViewBag.admin=await GetAdminCount();
            ViewBag.member=await GetMemberCount();

            List<Subscribe> subscribes=await _subscribeService.GetAllAsync();
            List<Contact> contacts=await _contactService.GetAllAsync();
            List<Review> reviews =await _reviewService.GetAllAsync();

            DashboardVM model = new()
            {
                UserCount = userCount,
                Contacts= contacts,
                ProductCount= productCount,
                CategoryCount= categoryCount,
                Subscribes= subscribes,
                Reviews= reviews,
                WishlistCount= wishlistCount,
                CartCount= cartCount,
            };
            return View(model);
        } 


        private async Task<int> GetSuperAdminCount()
        {
            var superAdminUsers= await _userManager.GetUsersInRoleAsync(Roles.SuperAdmin.ToString());

            var superAdminCount= superAdminUsers.Count;
            return superAdminCount;
        }


        private async Task<int> GetAdminCount()
        {
            var adminUsers = await _userManager.GetUsersInRoleAsync(Roles.Admin.ToString());

            var adminCount = adminUsers.Count;
            return adminCount;
        }

        private async Task<int> GetMemberCount()
        {
            var memberUsers = await _userManager.GetUsersInRoleAsync(Roles.Member.ToString());

            var memberCount = memberUsers.Count;
            return memberCount;
        }
    }
}
