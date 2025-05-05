using HRMgmtWeb.Data;
using HRMgmtWeb.Models;
using HRMgmtWeb.Models.Enums;
using HRMgmtWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace HRMgmtWeb.Controllers;

[Area("SuperAdmin")]
//[Authorize(Roles = "SuperAdmin")]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DashboardController> _logger;

    public DashboardController(ILogger<DashboardController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var dashboardData = new DashboardViewModel
        {
            TotalEmployees = await _context.Employees.CountAsync(),
            ActiveEmployees = await _context.Employees
                    .CountAsync(e => e.Status == EmploymentStatus.Active),
            OnLeaveEmployees = await _context.Employees
                    .CountAsync(e => e.Status == EmploymentStatus.OnLeave),
            RecentHires = await _context.Employees
                    .Include(e => e.Department)
                    .OrderByDescending(e => e.JoinDate)
                    .Take(5)
                    .ToListAsync(),
            OpenRequests = 0 
        };

        return View(dashboardData);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
