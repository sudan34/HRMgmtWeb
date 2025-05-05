using System.ComponentModel.DataAnnotations;

namespace HRMgmtWeb.Models.ViewModels
{
    public class UserEditVM
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public List<string> Roles { get; set; } = new List<string>();
    }
}
