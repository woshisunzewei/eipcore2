using Microsoft.AspNetCore.Mvc;

namespace EIP.Areas.System.Controllers
{
    /// <summary>
    /// 角色
    /// </summary>
    [Area("System")]
    public class RoleController : Controller
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
    }
}