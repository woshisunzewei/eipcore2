using Microsoft.AspNetCore.Mvc;

namespace EIP.Areas.System.Controllers
{
    /// <summary>
    /// 定时作业
    /// </summary>
    [Area("System")]
    public class JobController : Controller
    {
        /// <summary>
        /// 任务列表
        /// </summary>
        /// <returns></returns>
        public IActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 任务编辑器
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit()
        {
            return View();
        }

        /// <summary>
        /// 日志
        /// </summary>
        /// <returns></returns>
        public IActionResult Log()
        {
            return View();
        }

        /// <summary>
        /// Corn表达式
        /// </summary>
        /// <returns></returns>
        public IActionResult Cron()
        {
            return View();
        }

        /// <summary>
        /// 需要排除日历
        /// </summary>
        /// <returns></returns>
        public IActionResult CalendarList()
        {
            return View();
        }

        /// <summary>
        /// 日历编辑
        /// </summary>
        /// <returns></returns>
        public IActionResult CalendarEdit()
        {
            return View();
        }
    }
}