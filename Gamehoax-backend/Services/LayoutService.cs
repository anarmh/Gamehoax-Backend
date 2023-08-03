using Gamehoax_backend.Services.Interfaces;
using Gamehoax_backend.Viewmodel;

namespace Gamehoax_backend.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly ISettingService _settingService;
        private readonly ICartService _cartService;
        private readonly IWishlistService _wishlistService;
        public LayoutService(ISettingService settingService, ICartService cartService,IWishlistService wishlistService)
        {
            _settingService = settingService;
            _cartService = cartService;
            _wishlistService= wishlistService;
        }

        public async  Task<LayoutVM> GetAllDatas()
        {
            var datas =  _settingService.GetAll();
            int cartCount = _cartService.GetCount();
            int wishCount = _wishlistService.GetCount();
            return new LayoutVM { SettingDatas= datas,BasketCount= cartCount,WishlistCount=wishCount };
        }
    }
}
