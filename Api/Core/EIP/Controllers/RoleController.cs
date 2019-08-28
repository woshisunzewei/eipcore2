using EIP.Common.Core.Auth;
using EIP.Common.Core.Extensions;
using EIP.Common.Entities.Dtos;
using EIP.Common.Restful;
using EIP.Common.Restful.Attribute;
using EIP.Common.Restful.Extension;
using EIP.System.Business.Identity;
using EIP.System.Business.Permission;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Entities;
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
    ///     角色控制器
    /// </summary>
    [Authorize]
    public class RoleController : BaseController
    {
        #region 构造函数

        private readonly ISystemRoleLogic _roleLogic;
        private readonly ISystemPermissionUserLogic _permissionUserLogic;
        private readonly ISystemOrganizationLogic _organizationLogic;
        private readonly PrincipalUser _currentUser;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleLogic"></param>
        /// <param name="permissionUserLogic"></param>
        /// <param name="organizationLogic"></param>
        /// <param name="httpContextAccessor"></param>
        public RoleController(ISystemRoleLogic roleLogic,
            ISystemPermissionUserLogic permissionUserLogic,
            ISystemOrganizationLogic organizationLogic, 
            IHttpContextAccessor httpContextAccessor)
        {
            _currentUser = httpContextAccessor.CurrentUser();
            _roleLogic = roleLogic;
            _permissionUserLogic = permissionUserLogic;
            _organizationLogic = organizationLogic;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     保存用户角色信息
        /// </summary>
        /// <param name="userRole">角色json字符串</param>
        /// <param name="userId">用户信息</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("角色维护-方法-新增/编辑-保存用户角色信息")]
        public async Task<JsonResult> SaveUserRole(string userRole,
            Guid userId)
        {
            IList<SystemRoleUserSaveInput> models =
                JsonConvert.DeserializeObject<IList<SystemRoleUserSaveInput>>(userRole);
            IList<Guid> roles = models.Select(m => m.R).ToList();
            return Json(
                await _permissionUserLogic.SavePermissionMasterValueBeforeDelete(EnumPrivilegeMaster.角色, userId,
                    roles));
        }

        /// <summary>
        ///     根据组织机构获取角色信息
        /// </summary>
        /// <param name="input">组织机构主键Id</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("角色维护-方法-列表-根据组织机构获取角色信息")]
        public async Task<JsonResult> GetRolesByOrganization(SystemRolesGetByOrganizationId input)
        {
            return JsonForGridLoadOnce(await _roleLogic.GetRolesByOrganizationId(input));
        }

        /// <summary>
        ///     获取角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("用户控件-方法-获取用户")]
        public async Task<JsonResult> GetChosenRoles(IdInput input)
        {
            return Json(await _roleLogic.GetChosenRoles(input));
        }

        /// <summary>
        ///     保存角色数据
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("角色维护-方法-新增/编辑-保存")]
        public async Task<JsonResult> SaveRole(SystemRole role)
        {
            role.CreateUserId = _currentUser.UserId;
            role.CreateUserName = _currentUser.Name;
            return Json(await _roleLogic.SaveRole(role));
        }

        /// <summary>
        ///     删除角色数据
        /// </summary>
        /// <param name="input">角色Id</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("角色维护-方法-列表-删除")]
        public async Task<JsonResult> DeleteRole(IdInput input)
        {
            return Json(await _roleLogic.DeleteRole(input));
        }

        /// <summary>
        ///     角色复制
        /// </summary>
        /// <param name="input">角色Id</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("角色维护-方法-列表-角色复制")]
        public async Task<JsonResult> CopyRole(SystemCopyInput input)
        {
            input.CreateUserId = _currentUser.UserId;
            input.CreateUserName = _currentUser.Name;
            return Json(await _roleLogic.CopyRole(input));
        }

        /// <summary>
        /// 编辑/修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("角色维护-方法-列表-根据id获取值")]
        public async Task<JsonResult> GetById(IdInput input)
        {
            var role = await _roleLogic.GetByIdAsync(input.Id);
            var output = role.MapTo<SystemRoleOutput>();
            var org = (await _organizationLogic.GetByIdAsync(role.OrganizationId));
            if (org != null)
            {
                output.OrganizationName = org.Name;
            }
            return Json(output);
        }
        #endregion
    }
}

