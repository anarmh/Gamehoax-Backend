using Gamehoax_backend.Areas.Admin.ViewModels.Subscribes;
using Gamehoax_backend.Data;
using Gamehoax_backend.Helpers;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubscribeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISubscribeService _subscribeService;

        public SubscribeController(AppDbContext context, ISubscribeService subscribeService)
        {
            _context = context;
            _subscribeService = subscribeService;
        }

        public async Task<IActionResult> Index(int page=1, int take=5)
        {
            List<Subscribe> subscribes = await _subscribeService.GetPaginatedDatasAsync(page,take);
            List<SubscribeVM> mappedDatas = GetDatas(subscribes);

            int pageCount = await GetPageCountAsync(take);
            Paginate<SubscribeVM> paginatedDatas = new(mappedDatas, page, pageCount);
            return View(paginatedDatas);
        }


        private async Task<int> GetPageCountAsync(int take)
        {
            var reviewCount = await _subscribeService.GetCountAsync();
            return (int)Math.Ceiling((decimal)reviewCount / take);
        }

        private  List<SubscribeVM>  GetDatas(List<Subscribe> subscribes)
        {
            List<SubscribeVM> mappedDatas = new();
            foreach (var sub in subscribes)
            {
                mappedDatas.Add(new SubscribeVM
                {
                    Id = sub.Id,
                    Email = sub.Email,
                });
            }
            return mappedDatas;
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();

                Subscribe subscribe = await _subscribeService.GetByIdAsync((int)id);

                if (subscribe is null) return NotFound();

                _context.Subscribes.Remove(subscribe);

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
