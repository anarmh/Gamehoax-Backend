using Gamehoax_backend.Helpers;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services;
using Gamehoax_backend.Services.Interfaces;
using Gamehoax_backend.Viewmodel;
using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        public BlogController(IBlogService blogService, ICategoryService categoryService, ITagService tagService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        public async Task<IActionResult> Index(int page=1, int take=5,string searchText=null)
        {
            List<Blog> paginateBlogs=await _blogService.GetPaginateDatasAsync(page, take, searchText);
            List<Blog> blogs= await _blogService.GetAllAsync();
            List<Category> categories= await _categoryService.GetAllAsync();
            List<Tag> tags= await _tagService.GetAllAsync();

            ViewBag.searchText = searchText;
            int pageCount;
            if(searchText!=null)
            {
                pageCount = await GetPageCountAsync(take,searchText);
            }
            else
            {
                pageCount = await GetPageCountAsync(take,null);
            }

            Paginate<Blog> paginateDatas = new(paginateBlogs,page,pageCount);

            BlogVM model = new()
            {
                Blogs = blogs,
                Categories = categories,
                Tags = tags,
                PaginateDatas= paginateDatas
            };
            return View(model);
        }

        private async Task<int> GetPageCountAsync(int take,string searchText)
        {
            int blogCount;
            if (searchText != null)
            {
                blogCount = await _blogService.GetBlogsCountBySearchTextAsync(searchText);
            }
            else
            {
                blogCount = await _blogService.GetCountAsync();
            }

            return (int)Math.Ceiling((decimal)blogCount / take);
        }


        public async Task<IActionResult> Search(string searchtext,int page=1, int take=5)
        {
            ViewBag.searchText = searchtext;

            List<Blog> blogs= await _blogService.GetAllBySearchText(searchtext);
            var blogCount = await _blogService.GetCountAsync();
            var pageCount = (int)Math.Ceiling((decimal)blogCount / take);
            Paginate<Blog> paginatedDatas = new(blogs, page, pageCount);
            return PartialView("_BlogPartial",paginatedDatas);
        } 
    }
}
