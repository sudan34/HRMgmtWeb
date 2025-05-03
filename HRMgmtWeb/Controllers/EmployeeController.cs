using HRMgmtWeb.Models.ViewModels;
using HRMgmtWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRMgmtWeb.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM model)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.CreateEmployeeAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
