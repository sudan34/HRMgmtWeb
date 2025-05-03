using Microsoft.AspNetCore.Mvc;

namespace HRMgmtWeb.Controllers
{
    public class PayrollController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
