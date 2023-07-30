using Gamehoax_backend.Viewmodel;

namespace Gamehoax_backend.Services.Interfaces
{
    public interface ILayoutService
    {
        Task<LayoutVM> GetAllDatas();
    }
}
