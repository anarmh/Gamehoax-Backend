using Gamehoax_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gamehoax_backend.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly ILayoutService _layoutService;

        public HeaderViewComponent(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var datas = await _layoutService.GetAllDatas();
            return await Task.FromResult(View(datas));
        }
    }
}
