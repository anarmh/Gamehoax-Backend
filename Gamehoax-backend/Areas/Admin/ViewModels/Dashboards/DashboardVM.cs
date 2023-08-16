using Gamehoax_backend.Models;

namespace Gamehoax_backend.Areas.Admin.ViewModels.Dashboards
{
    public class DashboardVM
    {
        public int UserCount { get; set; }
        public int SuperAdminCount { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
