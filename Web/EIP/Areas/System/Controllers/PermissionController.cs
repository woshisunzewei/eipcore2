using Microsoft.AspNetCore.Mvc;
using System;

namespace EIP.Areas.System.Controllers
{
    /// <summary>
    /// 权限
    /// </summary>
    [Area("System")]
    public class PermissionController : Controller
    {
       
        /// <summary>
        ///     模块按钮权限
        /// </summary>
        /// <returns></returns>
        public IActionResult Button(Guid privilegeMasterValue,
            int privilegeMaster)
        {
            ViewData["privilegeMasterValue"] = privilegeMasterValue;
            ViewData["privilegeMaster"] = privilegeMaster;
            return View();
        }

        /// <summary>
        ///     数据权限
        /// </summary>
        /// <returns></returns>
        public IActionResult Data(Guid privilegeMasterValue,
            int privilegeMaster)
        {
            ViewData["privilegeMasterValue"] = privilegeMasterValue;
            ViewData["privilegeMaster"] = privilegeMaster;
            return View();
        }

        /// <summary>
        ///     模块权限
        /// </summary>
        /// <returns></returns>
        public IActionResult Menu(Guid privilegeMasterValue,
            int privilegeMaster)
        {
            ViewData["privilegeMasterValue"] = privilegeMasterValue;
            ViewData["privilegeMaster"] = privilegeMaster;
            return View();
        }
    }
}