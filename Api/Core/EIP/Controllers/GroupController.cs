using EIP.Common.Core.Auth;
using EIP.Common.Entities.Dtos;
using EIP.Common.Restful;
using EIP.Common.Restful.Attribute;
using EIP.Common.Restful.Extension;
using EIP.System.Business.Identity;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Entities;
using EIP.System.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EIP.Common.Core.Extensions;

namespace EIP.Controllers
{
    /// <summary>
    ///     组管理控制器
    /// </summary>
    [Authorize]
    public class GroupController : BaseController
    {
        #region 构造函数

        private readonly ISystemGroupLogic _groupLogic;
        private readonly ISystemUserInfoLogic _userInfoLogic;
        private readonly ISystemOrganizationLogic _organizationLogic;
        private readonly PrincipalUser _currentUser;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupLogic"></param>
        /// <param name="userInfoLogic"></param>
        /// <param name="organizationLogic"></param>
        /// <param name="httpContextAccessor"></param>
        public GroupController(ISystemGroupLogic groupLogic,
            ISystemUserInfoLogic userInfoLogic, ISystemOrganizationLogic organizationLogic, IHttpContextAccessor httpContextAccessor)
        {
            _currentUser = httpContextAccessor.CurrentUser();
            _groupLogic = groupLogic;
            _userInfoLogic = userInfoLogic;
            _organizationLogic = organizationLogic;
        }

        #endregion
      
        #region 方法

        /// <summary>
        ///     根据组织机构Id获取对应下的组信息:id为空查询所有
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("组维护-方法-列表-根据组织机构Id获取对应下的组信息:id为空查询所有")]
        public async Task<JsonResult> GetGroupByOrganizationId(SystemGroupGetGroupByOrganizationIdInput input)
        {
            return JsonForGridLoadOnce(await _groupLogic.GetGroupByOrganizationId(input));
        }

        /// <summary>
        ///     保存组数据
        /// </summary>
        /// <param name="group">组信息</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("组维护-方法-新增/编辑-保存")]
        public async Task<JsonResult> SaveGroup(SystemGroup group)
        {
            group.CreateUserId = _currentUser.UserId;
            group.CreateUserName = _currentUser.Name;
            return Json(await _groupLogic.SaveGroup(group, EnumGroupBelongTo.系统));
        }

        /// <summary>
        /// 编辑/修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("岗位维护-方法-列表-根据id获取值")]
        public async Task<JsonResult> GetById(IdInput input)
        {
            var group = await _groupLogic.GetByIdAsync(input.Id);
            var output = group.MapTo<SystemGroupOutput>();
            var org = (await _organizationLogic.GetByIdAsync(group.OrganizationId));
            if (org != null)
            {
                output.OrganizationName = org.Name;
            }
            return Json(output);
        }

        /// <summary>
        ///     删除组数据
        /// </summary>
        /// <param name="input">组Id</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("组维护-方法-新增/编辑-删除")]
        public async Task<JsonResult> DeleteGroup(IdInput input)
        {
            return Json(await _groupLogic.DeleteGroup(input));
        }

        /// <summary>
        ///     分页获取所有用户信息
        /// </summary>
        /// <param name="paging">用户信息分页参数</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("组维护-方法-列表-分页获取所有用户信息")]
        public async Task<JsonResult> GetPagingUser(SystemUserPagingInput paging)
        {
            paging.PrivilegeMaster = EnumPrivilegeMaster.岗位;
            return JsonForGridPaging(await _userInfoLogic.PagingUserQuery(paging));
        }

        /// <summary>
        ///     复制
        /// </summary>
        /// <param name="input">复制信息</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("岗位维护-方法-列表-复制")]
        public async Task<JsonResult> CopyGroup(SystemCopyInput input)
        {
            input.CreateUserId = _currentUser.UserId;
            input.CreateUserName = _currentUser.Name;
            return Json(await _groupLogic.CopyGroup(input));
        }
        #endregion
    }
}