using HRMgmtWeb.Data;
using HRMgmtWeb.Models.ViewModels;
using HRMgmtWeb.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HRMgmtWeb.Areas.HR.Controllers
{
    [Area("HR")]
    [Authorize(Roles = "HR")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmployeeService _employeeService;

     
        public EmployeeController(ApplicationDbContext context, IEmployeeService employeeService)
        {
            _context = context;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                // Ensure proper async execution
                var departments = await _context.Departments
                    .AsNoTracking() // Recommended for dropdown lists
                    .ToListAsync(); // Make sure to use ToListAsync()

                ViewBag.Departments = new SelectList(departments, "Id", "Name");
                return View();
            }
            catch (Exception ex)
            {
                // Log the error (implement proper logging in your application)
                return StatusCode(500, "Error loading departments");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeVM model)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.CreateEmployeeAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var model = new EmployeeVM
            {
                Id = employee.Id,
                EmployeeCode = employee.EmployeeCode,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                DateOfBirth = employee.DateOfBirth,
                JoinDate = employee.JoinDate,
                DepartmentId = employee.DepartmentId,
                Position = employee.Position,
                Status = employee.Status,
                PhoneNumber = employee.PhoneNumber,
                Address = employee.Address
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeVM model)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.UpdateEmployeeAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
