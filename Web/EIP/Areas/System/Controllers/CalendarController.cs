using Microsoft.AspNetCore.Mvc;

namespace EIP.Areas.System.Controllers
{
    [Area("System")]
    public class CalendarController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}