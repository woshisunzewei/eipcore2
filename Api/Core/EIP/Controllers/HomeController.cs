using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EIP.Common.Restful.Attribute;
using EIP.Common.Core.Auth;
using EIP.Common.Core.Extensions;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Tree;
using EIP.Common.Restful;
using EIP.Common.Restful.Extension;
using EIP.System.Business.Identity;
using EIP.System.Business.Permission;
using EIP.System.Models.Dtos.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIP.Controllers
{
    /// <summary>
    /// 主页
    /// </summary>
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ISystemUserInfoLogic _userInfoLogic;
        private readonly ISystemPermissionLogic _permissionLogic;
        private readonly StringBuilder _permissionStr = new StringBuilder();
        private readonly PrincipalUser _currentUser;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissionLogic"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="userInfoLogic"></param>
        public HomeController(ISystemPermissionLogic permissionLogic, 
            IHttpContextAccessor httpContextAccessor, 
            ISystemUserInfoLogic userInfoLogic)
        {
            _currentUser = httpContextAccessor.CurrentUser();
            _permissionLogic = permissionLogic;
            _userInfoLogic = userInfoLogic;
        }

        /// <summary>
        ///     加载模块权限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("首页-方法-获取菜单权限")]
        public async Task<string> LoadMenuPermission()
        {
            //所有权限
            var permissions = (await _permissionLogic.GetSystemPermissionMenuByUserId(_currentUser.UserId)).ToList();
            //所有第一级
            var firstPermissions = permissions.Where(f => f.parent.ToString() == Guid.Empty.ToString()).ToList();
            foreach (var permission in firstPermissions)
            {
                _permissionStr.Append(GetChildNodes(permissions, permission));
            }
            return _permissionStr.ToString();
        }

        /// <summary>
        ///     根据当前节点，加载子节点
        /// </summary>
        /// <param name="treeEntitys">TreeEntity的集合</param>
        /// <param name="currTreeEntity">当前节点</param>
        private string GetChildNodes(IList<JsTreeEntity> treeEntitys,
            JsTreeEntity currTreeEntity)
        {
            StringBuilder childTrees = new StringBuilder();
            //当前节点是否还有下级
            IList<JsTreeEntity> childNodes = treeEntitys.Where(f => f.parent.ToString() == currTreeEntity.id.ToString()).ToList();
            if (childNodes.Count <= 0)
            {
                childTrees.Append($@" <li>
                            <a href='{(currTreeEntity.url.IsNullOrEmpty() ? "#" : currTreeEntity.url)}' class='ajaxify'><i class='fa {
                        (currTreeEntity.icon.IsNullOrEmpty() ? "fa-circle-o" : currTreeEntity.icon)
                    }'></i>{currTreeEntity.text}</a>
                        </li>");
            }
            else
            {
                childTrees.Append(string.Format(@"<li {3}>
                            <a class='menuParent' class='ajaxify' href='{0}' open-type='{4}'>
                                <i class='fa {1}'></i><span>{2}</span> 
                                <span class='pull-right-container'>
                                    <i class='fa fa-angle-left pull-right'></i>
                                </span>
                            </a><ul class='treeview-menu'>"
                    , currTreeEntity.url.IsNullOrEmpty() ? "#" : currTreeEntity.url, currTreeEntity.icon.IsNullOrEmpty() ? "fa-tasks" : currTreeEntity.icon, currTreeEntity.text, currTreeEntity.parent.ToString() == Guid.Empty.ToString() ? "class='treeview'" : "", ""));
                //下面还有值
                foreach (var treeEntity in childNodes)
                {
                    childTrees.Append(GetChildNodes(treeEntitys, treeEntity));
                }
                childTrees.Append("</ul></li>");
            }
            return childTrees.ToString();
        }

        /// <summary>
        ///     保存修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("主界面-方法-保存修改后密码信息")]
        public async Task<JsonResult> SaveChangePassword(SystemUserChangePasswordInput input)
        {
            input.Id = _currentUser.UserId;
            return Json(await _userInfoLogic.SaveChangePassword(input));
        }

        /// <summary>
        ///     验证旧密码是否输入正确
        /// </summary>
        /// <param name="input">需要验证的参数</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("主界面-方法-验证旧密码是否输入正确")]
        public async Task<JsonResult> CheckOldPassword(CheckSameValueInput input)
        {
            input.Id = _currentUser.UserId;
            return Json(await _userInfoLogic.CheckOldPassword(input));
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult LoginOut()
        {
            return Json(new
            {

            });
        }
    }
}