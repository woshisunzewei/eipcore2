using EIP.Common.Core.Extensions;
using EIP.Common.Entities.Dtos;
using EIP.Common.Restful;
using EIP.Common.Restful.Attribute;
using EIP.System.Business.Permission;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EIP.Controllers
{
    /// <summary>
    ///     模块维护
    /// </summary>
    [Authorize]
    public class MenuController : BaseController
    {
        #region 构造函数

        private readonly ISystemMenuLogic _menuLogic;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuLogic"></param>
        public MenuController(ISystemMenuLogic menuLogic)
        {
            _menuLogic = menuLogic;
        }

        #endregion

        #region 方法
       
        /// <summary>
        ///     获取所有菜单信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("模块维护-方法-列表-获取所有菜单信息")]
        public async Task<JsonResult> GetAllMenuTree()
        {
            return JsonForJsTree(await _menuLogic.GetAllMenuTree());
        }

        /// <summary>
        ///     根据父级Id获取下级菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("模块维护-方法-列表-根据父级Id获取下级菜单")]
        public async Task<JsonResult> GetMenuByParentId(SystemMenuGetMenuByParentIdInput input)
        {
            return JsonForGridLoadOnce(await _menuLogic.GetMenuByParentId(input));
        }

        /// <summary>
        ///     保存
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("模块维护-方法-新增/编辑-保存")]
        public async Task<JsonResult> SaveMenu(SystemMenu menu)
        {
            return Json(await _menuLogic.SaveMenu(menu));
        }

        /// <summary>
        ///     删除菜单数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("模块维护-方法-列表-删除")]
        public async Task<JsonResult> DeleteMenu(IdInput input)
        {
            return Json(await _menuLogic.DeleteMenu(input));
        }

        /// <summary>
        ///     删除菜单数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("模块维护-方法-列表-删除")]
        public async Task<JsonResult> DeleteMenuAndChilds(IdInput<string> input)
        {
            return Json(await _menuLogic.DeleteMenuAndChilds(input));
        }

        /// <summary>
        ///     查询所有具有菜单权限的菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("模块维护-方法-列表-查询所有具有菜单权限的菜单")]
        public async Task<JsonResult> GetHaveMenuPermissionMenu()
        {
            return JsonForJsTree((await _menuLogic.GetHaveMenuPermissionMenu()).ToList());
        }
      
        /// <summary>
        ///     查询所有具有数据权限的菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("模块维护-方法-列表-查询所有具有数据权限的菜单")]
        public async Task<JsonResult> GetHaveDataPermissionMenu()
        {
            return JsonForJsTree((await _menuLogic.GetHaveDataPermissionMenu()).ToList());
        }

        /// <summary>
        ///     查询所有具有字段权限的菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("模块维护-方法-列表-查询所有具有字段权限的菜单")]
        public async Task<JsonResult> GetHaveFieldPermissionMenu()
        {
            return JsonForJsTree((await _menuLogic.GetHaveFieldPermissionMenu()).ToList());
        }

        /// <summary>
        ///     查询所有具有模块按钮的菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("模块维护-方法-列表-查询所有具有模块按钮的菜单")]
        public async Task<JsonResult> GetHaveMenuButtonPermissionMenu()
        {
            return JsonForJsTree((await _menuLogic.GetHaveMenuButtonPermissionMenu()).ToList());
        }

        /// <summary>
        /// 编辑/修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("模块维护-方法-列表-根据id获取值")]
        public async Task<JsonResult> GetById(IdInput input)
        {
            var menuDto = new SystemMenuEditOutput();
            //如果为编辑
            if (!input.Id.IsEmptyGuid())
            {
                var menu = await _menuLogic.GetByIdAsync(input.Id);
                menuDto = menu.MapTo<SystemMenuEditOutput>();
                //获取父级信息
                var parentInfo = await _menuLogic.GetByIdAsync(menuDto.ParentId);
                if (parentInfo != null)
                {
                    menuDto.ParentName = parentInfo.Name;
                }
            }
            return Json(menuDto);
        }
        #endregion
    }
}