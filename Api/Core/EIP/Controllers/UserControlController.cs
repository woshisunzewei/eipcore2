using EIP.Common.Core.Utils;
using EIP.Common.Entities;
using EIP.Common.Restful;
using EIP.Common.Restful.Attribute;
using EIP.System.Business.Config;
using EIP.System.Business.Identity;
using EIP.System.Business.Permission;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EIP.Controllers
{
    /// <summary>
    ///     用户控件控制器
    /// </summary>
    [Authorize]
    public class UserControlController : BaseController
    {
        #region 构造函数
        private readonly ISystemDictionaryLogic _dictionaryLogic;
        private readonly ISystemGroupLogic _groupLogic;
        private readonly ISystemPostLogic _postLogic;
        private readonly ISystemUserInfoLogic _userInfoLogic;
        private readonly ISystemPermissionUserLogic _permissionUserLogic;
        private readonly ISystemOrganizationLogic _organizationLogic;
        private readonly ISystemMenuLogic _menuLogic;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupLogic"></param>
        /// <param name="postLogic"></param>
        /// <param name="userInfoLogic"></param>
        /// <param name="permissionUserLogic"></param>
        /// <param name="organizationLogic"></param>
        /// <param name="menuLogic"></param>
        /// <param name="dictionaryLogic"></param>
        public UserControlController(
            ISystemGroupLogic groupLogic,
            ISystemPostLogic postLogic,
            ISystemUserInfoLogic userInfoLogic,
            ISystemPermissionUserLogic permissionUserLogic,
            ISystemOrganizationLogic organizationLogic,
            ISystemMenuLogic menuLogic,
            ISystemDictionaryLogic dictionaryLogic)
        {
            _groupLogic = groupLogic;
            _postLogic = postLogic;
            _menuLogic = menuLogic;
            _userInfoLogic = userInfoLogic;
            _permissionUserLogic = permissionUserLogic;
            _organizationLogic = organizationLogic;
            _dictionaryLogic = dictionaryLogic;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     读取树结构:排除下级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("用户控件-方法-选择所有下级字典:排除下级")]
        public async Task<JsonResult> GetDictionaryRemoveChildren(Guid? id = null)
        {
            return JsonForJstreeRemoveChildren((await _dictionaryLogic.GetDictionaryTree()).ToList(), id);
        }

        /// <summary>
        ///     读取树结构:排除下级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("用户控件-方法-读取组织机构树:排除下级")]
        public async Task<JsonResult> GetOrganizationTreeRemoveChildren(Guid? id = null)
        {
            return JsonForJstreeRemoveChildren((await _organizationLogic.GetOrganizationTree()).ToList(), id);
        }

        /// <summary>
        ///     读取树结构:排除下级
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="isRemove"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("用户控件-方法-读取菜单树结构:排除下级")]
        public async Task<JsonResult> GetMenuRemoveChildren(Guid? menuId = null,
           bool isRemove = true)
        {
            return isRemove
                ? JsonForJstreeRemoveChildren((await _menuLogic.GetAllMenuTree()).ToList(), menuId)
                : JsonForJsTree(await _menuLogic.GetAllMenuTree());
        }

        /// <summary>
        /// 获取枚举数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("用户控件-方法-获取枚举数据")]
        public JsonResult GetEnumNameValue(EnumFromType type)
        {
            Dictionary<Int32, String> dictionary = new Dictionary<Int32, String>();
            switch (type)
            {
                case EnumFromType.组织机构性质:
                    dictionary = EnumUtil.EnumToDictionary(typeof(EnumOrgNature), e => e.ToString());
                    break;
                case EnumFromType.组织机构状态:
                    dictionary = EnumUtil.EnumToDictionary(typeof(EnumOrgState), e => e.ToString());
                    break;
                case EnumFromType.人员状态:
                    dictionary = EnumUtil.EnumToDictionary(typeof(EnumUserNature), e => e.ToString());
                    break;
                case EnumFromType.角色状态:
                    dictionary = EnumUtil.EnumToDictionary(typeof(EnumRoleState), e => e.ToString());
                    break;
                case EnumFromType.省市区县:
                    dictionary = EnumUtil.EnumToDictionary(typeof(EnumDistrictLevelType), e => e.ToString());
                    break;
               
            }
            IList<NameValue<int>> nameValue = new List<NameValue<int>>();
            foreach (var dic in dictionary)
            {
                nameValue.Add(new NameValue<int>(dic.Value, dic.Key));
            }
            return Json(nameValue);
        }

        /// <summary>
        ///     分页获取所有用户信息
        /// </summary>
        /// <param name="paging">用户信息分页参数</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("用户控件-方法-分页获取所有具有该权限的用户信息")]
        public async Task<JsonResult> GetPagingPrivilegeMasterUser(SystemUserPagingInput paging)
        {
            return JsonForGridPaging(await _userInfoLogic.PagingUserQuery(paging));
        }

        /// <summary>
        ///     查看具有特权的人员
        /// </summary>
        /// <param name="privilegeMaster">归属人员类型:企业、角色、岗位、组</param>
        /// <param name="privilegeMasterValue">企业Id、角色Id、岗位Id、组Id</param>
        /// <returns></returns>
        [CreateBy("孙泽伟")]
        [Remark("用户控件-视图-查看具有特权的人员")]
        [HttpPost]
        public async Task<JsonResult> GetChosenPrivilegeMasterUser(EnumPrivilegeMaster privilegeMaster,
            Guid privilegeMasterValue)
        {
            //获取所有人员信息
            IList<SystemChosenUserOutput> chosenUserDtos = (await _userInfoLogic.GetChosenUser()).ToList();
            //获取所有的用户
            var permissions =(
                await
                    _permissionUserLogic.GetPermissionUsersByPrivilegeMasterAdnPrivilegeMasterValue(privilegeMaster,
                        privilegeMasterValue)).ToList();
            foreach (var dto in chosenUserDtos)
            {
                var permission = permissions.Where(w => w.PrivilegeMasterUserId == dto.UserId).FirstOrDefault();
                dto.Exist = permission != null;
            }
            return Json(chosenUserDtos.OrderByDescending(w => w.Exist).ToList());
        }

        /// <summary>
        ///     保存用户信息
        /// </summary>
        /// <param name="privilegeMasterUser">用户json字符串</param>
        /// <param name="privilegeMasterValue">角色信息</param>
        /// <param name="privilegeMaster">归属人员类型:企业、角色、岗位、组</param>
        /// <returns></returns>
        [CreateBy("孙泽伟")]
        [Remark("用户控件-方法-保存权限用户信息")]
        [HttpPost]
        public async Task<JsonResult> SavePrivilegeMasterUser(string privilegeMasterUser,
            Guid privilegeMasterValue,
            EnumPrivilegeMaster privilegeMaster)
        {
            IList<SystemPermissionSaveUserInput> models = JsonConvert.DeserializeObject<IList<SystemPermissionSaveUserInput>>(privilegeMasterUser);
            IList<Guid> users = models.Select(m => m.U).ToList();
            return
                Json(
                    await
                        _permissionUserLogic.SavePermissionUserBeforeDelete(privilegeMaster, privilegeMasterValue, users));
        }

        /// <summary>
        ///     删除用户
        /// </summary>
        /// <param name="privilegeMasterUserId">用户Id</param>
        /// <param name="privilegeMasterValue">归属类型Id:企业、角色、岗位、组</param>
        /// <param name="privilegeMaster">归属人员类型:企业、角色、岗位、组</param>
        /// <returns></returns>
        [CreateBy("孙泽伟")]
        [Remark("用户控件-方法-删除权限用户信息")]
        [HttpPost]
        public async Task<JsonResult> DeletePrivilegeMasterUser(Guid privilegeMasterUserId,
            Guid privilegeMasterValue,
            EnumPrivilegeMaster privilegeMaster)
        {
            return
                Json(await _permissionUserLogic.DeletePrivilegeMasterUser(privilegeMasterUserId, privilegeMasterValue,
                    privilegeMaster));
        }
        #endregion
    }
}