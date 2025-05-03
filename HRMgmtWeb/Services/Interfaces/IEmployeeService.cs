using HRMgmtWeb.Models.ViewModels;
using HRMgmtWeb.Models;
using System.Linq.Expressions;

namespace HRMgmtWeb.Services.Interfaces
{
    public interface IEmployeeService
    {
        public interface IEmployeeService
        {
            Task<List<Employee>> GetAllEmployeesAsync(
                Expression<Func<Employee, bool>> filter = null,
                Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null,
                string includeProperties = "");

            Task<Employee> GetEmployeeByIdAsync(int id);
            Task CreateEmployeeAsync(EmployeeVM model);
            Task UpdateEmployeeAsync(EmployeeVM model);
            Task DeleteEmployeeAsync(int id);
            Task<bool> EmployeeExistsAsync(int id);
            Task<List<Employee>> SearchEmployeesAsync(string searchString);
            Task<int> GetActiveEmployeeCountAsync();
        }
    }
}
