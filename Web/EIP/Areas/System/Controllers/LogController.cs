using Microsoft.AspNetCore.Mvc;

namespace EIP.Areas.System.Controllers
{
    /// <summary>
    /// 日志
    /// </summary>
    [Area("System")]
    public class LogController : Controller
    {
        /// <summary>
        ///     异常日志
        /// </summary>
        /// <returns></returns>
        public IActionResult Exception()
        {
            return View();
        }

        /// <summary>
        ///     登录日志
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        ///     操作日志
        /// </summary>
        /// <returns></returns>
        public IActionResult Operation()
        {
            return View();
        }
    }
}