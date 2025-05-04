namespace HRMgmtWeb.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalEmployees { get; set; }
        public int ActiveEmployees { get; set; }
        public int OnLeaveEmployees { get; set; }
        public int OpenRequests { get; set; }
        public List<Employee> RecentHires { get; set; }
    }
}
