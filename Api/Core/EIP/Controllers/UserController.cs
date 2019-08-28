using EIP.Common.Core.Auth;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Dtos.Reports;
using EIP.Common.Restful;
using EIP.Common.Restful.Attribute;
using EIP.Common.Restful.Extension;
using EIP.System.Business.Identity;
using EIP.System.Business.Permission;
using EIP.System.Models.Dtos.Identity;
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
    ///     用户管理:此用户为系统使用人员,系统的人员管理在其他模块进行管理(如:人事管理HR)
    ///     此模块只维护基础信息
    /// </summary>
    [Authorize]
    public class UserController : BaseController
    {
        #region 构造函数
        private readonly ISystemPermissionUserLogic _permissionUserLogic;
        private readonly ISystemUserInfoLogic _userInfoLogic;
        private readonly ISystemOrganizationLogic _organizationLogic;
        private readonly PrincipalUser _currentUser;
        private readonly ISystemPermissionLogic _permissionLogic;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfoLogic"></param>
        /// <param name="permissionUserLogic"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="organizationLogic"></param>
        /// <param name="permissionLogic"></param>
        public UserController(ISystemUserInfoLogic userInfoLogic,
            ISystemPermissionUserLogic permissionUserLogic,
            IHttpContextAccessor httpContextAccessor, 
            ISystemOrganizationLogic organizationLogic,
            ISystemPermissionLogic permissionLogic)
        {
            _currentUser = httpContextAccessor.CurrentUser();
            _permissionUserLogic = permissionUserLogic;
            _organizationLogic = organizationLogic;
            _permissionLogic = permissionLogic;
            _userInfoLogic = userInfoLogic;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     分页获取所有用户信息
        /// </summary>
        /// <param name="paging">用户信息分页参数</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("用户维护-方法-列表-分页获取所有用户信息")]
        public async Task<JsonResult> GetPagingUser(SystemUserPagingInput paging)
        {
            #region 获取权限控制器信息
            SystemPermissionSqlInput input = new SystemPermissionSqlInput
            {
                PrincipalUser = _currentUser,
                EnumPermissionRoteConvert = EnumPermissionRoteConvert.人员数据权限
            };
            paging.DataSql = await _permissionLogic.GetDataPermissionSql(input);
            #endregion
            var users = await _userInfoLogic.PagingUserQuery(paging);
            return JsonForGridPaging(users);
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
                EnumPermissionRoteConvert = EnumPermissionRoteConvert.人员数据权限
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
        ///     根据主键获取用户信息
        /// </summary>
        /// <param name="input">用户Id</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("用户维护-方法-列表-根据主键获取用户信息")]
        public async Task<JsonResult> GetById(IdInput input)
        {
            return Json(await _userInfoLogic.GetById(input));
        }

        /// <summary>
        ///     检测代码是否已经具有重复项
        /// </summary>
        /// <param name="input">需要验证的参数</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("用户维护-方法-新增/编辑-检测代码是否已经具有重复项")]
        public async Task<JsonResult> CheckUserCode(CheckSameValueInput input)
        {
            return Json(await _userInfoLogic.CheckUserCode(input));
        }

        /// <summary>
        ///     保存人员数据
        /// </summary>
        /// <param name="input">人员信息</param>

        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("用户维护-方法-新增/编辑-保存")]
        public async Task<JsonResult> SaveUser(SystemUserSaveInput input)
        {
            input.CreateUserId = _currentUser.UserId;
            input.CreateUserName = _currentUser.Name;
            return Json(await _userInfoLogic.SaveUser(input));
        }

        /// <summary>
        ///     删除人员数据
        /// </summary>
        /// <param name="input">人员Id</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("用户维护-方法-列表-删除")]
        public async Task<JsonResult> DeleteUser(IdInput input)
        {
            return Json(await _userInfoLogic.DeleteUser(input));
        }

        /// <summary>
        ///     导出到Excel
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("用户维护-方法-列表-导出到Excel")]
        public FileResult ExportExcel(SystemUserPagingInput paging)
        {
            ExcelReportDto excelReportDto = new ExcelReportDto()
            {
                //TemplatePath = Server.MapPath("/") + "Templates/System/User/用户导出模版.xlsx",
                DownTemplatePath = "用户信息" + string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now) + ".xlsx",
                Title = "用户信息.xlsx"
            };
            _userInfoLogic.ReportExcelUserQuery(paging, excelReportDto);
            //return File(new FileStream(excelReportDto.DownPath, FileMode.Open), "application/octet-stream", Server.UrlEncode(excelReportDto.Title));
            return null;
        }

        /// <summary>
        ///     根据用户Id获取用户详细情况
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("用户维护-方法-列表-根据用户Id获取用户详细情况")]
        public async Task<JsonResult> GetDetailByUserId(IdInput input)
        {
            return Json(await _userInfoLogic.GetDetailByUserId(input));
        }

        /// <summary>
        ///     根据用户Id重置某人密码
        /// </summary>
        /// <param name="input">用户Id</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("用户维护-方法-列表-重置密码")]
        public async Task<JsonResult> ResetPassword(SystemUserResetPasswordInput input)
        {
            return Json(await _userInfoLogic.ResetPassword(input));
        }

        /// <summary>
        ///     保存用户头像
        /// </summary>
        /// <param name="input">用户Id</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("用户维护-方法-列表-保存用户头像")]
        public async Task<JsonResult> SaveHeadImage(IdInput<string> input)
        {
            return Json(await _userInfoLogic.SaveHeadImage(new SystemUserSaveHeadImageInput
            {
                HeadImage = input.Id,
                UserId = _currentUser.UserId
            }));
        }

        /// <summary>
        ///     获取用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("用户控件-方法-获取用户")]
        public async Task<JsonResult> GetChosenPrivilegeMasterUser(SystemUserGetChosenPrivilegeMasterUser input)
        {
            return Json(await _userInfoLogic.GetChosenPrivilegeMasterUser(input));
        }

        /// <summary>
        ///     保存用户信息
        /// </summary>
        /// <param name="privilegeMasterUser">用户json字符串</param>
        /// <param name="privilegeMasterValue">角色信息</param>
        /// <param name="privilegeMaster">归属人员类型:组织机构、角色、岗位、组</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("用户控件-方法-保存权限用户信息")]
        public async Task<JsonResult> SavePrivilegeMasterUser(string privilegeMasterUser,
            Guid privilegeMasterValue,
            EnumPrivilegeMaster privilegeMaster)
        {
            var models = JsonConvert.DeserializeObject<IList<SystemPermissionSaveUserInput>>(privilegeMasterUser);
            IList<Guid> users = models.Select(m => m.U).ToList();
            return Json(await _permissionUserLogic.SavePermissionUserBeforeDelete(privilegeMaster, privilegeMasterValue, users));
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
        [HttpGet]
        [CreateBy("孙泽伟")]
        [Remark("主界面-方法-验证旧密码是否输入正确")]
        public async Task<JsonResult> CheckOldPassword(CheckSameValueInput input)
        {
            input.Id = _currentUser.UserId;
            return JsonForCheckSameValueValidator(await _userInfoLogic.CheckOldPassword(input));
        }
        #endregion
    }
}