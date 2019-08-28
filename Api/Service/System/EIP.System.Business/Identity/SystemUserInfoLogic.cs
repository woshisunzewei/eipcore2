using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Core.Extensions;
using EIP.Common.Core.Resource;
using EIP.Common.Core.Utils;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Dtos.IView;
using EIP.Common.Entities.Dtos.Reports;
using EIP.Common.Entities.Paging;
using EIP.System.Business.Permission;
using EIP.System.DataAccess.Identity;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using EIP.System.Models.Enums;
using EIP.System.Models.Resx;
using Microsoft.Extensions.Options;

namespace EIP.System.Business.Identity
{
    /// <summary>
    ///     用户业务逻辑实现
    /// </summary>
    public class SystemUserInfoLogic : DapperAsyncLogic<SystemUserInfo>, ISystemUserInfoLogic
    {
        #region 构造函数

        private readonly ISystemOrganizationLogic _organizationLogic;
        private readonly ISystemPermissionUserLogic _permissionUserLogic;
        private readonly ISystemUserInfoRepository _userInfoRepository;
        private readonly IOptions<EIPConfig> _configOptions;
        public SystemUserInfoLogic(ISystemUserInfoRepository userInfoRepository,
            ISystemPermissionUserLogic permissionUserLogic, IOptions<EIPConfig> configOptions, ISystemOrganizationLogic organizationLogic)
            : base(userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
            _permissionUserLogic = permissionUserLogic;
            _configOptions = configOptions;
            _organizationLogic = organizationLogic;
        }

        public SystemUserInfoLogic(IOptions<EIPConfig> configOptions, ISystemOrganizationLogic organizationLogic)
        {
            _configOptions = configOptions;
            _organizationLogic = organizationLogic;
            _userInfoRepository = new SystemUserInfoRepository();
        }

        #endregion

        #region 方法

        /// <summary>
        ///     根据登录代码和密码查询用户信息
        /// </summary>
        /// <param name="input">登录名、密码等</param>
        /// <returns></returns>
        public async Task<OperateStatus<SystemUserLoginOutput>> CheckUserByCodeAndPwd(SystemUserLoginInput input)
        {
            var operateStatus = new OperateStatus<SystemUserLoginOutput>();
            //将传入的密码加密
            var encryptPwd = DEncryptUtil.Encrypt(input.Pwd,  _configOptions.Value.PasswordKey);
            //查询信息
            input.Pwd = encryptPwd;
            var data = await _userInfoRepository.CheckUserByCodeAndPwd(input);
            //是否存在
            if (data == null)
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message = ResourceSystem.用户名或密码错误;
                return operateStatus;
            }
            //是否冻结
            if (data.IsFreeze)
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message = ResourceSystem.登录用户已冻结;
                return operateStatus;
            }
            //成功
            operateStatus.ResultSign = ResultSign.Successful;
            operateStatus.Message = "/";
            
            if (data.FirstVisitTime == null)
            {
                //更新用户最后一次登录时间
                _userInfoRepository.UpdateFirstVisitTime(new IdInput(data.UserId));
            }
            //更新用户最后一次登录时间
            _userInfoRepository.UpdateLastLoginTime(new IdInput(data.UserId));
            data.LoginId = CombUtil.NewComb();
            operateStatus.Data = data;
            return operateStatus;
        }


        /// <summary>
        ///     分页查询
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        public async Task<PagedResults<SystemUserOutput>> PagingUserQuery(SystemUserPagingInput paging)
        {
            if(paging.DataSql.IsNullOrEmpty())
                return new PagedResults<SystemUserOutput>();
            var data = await _userInfoRepository.PagingUserQuery(paging);
            var allOrgs = (await _organizationLogic.GetAllEnumerableAsync()).ToList();
            foreach (var user in data.Data)
            {
                var organization = allOrgs.FirstOrDefault(w => w.OrganizationId == user.OrganizationId);
                if (organization != null && !organization.ParentIds.IsNullOrEmpty())
                {
                    foreach (var parent in organization.ParentIds.Split(','))
                    {
                        //查找上级
                        var dicinfo = allOrgs.FirstOrDefault(w => w.OrganizationId.ToString() == parent);
                        if (dicinfo != null) user.OrganizationNames += dicinfo.Name + ">";
                    }
                    if (!user.OrganizationNames.IsNullOrEmpty())
                        user.OrganizationNames = user.OrganizationNames.TrimEnd('>');
                }
            }
            return data;
        }

        /// <summary>
        ///     Excel导出方式
        /// </summary>
        /// <param name="paging">查询参数</param>
        /// <param name="excelReportDto"></param>
        /// <returns></returns>
        public async Task<OperateStatus> ReportExcelUserQuery(SystemUserPagingInput paging,
            ExcelReportDto excelReportDto)
        {
            var operateStatus = new OperateStatus();
            try
            {
                //组装数据
                IList<SystemUserOutput> dtos = (await _userInfoRepository.PagingUserQuery(paging)).Data.ToList();
                var tables = new Dictionary<string, DataTable>(StringComparer.OrdinalIgnoreCase);
                //组装需要导出数据

                operateStatus.ResultSign = ResultSign.Successful;
            }
            catch (Exception)
            {
                operateStatus.ResultSign = ResultSign.Error;
            }
            return operateStatus;
        }

        /// <summary>
        ///     检测配置项代码是否已经具有重复项
        /// </summary>
        /// <param name="input">需要验证的参数</param>
        /// <returns></returns>
        public async Task<OperateStatus> CheckUserCode(CheckSameValueInput input)
        {
            var operateStatus = new OperateStatus();
            if (await _userInfoRepository.CheckUserCode(input))
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message = string.Format(Chs.HaveCode, input.Param);
            }
            else
            {
                operateStatus.ResultSign = ResultSign.Successful;
                operateStatus.Message = Chs.CheckSuccessful;
            }
            return operateStatus;
        }

        /// <summary>
        ///     保存人员信息
        /// </summary>
        /// <param name="input">人员信息</param>
        /// <returns></returns>
        public async Task<OperateStatus> SaveUser(SystemUserSaveInput input)
        {
            OperateStatus operateStatus;
            if (input.UserId.IsEmptyGuid())
            {
                //新增
                input.CreateTime = DateTime.Now;
                input.UserId = Guid.NewGuid();
                if (!input.Code.IsNullOrEmpty())
                {
                    input.Password = DEncryptUtil.Encrypt("123456", _configOptions.Value.PasswordKey);
                }
                SystemUserInfo userInfoMap = input.MapTo<SystemUserInfo>();
                operateStatus = await InsertAsync(userInfoMap);
                if (operateStatus.ResultSign == ResultSign.Successful)
                {
                    //添加用户到组织机构
                    operateStatus = await
                            _permissionUserLogic.SavePermissionUser(EnumPrivilegeMaster.组织机构, input.OrganizationId,
                                new List<Guid> { input.UserId });
                    if (operateStatus.ResultSign == ResultSign.Successful)
                    {
                        return operateStatus;
                    }
                }
                else
                {
                    return operateStatus;
                }
            }
            else
            {
                //删除对应组织机构
                operateStatus = await _permissionUserLogic.DeletePrivilegeMasterUser(input.UserId, EnumPrivilegeMaster.组织机构);
                if (operateStatus.ResultSign == ResultSign.Successful)
                {
                    //添加用户到组织机构
                    operateStatus = await _permissionUserLogic.SavePermissionUser(EnumPrivilegeMaster.组织机构, input.OrganizationId, new List<Guid> { input.UserId });
                    if (operateStatus.ResultSign == ResultSign.Successful)
                    {
                        var userInfo = await GetByIdAsync(input.UserId);
                        input.CreateTime = userInfo.CreateTime;
                        input.Password = userInfo.Password;
                        input.UpdateTime = DateTime.Now;
                        input.UpdateUserId = userInfo.CreateUserId;
                        input.UpdateUserName = input.CreateUserName;
                        SystemUserInfo userInfoMap = input.MapTo<SystemUserInfo>();
                        return await UpdateAsync(userInfoMap);
                    }
                }
            }
            return operateStatus;
        }

        /// <summary>
        ///     获取所有用户
        /// </summary>
        /// <param name="input">是否冻结</param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemChosenUserOutput>> GetChosenUser(FreezeInput input = null)
        {
            return await _userInfoRepository.GetChosenUser(input);
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemUserInfo>> GetUser(FreezeInput input = null)
        {
            return await _userInfoRepository.GetUser(input);
        }

        /// <summary>
        ///     删除用户信息
        /// </summary>
        /// <param name="input">用户id</param>
        /// <returns></returns>
        public async Task<OperateStatus> DeleteUser(IdInput input)
        {
            await _permissionUserLogic.DeletePermissionUser(input);
            return await DeleteAsync(new SystemUserInfo
            {
                UserId = input.Id
            });
        }

        /// <summary>
        ///     根据用户Id获取该用户信息
        /// </summary>
        /// <param name="input">用户Id</param>
        /// <returns></returns>
        public async Task<SystemUserDetailOutput> GetDetailByUserId(IdInput input)
        {
            //获取用户基本信息
            var userDto = (await _userInfoRepository.FindByIdAsync(input.Id)).MapTo<SystemUserOutput>();
            //转换
            var userDetailDto = userDto.MapTo<SystemUserDetailOutput>();
            //获取角色、组、岗位数据
            IList<SystemPrivilegeDetailListOutput> privilegeDetailDtos = (await
                _permissionUserLogic.GetSystemPrivilegeDetailOutputsByUserId(input)).ToList();
            var allOrgs = (await _organizationLogic.GetAllEnumerableAsync()).ToList();
            foreach (var dto in privilegeDetailDtos)
            {
                string description = string.Empty;
                var organization = allOrgs.FirstOrDefault(w => w.OrganizationId == dto.OrganizationId);
                if (organization != null && !organization.ParentIds.IsNullOrEmpty())
                {
                    foreach (var parent in organization.ParentIds.Split(','))
                    {
                        //查找上级
                        var dicinfo = allOrgs.FirstOrDefault(w => w.OrganizationId.ToString() == parent);
                        if (dicinfo != null) description += dicinfo.Name + ">";
                    }
                    if (!description.IsNullOrEmpty())
                        description = description.TrimEnd('>');
                }
                dto.Organization = description;
            }

            //角色
            userDetailDto.Role = privilegeDetailDtos.Where(w => w.PrivilegeMaster == EnumPrivilegeMaster.角色).ToList();
            //组
            userDetailDto.Group = privilegeDetailDtos.Where(w => w.PrivilegeMaster == EnumPrivilegeMaster.组).ToList();
            //岗位
            userDetailDto.Post = privilegeDetailDtos.Where(w => w.PrivilegeMaster == EnumPrivilegeMaster.岗位).ToList();
            return userDetailDto;
        }

        /// <summary>
        ///     根据用户Id重置某人密码
        /// </summary>
        /// <param name="input">用户Id</param>
        /// <returns></returns>
        public async Task<OperateStatus> ResetPassword(SystemUserResetPasswordInput input)
        {
            var operateStatus = new OperateStatus();
            //将传入的密码加密
            var encryptPwd = DEncryptUtil.Encrypt(input.EncryptPassword, _configOptions.Value.PasswordKey);
            if (await _userInfoRepository.ResetPassword(new SystemUserResetPasswordInput
            {
                EncryptPassword = encryptPwd,
                Id = input.Id
            }))
            {
                operateStatus.ResultSign = ResultSign.Successful;
                operateStatus.Message = string.Format(ResourceSystem.重置密码成功, input.EncryptPassword);
            }
            return operateStatus;
        }

        /// <summary>
        ///     保存用户头像
        /// </summary>
        /// <param name="input">用户头像</param>
        /// <returns></returns>
        public async Task<OperateStatus> SaveHeadImage(SystemUserSaveHeadImageInput input)
        {
            var operateStatus = new OperateStatus();
            if (await _userInfoRepository.SaveHeadImage(input))
            {
                operateStatus.ResultSign = ResultSign.Successful;
                operateStatus.Message = Chs.Successful;
            }
            return operateStatus;
        }

        /// <summary>
        /// 保存修改后密码信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OperateStatus> SaveChangePassword(SystemUserChangePasswordInput input)
        {
            var operateStatus = new OperateStatus();
            //后台再次验证是否一致
            if (!input.NewPassword.Equals(input.ConfirmNewPassword))
            {
                operateStatus.Message =  "录入的新密码和确认密码不一致。";
                return operateStatus;
            }
            //旧密码是否正确
            operateStatus = await CheckOldPassword(new CheckSameValueInput { Id = input.Id, Param = input.OldPassword });
            if (operateStatus.ResultSign == ResultSign.Error)
            {
                return operateStatus;
            }
            //将传入的密码加密
            var encryptPwd = DEncryptUtil.Encrypt(input.NewPassword, _configOptions.Value.PasswordKey);
            if (await _userInfoRepository.ResetPassword(new SystemUserResetPasswordInput
            {
                EncryptPassword = encryptPwd,
                Id = input.Id
            }))
            {
                operateStatus.ResultSign = ResultSign.Successful;
                operateStatus.Message = string.Format(ResourceSystem.重置密码成功, input.NewPassword);
            }
            return operateStatus;
        }

        /// <summary>
        ///     验证旧密码是否输入正确
        /// </summary>
        /// <param name="input">需要验证的参数</param>
        /// <returns></returns>
        public async Task<OperateStatus> CheckOldPassword(CheckSameValueInput input)
        {
            var operateStatus = new OperateStatus();
            input.Param = DEncryptUtil.Encrypt(input.Param, _configOptions.Value.PasswordKey);
            if (!await _userInfoRepository.CheckOldPassword(input))
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message = "旧密码不正确";
            }
            else
            {
                operateStatus.ResultSign = ResultSign.Successful;
            }
            return operateStatus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SystemUserGetRoleUsersOutput> GetChosenPrivilegeMasterUser(SystemUserGetChosenPrivilegeMasterUser input)
        {
            SystemUserGetRoleUsersOutput output = new SystemUserGetRoleUsersOutput();
            var chosenUserDtos = await _userInfoRepository.GetChosenUser(new FreezeInput { IsFreeze = false });
            var allOrgs = (await _organizationLogic.GetAllEnumerableAsync()).ToList();
            //获取所有的用户
            var permissions =
                (await
                    _permissionUserLogic.GetPermissionUsersByPrivilegeMasterAdnPrivilegeMasterValue(input.PrivilegeMaster,
                        input.PrivilegeMasterValue)).ToList();
            IList<string> haveUser = new List<string>();
            IList<TransferDto> allUser = new List<TransferDto>();
            foreach (var user in chosenUserDtos)
            {
                var permission = permissions.Where(w => w.PrivilegeMasterUserId == user.UserId).FirstOrDefault();
                if (permission != null)
                {
                    haveUser.Add(user.UserId.ToString());
                }

                TransferDto dto = new TransferDto
                {
                    key = user.UserId.ToString(),
                    label = user.Name
                };
                string description = string.Empty;
                var organization = allOrgs.FirstOrDefault(w => w.OrganizationId == user.OrganizationId);
                if (organization != null && !organization.ParentIds.IsNullOrEmpty())
                {
                    foreach (var parent in organization.ParentIds.Split(','))
                    {
                        //查找上级
                        var dicinfo = allOrgs.FirstOrDefault(w => w.OrganizationId.ToString() == parent);
                        if (dicinfo != null) description += dicinfo.Name + ">";
                    }
                    if (!description.IsNullOrEmpty())
                        description = description.TrimEnd('>');
                }
                dto.description = description;
                allUser.Add(dto);
            }
            output.AllUser = allUser;
            output.HaveUser = haveUser;
            return output;
        }

        /// <summary>
        /// 根据Id获取用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SystemUserGetByIdOutput> GetById(IdInput input)
        {
            return await _userInfoRepository.GetById(input);
        }

        #endregion
    }
}