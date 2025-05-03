using Microsoft.AspNetCore.Mvc;

namespace HRMgmtWeb.Controllers
{
    public class LeaveController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
