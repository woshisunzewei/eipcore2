using EIP.Common.Core.Auth;
using EIP.Common.Entities.Dtos;
using EIP.Common.Restful;
using EIP.Common.Restful.Attribute;
using EIP.Common.Restful.Extension;
using EIP.System.Business.Identity;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EIP.Common.Core.Extensions;

namespace EIP.Controllers
{
    /// <summary>
    ///     岗位控制器
    /// </summary>
    [Authorize]
    public class PostController : BaseController
    {
        #region 构造函数
        private readonly ISystemPostLogic _postLogic;
        private readonly PrincipalUser _currentUser;
        private readonly ISystemOrganizationLogic _organizationLogic;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postLogic"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="organizationLogic"></param>
        public PostController(ISystemPostLogic postLogic, IHttpContextAccessor httpContextAccessor, ISystemOrganizationLogic organizationLogic)
        {
            _currentUser = httpContextAccessor.CurrentUser();
            _postLogic = postLogic;
            _organizationLogic = organizationLogic;
        }

        #endregion
        
        #region 方法
        /// <summary>
        /// 根据组织机构获取岗位信息
        /// </summary>
        /// <param name="input">组织机构Id</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("岗位维护-方法-列表-根据组织机构获取岗位信息")]
        public async Task<JsonResult> GetPostByOrganizationId(SystemPostGetByOrganizationId input)
        {
            return JsonForGridLoadOnce(await _postLogic.GetPostByOrganizationId(input));
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
            var role = await _postLogic.GetByIdAsync(input.Id);
            var output = role.MapTo<SystemPostOutput>();
            var org = (await _organizationLogic.GetByIdAsync(role.OrganizationId));
            if (org != null)
            {
                output.OrganizationName = org.Name;
            }
            return Json(output);
        }

        /// <summary>
        ///     保存岗位数据
        /// </summary>
        /// <param name="post">岗位信息</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("岗位维护-方法-新增/编辑-保存")]
        public async Task<JsonResult> SavePost(SystemPost post)
        {
            post.CreateUserId = _currentUser.UserId;
            post.CreateUserName = _currentUser.Name;
            return Json(await _postLogic.SavePost(post));
        }

        /// <summary>
        ///     复制
        /// </summary>
        /// <param name="input">复制信息</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("岗位维护-方法-列表-复制")]
        public async Task<JsonResult> CopyPost(SystemCopyInput input)
        {
            input.CreateUserId = _currentUser.UserId;
            input.CreateUserName = _currentUser.Name;
            return Json(await _postLogic.CopyPost(input));
        }

        /// <summary>
        ///    删除岗位数据
        /// </summary>
        /// <param name="input">岗位Id</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("岗位维护-方法-列表-删除")]
        public async Task<JsonResult> DeletePost(IdInput input)
        {
            return Json(await _postLogic.DeletePost(input));
        }
        #endregion
    }
}