namespace HRMgmtWebApp.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<string> Roles { get; set; }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }

    public class RoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
