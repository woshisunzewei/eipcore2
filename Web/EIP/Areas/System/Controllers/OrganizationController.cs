using Microsoft.AspNetCore.Mvc;

namespace EIP.Areas.System.Controllers
{
    /// <summary>
    /// 归属机构
    /// </summary>
    [Area("System")]
    public class OrganizationController : Controller
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

        /// <summary>
        /// 地图
        /// </summary>
        /// <returns></returns>
        public IActionResult Map()
        {
            return View();
        }

        /// <summary>
        /// 图表
        /// </summary>
        /// <returns></returns>
        public IActionResult OrgChart()
        {
            return View();
        }
    }
}