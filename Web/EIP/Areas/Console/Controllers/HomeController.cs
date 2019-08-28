using Microsoft.AspNetCore.Mvc;

namespace EIP.Areas.Console.Controllers
{
    /// <summary>
    /// 控制面板
    /// </summary>
    [Area("Console")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }
    }
}