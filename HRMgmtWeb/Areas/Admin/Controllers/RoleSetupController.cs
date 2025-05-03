using Microsoft.AspNetCore.Mvc;

namespace HRMgmtWeb.Areas.Admin.Controllers
{
    public class RoleSetupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
