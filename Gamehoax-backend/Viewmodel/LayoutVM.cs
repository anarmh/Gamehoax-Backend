using Gamehoax_backend.Models;

namespace Gamehoax_backend.Viewmodel
{
    public class LayoutVM
    {
        public Dictionary<string, string> SettingDatas { get; set; }
        public int BasketCount { get; set; }
        public int WishlistCount { get; set; }
        public string Email { get; set; }
    }
}
