using Microsoft.AspNetCore.Mvc;

namespace HRMgmtWeb.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
