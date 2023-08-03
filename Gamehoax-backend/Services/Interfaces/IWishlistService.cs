using Gamehoax_backend.Models;
using Gamehoax_backend.Viewmodel.Wishlist;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface IWishlistService
    {
        void AddProduct(List<WishlistVM> wishlist, Product product);
        List<WishlistVM> GetDatasFromCookie();
        void DeleteData(int? id);
        int GetCount();
    }
}
