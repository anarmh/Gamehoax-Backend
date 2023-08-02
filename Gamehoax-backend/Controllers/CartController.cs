﻿using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Gamehoax_backend.Viewmodel.Cart;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gamehoax_backend.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly AppDbContext _context;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        public CartController(ICartService cartService, IProductService productService, AppDbContext context, IHttpContextAccessor accessor)
        {
            _cartService = cartService;
            _productService = productService;
            _context = context;
            _accessor = accessor;
        }

        public async Task<IActionResult> Index()
        {
            List<CartDetailVM> cartList = new();

            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {

                List<CartVM> cartDatas = JsonConvert.DeserializeObject<List<CartVM>>(_accessor.HttpContext.Request.Cookies["basket"]);

                foreach (var item in cartDatas)
                {
                    var dbProduct = await _productService.GetByIdAsync(item.ProductId);

                    if (dbProduct != null)
                    {
                        CartDetailVM cartDetail = new()
                        {
                            Id = dbProduct.Id,
                            Title = dbProduct.Title,
                            Image = dbProduct.ProductImages.Where(m => m.IsMain).FirstOrDefault().Image,
                            Count = item.Count,
                            Price = dbProduct.Price,
                            Total = item.Count * dbProduct.Price
                        };
                        cartList.Add(cartDetail);
                    }

                }

            }
            return View(cartList);
        }



        [HttpPost]
        public async Task<IActionResult> AddToCart(int? id)
        {

            if (id is null) return BadRequest();

            Product product = await _productService.GetByIdAsync(id);
            if (product is null) return NotFound();

            List<CartVM> basket = _cartService.GetDatasFromCookie();

            _cartService.AddProduct(basket, product);

            return Ok(basket.Sum(m => m.Count));
           
        }


        [HttpPost]
        public IActionResult DeleteDataFromBasket(int? id)
        {
            if (id is null) return BadRequest();

            _cartService.DeleteData((int)id);
            List<CartVM> baskets = _cartService.GetDatasFromCookie();
            return Ok(baskets.Count);
        }

    }
}
