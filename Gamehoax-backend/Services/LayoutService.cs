using Gamehoax_backend.Services.Interfaces;
using Gamehoax_backend.Viewmodel;

namespace Gamehoax_backend.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly ISettingService _settingService;
        public LayoutService(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async  Task<LayoutVM> GetAllDatas()
        {
            var datas =  _settingService.GetAll();
            return new LayoutVM { SettingDatas= datas };
        }
    }
}
