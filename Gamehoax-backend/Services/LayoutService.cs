using Gamehoax_backend.Services.Interfaces;
using Gamehoax_backend.Viewmodel;

namespace Gamehoax_backend.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly ISettingService _settingService;
        private readonly ICartService _cartService;
        public LayoutService(ISettingService settingService, ICartService cartService)
        {
            _settingService = settingService;
            _cartService = cartService;
        }

        public async  Task<LayoutVM> GetAllDatas()
        {
            var datas =  _settingService.GetAll();
            int count = _cartService.GetCount();
            return new LayoutVM { SettingDatas= datas,BasketCount=count };
        }
    }
}
