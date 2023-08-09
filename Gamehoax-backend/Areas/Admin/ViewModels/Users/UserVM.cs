namespace Gamehoax_backend.Areas.Admin.ViewModels.Users
{
    public class UserVM
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}