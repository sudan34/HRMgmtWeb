using HRMgmtWeb.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace HRMgmtWeb.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public LeaveRequestStatus Status { get; set; } = LeaveRequestStatus.Pending;

        [Required]
        public string LeaveType { get; set; } // Vacation, Sick, Personal, etc.

        public string Reason { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.UtcNow;

        public DateTime? ActionDate { get; set; }

        public string ActionNotes { get; set; }
    }
}
