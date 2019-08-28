using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Core.Extensions;
using EIP.Common.Core.Resource;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.System.Business.Identity;
using EIP.System.DataAccess.Identity;
using EIP.System.DataAccess.Permission;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using EIP.System.Models.Enums;

namespace EIP.System.Business.Permission
{
    /// <summary>
    ///     权限用户业务逻辑
    /// </summary>
    public class SystemPermissionUserLogic : DapperAsyncLogic<SystemPermissionUser>, ISystemPermissionUserLogic
    {
        #region 构造函数
        private readonly ISystemOrganizationRepository _organizationRepository;
        private readonly ISystemPermissionUserRepository _permissionUsernRepository;

        public SystemPermissionUserLogic(ISystemPermissionUserRepository permissionUserRepository,
            ISystemOrganizationRepository organizationRepository)
            : base(permissionUserRepository)
        {
            _permissionUsernRepository = permissionUserRepository;
            _organizationRepository = organizationRepository;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     保存各种用户:组织机构、岗位、组、人员
        /// </summary>
        /// <param name="master">类型</param>
        /// <param name="value">业务表Id：如组织机构Id、组Id、岗位Id、人员Id等</param>
        /// <param name="userIds">权限类型:组织机构、组、岗位、人员Id</param>
        /// <returns></returns>
        public async Task<OperateStatus> SavePermissionUser(EnumPrivilegeMaster master,
            Guid value,
            IList<Guid> userIds)
        {
            IList<SystemPermissionUser> systemPermissionUsers = userIds.Select(userId => new SystemPermissionUser
            {
                PrivilegeMaster = (byte)master,
                PrivilegeMasterUserId = userId,
                PrivilegeMasterValue = value
            }).ToList();
            //批量保存
            return await InsertMultipleAsync(systemPermissionUsers);
        }

        /// <summary>
        ///     保存各种用户:组织机构、岗位、组、人员【先进行删除,再进行添加】
        /// </summary>
        /// <param name="master">类型</param>
        /// <param name="value">业务表Id：如组织机构Id、组Id、岗位Id、人员Id等</param>
        /// <param name="userIds">权限类型:组织机构、组、岗位、人员Id</param>
        /// <returns></returns>
        public async Task<OperateStatus> SavePermissionUserBeforeDelete(EnumPrivilegeMaster master,
            Guid value,
            IList<Guid> userIds)
        {
            var operateStatus = new OperateStatus();
            //删除
            await _permissionUsernRepository.DeletePermissionUser(master, value);
            IList<SystemPermissionUser> systemPermissionUsers = userIds.Select(userId => new SystemPermissionUser
            {
                PrivilegeMaster = (byte)master,
                PrivilegeMasterUserId = userId,
                PrivilegeMasterValue = value
            }).ToList();
            if (systemPermissionUsers.Any())
            {
                //批量保存
                operateStatus = await InsertMultipleAsync(systemPermissionUsers);
            }
            else
            {
                operateStatus.ResultSign = ResultSign.Successful;
                operateStatus.Message = Chs.Successful;
            }
            return operateStatus;
        }

        /// <summary>
        ///     保存用户权限类型
        /// </summary>
        /// <param name="privilegeMaster">类型</param>
        /// <param name="privilegeMasterUserId">业务表Id：如组织机构Id、组Id、岗位Id、人员Id等</param>
        /// <param name="privilegeMasterValue">权限类型:角色Id</param>
        /// <returns></returns>
        public async Task<OperateStatus> SavePermissionMasterValueBeforeDelete(EnumPrivilegeMaster privilegeMaster,
            Guid privilegeMasterUserId,
            IList<Guid> privilegeMasterValue)
        {
            var operateStatus = new OperateStatus();
            //删除
            await _permissionUsernRepository.DeletePermissionMaster(privilegeMaster, privilegeMasterUserId);
            IList<SystemPermissionUser> systemPermissionUsers =
                privilegeMasterValue.Select(roleId => new SystemPermissionUser
                {
                    PrivilegeMaster = (byte)privilegeMaster,
                    PrivilegeMasterUserId = privilegeMasterUserId,
                    PrivilegeMasterValue = roleId
                }).ToList();
            if (systemPermissionUsers.Any())
            {
                //批量保存
                operateStatus = await InsertMultipleAsync(systemPermissionUsers);
            }
            else
            {
                operateStatus.ResultSign = ResultSign.Successful;
                operateStatus.Message = Chs.Successful;
            }
            return operateStatus;
        }

        /// <summary>
        ///     删除用户
        /// </summary>
        /// <param name="input">用户Id</param>
        /// <returns></returns>
        public async Task<OperateStatus> DeletePermissionUser(IdInput input)
        {
            var operateStatus = new OperateStatus();
            if (await _permissionUsernRepository.DeletePermissionUser(input) > 0)
            {
                operateStatus.ResultSign = ResultSign.Successful;
                operateStatus.Message = Chs.Successful;
            }
            return operateStatus;
        }

        /// <summary>
        ///     删除用户
        /// </summary>
        /// <param name="privilegeMasterUserId">用户Id</param>
        /// <param name="privilegeMasterValue">归属类型Id:组织机构、角色、岗位、组</param>
        /// <param name="privilegeMaster">归属人员类型:组织机构、角色、岗位、组</param>
        /// <returns></returns>
        public async Task<OperateStatus> DeletePrivilegeMasterUser(Guid privilegeMasterUserId,
            Guid privilegeMasterValue,
            EnumPrivilegeMaster privilegeMaster)
        {
            var operateStatus = new OperateStatus();
            if (
                await _permissionUsernRepository.DeletePrivilegeMasterUser(privilegeMasterUserId, privilegeMasterValue,
                    privilegeMaster) > 0)
            {
                operateStatus.ResultSign = ResultSign.Successful;
                operateStatus.Message = Chs.Successful;
            }
            return operateStatus;
        }

        /// <summary>
        ///     根据用户Id获取角色、组、岗位信息
        /// </summary>
        /// <param name="input">人员Id</param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemPrivilegeDetailListOutput>> GetSystemPrivilegeDetailOutputsByUserId(
            IdInput input)
        {
            return await _permissionUsernRepository.GetSystemPrivilegeDetailOutputsByUserId(input);
        }

        /// <summary>
        ///     根据特权类型及特权id获取特权用户信息
        /// </summary>
        /// <param name="privilegeMaster">特权类型</param>
        /// <param name="privilegeMasterValue">特权id</param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemPermissionUser>> GetPermissionUsersByPrivilegeMasterAdnPrivilegeMasterValue(
            EnumPrivilegeMaster privilegeMaster,
            Guid? privilegeMasterValue = null)
        {
            return
                await
                    _permissionUsernRepository.GetPermissionUsersByPrivilegeMasterAdnPrivilegeMasterValue(
                        privilegeMaster, privilegeMasterValue);
        }

        /// <summary>
        ///     获取菜单、字段对应拥有者信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SystemPrivilegeDetailOutput> GetSystemPrivilegeDetailOutputsByAccessAndValue(
            SystemPrivilegeDetailInput input)
        {
            var output = new SystemPrivilegeDetailOutput();
            //获取角色、组、岗位数据
            IList<SystemPrivilegeDetailListOutput> privilegeDetailDtos =
                (await _permissionUsernRepository.GetSystemPrivilegeDetailOutputsByAccessAndValue(input)).ToList().DistinctBy(
                    p => new {p.Name, p.OrganizationId, p.PrivilegeMaster }).ToList();

            var allOrgs = (await _organizationRepository.FindAllAsync()).ToList();
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
            output.Role = privilegeDetailDtos.Where(w => w.PrivilegeMaster == EnumPrivilegeMaster.角色).ToList();
            //组
            output.Group = privilegeDetailDtos.Where(w => w.PrivilegeMaster == EnumPrivilegeMaster.组).ToList();
            //岗位
            output.Post = privilegeDetailDtos.Where(w => w.PrivilegeMaster == EnumPrivilegeMaster.岗位).ToList();
            //组织机构
            output.Organization = privilegeDetailDtos.Where(w => w.PrivilegeMaster == EnumPrivilegeMaster.组织机构).ToList();
            //用户
            output.User = privilegeDetailDtos.Where(w => w.PrivilegeMaster == EnumPrivilegeMaster.人员).ToList();
            return output;
        }

        #endregion

        /// <summary>
        ///     删除用户对应权限数据
        /// </summary>
        /// <param name="privilegeMasterUserId">用户Id</param>
        /// <param name="privilegeMaster">归属人员类型:组织机构、角色、岗位、组</param>
        /// <returns></returns>
        public async Task<OperateStatus> DeletePrivilegeMasterUser(Guid privilegeMasterUserId, EnumPrivilegeMaster privilegeMaster)
        {
            var operateStatus = new OperateStatus();
            if (await _permissionUsernRepository.DeletePrivilegeMasterUser(privilegeMasterUserId,privilegeMaster) > 0)
            {
                operateStatus.ResultSign = ResultSign.Successful;
                operateStatus.Message = Chs.Successful;
            }
            return operateStatus;
        }
    }
}