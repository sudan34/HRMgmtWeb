using HRMgmtWeb.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMgmtWeb.Areas.Admin.Controllers
{
    //[Area("Admin")]
    //[Authorize(Roles = "Administrator,HRManager")]
    public class PolicySetupController : Controller
    {
        public IActionResult LeavePolicies()
        {
            return View();
        }

       // [HttpPost]
        //public IActionResult UpdateLeavePolicies(PolicyVM model)
        //{
        //    // Implementation
        //}
    }
}
