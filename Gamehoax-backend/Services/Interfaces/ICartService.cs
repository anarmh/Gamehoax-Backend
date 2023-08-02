using Gamehoax_backend.Models;

using Gamehoax_backend.Viewmodel.Cart;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface ICartService
    {
        void AddProduct(List<CartVM> basket, Product product);
        List<CartVM> GetDatasFromCookie();
        Task<decimal> GetTotalPrice();
        int GetCount();
        void DeleteData(int? id);
    }
}
