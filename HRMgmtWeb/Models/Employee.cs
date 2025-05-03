using HRMgmtWeb.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace HRMgmtWeb.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string EmployeeCode { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;

        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Position { get; set; }

        public EmploymentStatus Status { get; set; } = EmploymentStatus.Active;

        [Phone]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }
    }
}
