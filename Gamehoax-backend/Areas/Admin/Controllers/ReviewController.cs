using Gamehoax_backend.Areas.Admin.ViewModels.Reviews;
using Gamehoax_backend.Areas.Admin.ViewModels.Users;
using Gamehoax_backend.Data;
using Gamehoax_backend.Helpers;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly AppDbContext _context;
        public ReviewController(IReviewService reviewService,AppDbContext context)
        {
            _reviewService = reviewService;
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 5)
        {
            List<Review> reviews = await _reviewService.GetPaginatedDatasAsync(page,take);
            List<ReviewVM> mappedDatas = GetDatas(reviews);

            int pageCount = await GetPageCountAsync(take);
            Paginate<ReviewVM> paginatedDatas = new(mappedDatas, page, pageCount);
            return View(paginatedDatas);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            var reviewCount = await _reviewService.GetCountAsync();
            return (int)Math.Ceiling((decimal)reviewCount / take);
        }

        private  List<ReviewVM> GetDatas(List<Review> reviews)
        {
            List<ReviewVM> mappedDatas = new();
            foreach (var review in reviews)
            {
               
                ReviewVM reviewList = new()
                {
                    Id = review.Id,
                    FullName = review.AppUser.FullName,
                    Email = review.AppUser.Email,
                    Describe = review.Describe,
                   RatingId=review.RatingId,
                   ProductName=review.Product.Name
                };
                mappedDatas.Add(reviewList);
            }
            return mappedDatas;
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();

                Review review = await _reviewService.GetByIdAsync((int)id);

                if (review is null) return NotFound();

                _context.Reviews.Remove(review);

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
