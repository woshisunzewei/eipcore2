using Microsoft.AspNetCore.Mvc;
using System;

namespace EIP.Areas.Common.Controllers
{
    /// <summary>
    /// 公用
    /// </summary>
    [Area("Common")]
    public class UserControlController : Controller
    {
        /// <summary>
        ///     查看具有特权的人员
        /// </summary>
        /// <param name="privilegeMaster">归属人员类型:企业、角色、岗位、组</param>
        /// <param name="privilegeMasterValue">企业Id、角色Id、岗位Id、组Id</param>
        /// <returns></returns>
        public IActionResult ChosenPrivilegeMasterUser(int privilegeMaster,
            Guid privilegeMasterValue)
        {
            ViewData["privilegeMaster"] = privilegeMaster;
            ViewData["privilegeMasterValue"] = privilegeMasterValue;
            return View();
        }
    }
}