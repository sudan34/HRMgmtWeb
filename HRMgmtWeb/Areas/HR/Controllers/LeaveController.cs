using Microsoft.AspNetCore.Mvc;

namespace HRMgmtWeb.Areas.HR.Controllers
{
    public class LeaveController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
