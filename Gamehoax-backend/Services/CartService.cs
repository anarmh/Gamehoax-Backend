using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Gamehoax_backend.Viewmodel.Cart;
using Newtonsoft.Json;

namespace Gamehoax_backend.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IProductService _productService;
        public CartService(IProductService productService, IHttpContextAccessor accessor)
        {
            _productService= productService;
            _accessor = accessor;
        }

        public void AddProduct(List<CartVM> basket, Product product)
        {
            CartVM existProduct=basket.FirstOrDefault(m=>m.ProductId==product.Id);

            if (existProduct==null)
            {
                basket.Add(new CartVM()
                {
                    ProductId = product.Id,
                    Count = 1
                });
            }
            else
            {
                existProduct.Count++;
            }
            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
        }

        public List<CartVM> GetDatasFromCookie()
        {
            List<CartVM> carts;

            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {
                carts = JsonConvert.DeserializeObject<List<CartVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                carts= new List<CartVM>();
            }
            return carts;

        }

        
        public  void DeleteData(int? id)
        {
            var baskets = JsonConvert.DeserializeObject<List<CartVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            var deletedProduct = baskets.FirstOrDefault(b => b.ProductId == id);
            baskets.Remove(deletedProduct);
            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(baskets));
        
        }

        public int GetCount()
        {
            List<CartVM> basket;

            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<CartVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<CartVM>();
            }

            return basket.Sum(m => m.Count);
        }


      
    }
}
