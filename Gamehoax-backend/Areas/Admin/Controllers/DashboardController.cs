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
        public DashboardController(IUserService userService,
                                   UserManager<AppUser> userManager,
                                   IContactService contactService)
        {
            _userService = userService; 
            _userManager = userManager;
            _contactService = contactService;
        }
        public async Task<IActionResult> Index()
        {
            int userCount=await _userService.GetCountAsync();
            var superAdminUsers = await _userManager.GetUsersInRoleAsync(Roles.SuperAdmin.ToString());
            int superAdminCount = superAdminUsers.Count;
            List<Contact> contacts=await _contactService.GetAllAsync();
            DashboardVM model = new()
            {
                UserCount = userCount,
                SuperAdminCount = superAdminCount,
                Contacts= contacts,
            };
            return View(model);
        } 
    }
}
