using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMgmtWeb.Areas.Admin.Controllers
{
    //[Area("Admin")]
    //[Authorize(Roles = "Administrator")]
    public class SetupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
