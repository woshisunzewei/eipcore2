using Microsoft.AspNetCore.Mvc;

namespace EIP.Areas.System.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [Area("System")]
    public class UserController : Controller
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 编辑器
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit()
        {
            return View();
        }

        /// <summary>
        /// 编辑头像
        /// </summary>
        /// <returns></returns>
        public IActionResult HeadImage()
        {
            return View();
        }
    }
}