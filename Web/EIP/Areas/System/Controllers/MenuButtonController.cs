using Microsoft.AspNetCore.Mvc;

namespace EIP.Areas.System.Controllers
{
    /// <summary>
    /// 菜单按钮
    /// </summary>
    [Area("System")]
    public class MenuButtonController : Controller
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
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit()
        {
            return View();
        }
    }
}