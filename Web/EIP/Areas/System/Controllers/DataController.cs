using Microsoft.AspNetCore.Mvc;

namespace EIP.Areas.System.Controllers
{
    /// <summary>
    /// 数据
    /// </summary>
    [Area("System")]
    public class DataController : Controller
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