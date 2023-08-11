using Gamehoax_backend.Areas.Admin.ViewModels.Users;
using Gamehoax_backend.Data;
using Gamehoax_backend.Helpers;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gamehoax_backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public UserController(IUserService userService, AppDbContext context,UserManager<AppUser> userManager)
        {
            _userService = userService;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 5)
        {
            List<AppUser> appUsers = await _userService.GetPaginatedDatasAsync(page, take);
            List<UserVM> mappedDatas = await GetDatasAsync(appUsers,_userManager);

            int pageCount = await GetPageCountAsync(take);

            Paginate<UserVM> paginatedDatas = new(mappedDatas, page, pageCount);

            return View(paginatedDatas);
        }
        private async Task<int> GetPageCountAsync(int take)
        {
            var userCount = await _userService.GetCountAsync();
            return (int)Math.Ceiling((decimal)userCount / take);
        }
        private async Task<List<UserVM>> GetDatasAsync(List<AppUser> users, UserManager<AppUser> userManager)
        {
            List<UserVM> mappedDatas = new();
            foreach (var user in users)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                UserVM userList = new()
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    UserName = user.UserName,
                    UserRoles= userRoles
                };
                mappedDatas.Add(userList);
            }
            return mappedDatas;
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (id == null) return BadRequest();

                AppUser user = await _userService.GetByIdAsync(id);

                if (user is null) return NotFound();

                _context.Users.Remove(user);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
    }
}
