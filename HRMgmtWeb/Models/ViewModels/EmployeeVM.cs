using HRMgmtWeb.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace HRMgmtWeb.Models.ViewModels
{
    public class EmployeeVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Employee ID")]
        public string EmployeeCode { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Join Date")]
        public DateTime JoinDate { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required]
        public string Position { get; set; }

        [Display(Name = "Status")]
        public EmploymentStatus Status { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }
    }
}
