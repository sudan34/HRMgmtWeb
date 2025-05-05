using Microsoft.AspNetCore.Mvc;

namespace HRMgmtWeb.Areas.Admin.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
