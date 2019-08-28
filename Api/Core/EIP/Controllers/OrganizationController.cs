using System.Linq;
using EIP.Common.Core.Auth;
using EIP.Common.Core.Extensions;
using EIP.Common.Entities.Dtos;
using EIP.Common.Restful;
using EIP.Common.Restful.Attribute;
using EIP.Common.Restful.Extension;
using EIP.System.Business.Identity;
using EIP.System.Business.Permission;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using EIP.System.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EIP.Controllers
{
    /// <summary>
    ///     组织机构
    /// </summary>
    [Authorize]
    public class OrganizationController : BaseController
    {
        #region 构造函数
        private readonly ISystemOrganizationLogic _organizationLogic;
        private readonly PrincipalUser _currentUser;
        private readonly ISystemPermissionLogic _permissionLogic;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizationLogic"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="permissionLogic"></param>
        public OrganizationController(ISystemOrganizationLogic organizationLogic,
            IHttpContextAccessor httpContextAccessor,
            ISystemPermissionLogic permissionLogic)
        {
            _currentUser = httpContextAccessor.CurrentUser();
            _organizationLogic = organizationLogic;
            _permissionLogic = permissionLogic;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     读取组织机构树
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("组织机构维护-方法-列表-读取组织机构树")]
        //[Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetOrganizationTree()
        {
            return JsonForJsTree(await _organizationLogic.GetOrganizationTree());
        }

        /// <summary>
        ///     读取组织机构树
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("组织机构维护-方法-列表-读取组织机构树")]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetOrganizationChart()
        {
            return Json((await _organizationLogic.GetOrganizationChart()).First());
        }

        /// <summary>
        ///     读取组织机构树
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("组织机构维护-方法-列表-读取组织机构树")]
        public async Task<JsonResult> GetOrganizationDataTree()
        {
            #region 获取权限控制器信息
            SystemPermissionSqlInput permissionInput = new SystemPermissionSqlInput
            {
                PrincipalUser = _currentUser,
                EnumPermissionRoteConvert = EnumPermissionRoteConvert.组织机构数据权限
            };
            SystemOrganizationDataPermissionTreeInput input =
                new SystemOrganizationDataPermissionTreeInput
                {
                    PrincipalUser = _currentUser,
                    DataSql = await _permissionLogic.GetDataPermissionSql(permissionInput)
                };
            #endregion
            return Json(await _organizationLogic.GetOrganizationDataPermissionTree(input));
        }

        /// <summary>
        ///     读取组织机构树
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("组织机构维护-方法-新增/编辑-读取组织机构树")]
        public async Task<JsonResult> GetOrganizationsByParentId(SystemOrganizationDataPermissionTreeInput input)
        {
            #region 获取权限控制器信息
            SystemPermissionSqlInput permissionInput = new SystemPermissionSqlInput
            {
                PrincipalUser = _currentUser,
                EnumPermissionRoteConvert = EnumPermissionRoteConvert.组织机构数据权限
            };
            input.PrincipalUser = _currentUser;
            input.DataSql = await _permissionLogic.GetDataPermissionSql(permissionInput);

            #endregion
            return JsonForGridLoadOnce(await _organizationLogic.GetOrganizationsByParentId(input));
        }

        /// <summary>
        ///     保存组织机构数据
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("组织机构维护-方法-新增/编辑-保存")]
        public async Task<JsonResult> SaveOrganization(SystemOrganization organization)
        {
            organization.CreateUserId = _currentUser.UserId;
            organization.CreateUserName = _currentUser.Name;
            return Json(await _organizationLogic.SaveOrganization(organization));
        }

        /// <summary>
        ///     删除组织机构
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("组织机构维护-方法-列表-删除")]
        public async Task<JsonResult> DeleteOrganization(IdInput input)
        {
            return Json(await _organizationLogic.DeleteOrganization(input));
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
            var menu = await _organizationLogic.GetByIdAsync(input.Id);
            var orgOutput = menu.MapTo<SystemOrganizationOutput>();
            //获取父级信息
            var parentInfo = await _organizationLogic.GetByIdAsync(orgOutput.ParentId);
            if (parentInfo != null)
            {
                orgOutput.ParentName = parentInfo.Name;
            }
            return Json(orgOutput);
        }
        #endregion
    }
}