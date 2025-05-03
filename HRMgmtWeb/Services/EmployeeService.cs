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

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllAsync();
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
    }
}