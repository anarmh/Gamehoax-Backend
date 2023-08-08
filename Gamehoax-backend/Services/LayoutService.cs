using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Gamehoax_backend.Viewmodel;

namespace Gamehoax_backend.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly ISettingService _settingService;
        private readonly ICartService _cartService;
        private readonly IWishlistService _wishlistService;
        private readonly ISubscribeService _subscribeService;
        public LayoutService(ISettingService settingService, ICartService cartService,IWishlistService wishlistService, ISubscribeService subscribeService)
        {
            _settingService = settingService;
            _cartService = cartService;
            _wishlistService = wishlistService;
            _subscribeService = subscribeService;
        }

        public async  Task<LayoutVM> GetAllDatas()
        {
            var datas =  _settingService.GetAll();
            int cartCount = _cartService.GetCount();
            int wishCount = _wishlistService.GetCount();
            List<Subscribe> subscribes = await _subscribeService.GetAllAsync();

            LayoutVM model = new()
            {
                SettingDatas = datas,
                BasketCount = cartCount,
                WishlistCount = wishCount,
                Email=subscribes.FirstOrDefault()?.Email
            };
           
            return model;
        }
    }
}
