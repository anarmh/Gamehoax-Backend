﻿using Gamehoax_backend.Data;
using Gamehoax_backend.Helpers;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Gamehoax_backend.Viewmodel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Gamehoax_backend.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        public ShopController(IProductService productService, ICategoryService categoryService, ITagService tagService,AppDbContext context)
        {
            _productService = productService;
            _categoryService = categoryService;
            _tagService = tagService;
            _context= context;
        }

        public async Task<IActionResult> Index(int page=1, int take=2, string sortValue=null, string searchText=null, int? categoryId=null, int? tagId=null,int? value1=null,int? value2=null)
        {

            List<Product> paginateProducts = await _productService.GetPaginateDatasAsync(page,take,sortValue,searchText,categoryId,tagId,value1,value2);
            List<ProductVM> mappedDatas = _productService.GetMappedDatas(paginateProducts);

            ViewBag.categoryId = categoryId;
            ViewBag.tagId = tagId;
            ViewBag.value1 = value1;
            ViewBag.value2 = value2;
            ViewBag.searchText = searchText;
            ViewBag.sortValue = sortValue;


            int pageCount = 0;
            if (categoryId != null)
            {
                pageCount = await GetPageCountAsync(take, null, null, categoryId, null, null, null);
            }
            if (tagId != null)
            {
                pageCount = await GetPageCountAsync(take, null, null, null, tagId, null, null);
            }
            if (value1 != null && value2 != null)
            {
                pageCount = await GetPageCountAsync(take, null, null, null, null, value1, value2);
            }
            if (searchText != null)
            {
                pageCount = await GetPageCountAsync(take, null, searchText, null, null, null, null);

            }
            if (sortValue != null)
            {
                pageCount = await GetPageCountAsync(take, sortValue, null, null, null, null, null);

            }

            Paginate<ProductVM> paginatedDatas = new(mappedDatas, page, pageCount);

            List<Product> products= await _productService.GetAllAsync();
            List<Category> categories= await _categoryService.GetAllAsync();
            List<Tag> tags= await _tagService.GetAllAsync();
            
            ShopVM model = new()
            {
                Products= products,
                Categories= categories,
                Tags= tags,
                PaginateDatas= paginatedDatas,
            };
            return View(model);
        }



        private async Task<int> GetPageCountAsync(int take, string sortValue,string searchText,int? categoryId, int? tagId,int? value1,int? value2)
        {

            int prodCount=0;
            if (sortValue != null)
            {
                prodCount = await _productService.GetProductsCountBySortTextAsync(sortValue);
            }
            if (searchText != null)
            {
                prodCount = await _productService.GetProductsCountBySearchTextAsync(searchText);
            }
            if (categoryId != null)
            {
                prodCount = await _productService.GetProductsCountByCategoryAsync(categoryId);
            }
            if (tagId != null)
            {
                prodCount = await _productService.GetProductsCountByCategoryAsync(categoryId);
            }
            if (value1 != null && value2 != null)
            {
                prodCount = await _productService.GetProductsCountByRangeAsync(value1, value2);
            }
            if (sortValue == null && searchText == null && categoryId == null && tagId == null  && value1 == null && value2 == null)
            {
                prodCount = await _productService.GetCountAsync();
            }

            return (int)Math.Ceiling((decimal)prodCount / take);
        }



        [HttpGet]
        public async Task<IActionResult> GetRangeProducts(int value1, int value2, int page = 1, int take = 3)
        {
            ViewBag.value1 = value1;
            ViewBag.value2 = value2;


            List<Product> products = await _context.Products.Where(x => x.Price >= value1 && x.Price <= value2).Include(m => m.ProductImages).Include(m=>m.Rating).ToListAsync();
            var productCount = products.Count();
            var pageCount = (int)Math.Ceiling((decimal)productCount / take);
            List<ProductVM> mappedDatas = _productService.GetMappedDatas(products);
            Paginate<ProductVM> paginatedDatas = new(mappedDatas, page, pageCount);
            return PartialView("_ProductsPartial", paginatedDatas);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory(int? id, int page = 1, int take = 3)
        {
            if (id is null) return BadRequest();
            ViewBag.category = id;

            var products = await _productService.GetProductsByCategoryIdAsync(id, page, take);

            int pageCount = await GetPageCountAsync(take, null, null, (int)id, null, null, null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductsPartial", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByTag(int? id, int page = 1, int take = 3)
        {
            if (id is null) return BadRequest();
            ViewBag.tagId = id;

            var products = await _productService.GetProductsByTagIdAsync(id, page, take);

            int pageCount = await GetPageCountAsync(take, null, null, (int)id, null, null, null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductsPartial", model);
        }


        public async Task<IActionResult> Sort(string sortValue, int page = 1, int take = 3)
        {
            ViewBag.sortValue = sortValue;

            List<Product> products = new();

            if (sortValue == "1")
            {
                products = await _context.Products.Include(m => m.ProductImages).Include(m=>m.Rating).ToListAsync();
            };
            if (sortValue == "2")
            {
                products = await _context.Products.Include(m => m.ProductImages).OrderByDescending(p => p.CreateDate).ToListAsync();

            };
            if (sortValue == "3")
            {
                products = await _context.Products.Include(m => m.ProductImages).OrderByDescending(p => p.Rating.RatingCount).ToListAsync();

            };
           
            if (sortValue == "4")
            {
                products = await _context.Products.Include(m => m.ProductImages).OrderBy(p => p.Price).ToListAsync();

            };
            if (sortValue == "4")
            {
                products = await _context.Products.Include(m => m.ProductImages).OrderByDescending(p => p.Price).ToListAsync();

            };
            int productCount = products.Count();
            var pageCount = (int)Math.Ceiling((decimal)productCount / take);
            List<ProductVM> mappedDatas = _productService.GetMappedDatas(products);
            Paginate<ProductVM> model = new(mappedDatas, page, pageCount);

            return PartialView("_ProductsPartial", model);
        }
    }
}
