using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMgmtWeb.Areas.Admin.Controllers
{
    //[Area("Admin")]
    //[Authorize(Roles = "Administrator")]
    public class DepartmentSetupController : Controller
    {
        //private readonly IDepartmentService _deptService;

        //public DepartmentSetupController(IDepartmentService deptService)
        //{
        //    _deptService = deptService;
        //}
        public IActionResult Index()
        {
            //var departments = await _deptService.GetAllAsync();
            //return View(departments);
            return View();
        }
    }
}
