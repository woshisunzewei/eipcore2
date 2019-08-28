using Microsoft.AspNetCore.Mvc;

namespace EIP.Controllers
{
    /// <summary>
    /// 帐号
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }
    }
}