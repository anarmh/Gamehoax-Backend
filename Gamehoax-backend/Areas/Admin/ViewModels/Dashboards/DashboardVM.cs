using Gamehoax_backend.Models;

namespace Gamehoax_backend.Areas.Admin.ViewModels.Dashboards
{
    public class DashboardVM
    {
        public int UserCount { get; set; }
        public int ProductCount { get; set; }
        public int CategoryCount { get; set; }
        public int WishlistCount { get; set; }
        public int CartCount { get; set; }
        public List<Subscribe> Subscribes { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
