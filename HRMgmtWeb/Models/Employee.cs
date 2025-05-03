using HRMgmtWeb.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace HRMgmtWeb.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public string Position { get; set; }
        public EmploymentStatus Status { get; set; } // Active, Terminated, OnLeave
    }
}
