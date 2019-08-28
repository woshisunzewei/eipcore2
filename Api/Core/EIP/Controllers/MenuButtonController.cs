using EIP.Common.Core.Extensions;
using EIP.Common.Entities.Dtos;
using EIP.Common.Restful;
using EIP.Common.Restful.Attribute;
using EIP.System.Business.Permission;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EIP.Controllers
{
    /// <summary>
    ///     界面按钮控制器
    /// </summary>
    [Authorize]
    public class MenuButtonController : BaseController
    {
        #region 构造函数
        private readonly ISystemMenuButtonLogic _menuButtonLogic;
        private readonly ISystemMenuButtonFunctionLogic _menuButtonFunctionLogic;
        private readonly ISystemMenuLogic _menuLogic;
        public MenuButtonController(ISystemMenuButtonLogic menuButtonLogic,
            ISystemMenuButtonFunctionLogic menuButtonFunctionLogic,
            ISystemMenuLogic menuLogic)
        {
            _menuButtonLogic = menuButtonLogic;
            _menuButtonFunctionLogic = menuButtonFunctionLogic;
            _menuLogic = menuLogic;
        }

        #endregion
        
        #region 方法

        /// <summary>
        ///     根据菜单Id获取模块按钮信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("界面按钮-方法-列表-根据菜单Id获取按钮信息")]
        public async Task<JsonResult> GetMenuButtonByMenuId(SystemMenuGetMenuButtonByMenuIdInput input)
        {
            return JsonForGridLoadOnce(await _menuButtonLogic.GetMenuButtonByMenuId(input));
        }

        /// <summary>
        ///     保存按钮信息
        /// </summary>
        /// <param name="function">模块按钮信息</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("界面按钮-方法-保存")]
        public async Task<JsonResult> SaveMenuButton(SystemMenuButtonSaveInput function)
        {
            return Json(await _menuButtonLogic.SaveMenuButton(function));
        }

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("界面按钮-方法-删除")]
        public async Task<JsonResult> DeleteMenuButton(IdInput input)
        {
            return Json(await _menuButtonLogic.DeleteMenuButton(input));
        }

        /// <summary>
        /// 获取该菜单按钮下对应的模块按钮
        /// </summary>
        /// <param name="input">菜单Id</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("界面按钮-方法-获取该菜单按钮下已关联的模块按钮")]
        public async Task<JsonResult> GetMenuButtonFunctions(IdInput input)
        {
            return Json(await _menuButtonFunctionLogic.GetMenuButtonFunctions(input));
        }

        /// <summary>
        /// 获取所有模块按钮信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("界面按钮-方法-获取所有菜单按钮模块按钮关联信息")]
        public async Task<JsonResult> GetAllFunctions(SystemMenuButtonGetFunctionsInput input)
        {
            input.IsPage = true;
            return Json(await _menuButtonFunctionLogic.GetAllFunctions(input));
        }

        /// <summary>
        /// 保存菜单按钮模块按钮
        /// </summary>
        /// <param name="menuButtonFunctions">菜单按钮模块按钮</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("界面按钮-方法-保存菜单按钮模块按钮关联")]
        public async Task<JsonResult> SaveMenuButtonFunction(string menuButtonFunctions)
        {
            //return Json(await _menuButtonFunctionLogic.SaveMenuButtonFunction(menuButtonFunctions.JsonStringToList<SystemMenuButtonFunction>()));
            return null;
        }

        /// <summary>
        /// 删除菜单按钮模块按钮关联
        /// </summary>
        /// <param name="menuButtonFunction"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("界面按钮-方法-删除菜单按钮模块按钮关联")]
        public async Task<JsonResult> DeleteMenuButtonFunction(SystemMenuButtonFunction menuButtonFunction)
        {
            return Json(await _menuButtonFunctionLogic.DeleteMenuButtonFunction(menuButtonFunction));
        }

        /// <summary>
        /// 删除菜单按钮模块按钮关联
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("界面按钮-编辑")]
        public async Task<JsonResult> GetById(IdInput input)
        {
            var button = await _menuButtonLogic.GetByIdAsync(input.Id);
            var output = button.MapTo<SystemMenuButtonOutput>();
            //获取菜单信息
            var parentInfo = await _menuLogic.GetByIdAsync(output.MenuId);
            if (parentInfo != null)
            {
                output.MenuName = parentInfo.Name;
            }
            return Json(output);
        }
        #endregion
    }
}