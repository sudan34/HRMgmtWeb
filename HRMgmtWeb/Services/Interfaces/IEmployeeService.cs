using HRMgmtWeb.Models.ViewModels;
using HRMgmtWeb.Models;

namespace HRMgmtWeb.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task CreateEmployeeAsync(EmployeeVM model);
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task UpdateEmployeeAsync(EmployeeVM model);
        Task DeleteEmployeeAsync(int id);
    }
}
