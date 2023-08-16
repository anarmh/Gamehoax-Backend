using Gamehoax_backend.Models;

using Gamehoax_backend.Viewmodel.Cart;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface ICartService
    {
        void AddProduct(List<CartVM> basket, Product product);
        List<CartVM> GetDatasFromCookie();
        int GetCount();
        void DeleteData(int? id);
        Task<Cart> GetByUserIdAsync(string userId);
        Task<List<CartProduct>> GetAllByCartIdAsync(int? cartId);
        Task<int> GetCountAsync();
    }
}
