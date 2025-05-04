using HRMgmtWeb.Data;
using HRMgmtWeb.Data.Interfaces;
using HRMgmtWeb.Models;
using HRMgmtWeb.Models.Enums;
using HRMgmtWeb.Models.ViewModels;
using HRMgmtWeb.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HRMgmtWeb.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ApplicationDbContext _context;

        public EmployeeService(IEmployeeRepository employeeRepository, ApplicationDbContext context)
        {
            _employeeRepository = employeeRepository;
            _context = context;
        }

        // Implement all interface methods:

        public async Task<List<Employee>> GetAllEmployeesAsync(
            Expression<Func<Employee, bool>> filter = null,
            Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<Employee> query = _context.Employees;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task CreateEmployeeAsync(EmployeeVM model)
        {
            var employee = new Employee
            {
                EmployeeCode = model.EmployeeCode,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth,
                JoinDate = model.JoinDate,
                DepartmentId = model.DepartmentId,
                Position = model.Position,
                Status = model.Status,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address
            };

            await _employeeRepository.CreateAsync(employee);
        }

        public async Task UpdateEmployeeAsync(EmployeeVM model)
        {
            var employee = new Employee
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth,
                DepartmentId = model.DepartmentId,
                Position = model.Position,
                Status = model.Status,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                ModifiedDate = DateTime.UtcNow
            };

            await _employeeRepository.UpdateAsync(employee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _employeeRepository.DeleteAsync(id);
        }

        public async Task<bool> EmployeeExistsAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            return employee != null;
        }

        public async Task<List<Employee>> SearchEmployeesAsync(string searchString)
        {
            return await _context.Employees
                .Where(e => e.FirstName.Contains(searchString) ||
                           e.LastName.Contains(searchString) ||
                           e.Email.Contains(searchString) ||
                           e.Position.Contains(searchString))
                .ToListAsync();
        }

        public async Task<int> GetActiveEmployeeCountAsync()
        {
            return await _context.Employees
                .CountAsync(e => e.Status == EmploymentStatus.Active);
        }
    }
}