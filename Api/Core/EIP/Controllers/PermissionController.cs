using EIP.Common.Core.Auth;
using EIP.Common.Entities;
using EIP.Common.Restful;
using EIP.Common.Restful.Attribute;
using EIP.Common.Restful.Extension;
using EIP.System.Business.Permission;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIP.Controllers
{
    /// <summary>
    ///     权限控制器
    /// </summary>
    [Authorize]
    public class PermissionController : BaseController
    {
        #region 构造函数

        private readonly ISystemPermissionLogic _permissionLogic;
        private readonly PrincipalUser _currentUser;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissionLogic"></param>
        /// <param name="httpContextAccessor"></param>
        public PermissionController(ISystemPermissionLogic permissionLogic, 
            IHttpContextAccessor httpContextAccessor)
        {
            _currentUser = httpContextAccessor.CurrentUser();
            _permissionLogic = permissionLogic;
        }

        #endregion
        
        /// <summary>
        ///     根据特权Id获取菜单权限信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("模块权限-方法-列表-根据特权Id获取菜单权限信息")]
        public async Task<JsonResult> GetPermissionByPrivilegeMasterValue(SystemPermissionByPrivilegeMasterValueInput input)
        {
            return Json(await _permissionLogic.GetPermissionByPrivilegeMasterValue(input));
        }

        /// <summary>
        ///     获取所有模块按钮
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("模块按钮权限-视图-获取所有模块按钮")]
        public async Task<JsonResult> GetFunctionByPrivilegeMaster(Guid privilegeMasterValue,
            EnumPrivilegeMaster privilegeMaster)
        {
            return Json((await _permissionLogic.GetFunctionByPrivilegeMaster(privilegeMasterValue, privilegeMaster)).ToList());
        }

        /// <summary>
        ///     模块按钮权限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("模块按钮权限-视图-列表")]
        public async Task<JsonResult> GetDataByPrivilegeMaster(Guid privilegeMasterValue,
            EnumPrivilegeMaster privilegeMaster)
        {
            //获取所有模块按钮
            return Json((await _permissionLogic.GetDataByPrivilegeMaster(privilegeMasterValue, privilegeMaster)).ToList());
        }
        #region 公用

        /// <summary>
        ///     根据角色Id,岗位Id,组Id,人员Id获取具有的菜单信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns>树形菜单信息</returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("系统权限公用-方法-列表-根据角色Id,岗位Id,组Id,人员Id获取具有的菜单信息")]
        public async Task<JsonResult> GetMenuHavePermissionByPrivilegeMasterValue(SystemPermissiontMenuHaveByPrivilegeMasterValueInput input)
        {
            return JsonForJsTree((await _permissionLogic.GetMenuHavePermissionByPrivilegeMasterValue(input)).ToList());
        }

        /// <summary>
        ///     保存权限
        /// </summary>
        /// <param name="input">权限类型:菜单、模块按钮</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("系统权限公用-方法-保存权限")]
        public async Task<JsonResult> SavePermission(SystemPermissionSaveInput input)
        {
            input.Permissiones = JsonConvert.DeserializeObject<IList<SystemPermissionSaveConvertInput>>(input.MenuPermissions).Select(m => m.P).ToList();
            return Json(await _permissionLogic.SavePermission(input));
        }

        /// <summary>
        ///     "系统权限公用-方法-获取菜单功能项信息"
        /// </summary>
        /// <param name="mvcRote">权限类型:菜单、模块按钮</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("系统权限公用-方法-获取菜单功能项信息")]
        public async Task<JsonResult> GetFunctionByMenuIdAndUserId(MvcRote mvcRote)
        {
            return Json(await _permissionLogic.GetFunctionByMenuIdAndUserId(mvcRote, _currentUser.UserId));
        }
        #endregion
    }
}