using Microsoft.AspNetCore.Mvc;

namespace HRMgmtWeb.Controllers
{
    public class AttendanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
