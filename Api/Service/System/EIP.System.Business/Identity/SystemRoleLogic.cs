using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Core.Extensions;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.Common.Core.Resource;
using EIP.Common.Core.Utils;
using EIP.Common.Entities.Dtos.IView;
using EIP.System.Business.Permission;
using EIP.System.DataAccess.Identity;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using EIP.System.Models.Enums;
using EIP.System.Models.Resx;

namespace EIP.System.Business.Identity
{
    public class SystemRoleLogic : DapperAsyncLogic<SystemRole>, ISystemRoleLogic
    {
        #region 构造函数

        private readonly ISystemRoleRepository _roleRepository;
        private readonly ISystemPermissionUserLogic _permissionUserLogic;
        private readonly ISystemPermissionLogic _permissionLogic;
        private readonly ISystemOrganizationRepository _organizationRepository;
        public SystemRoleLogic(ISystemRoleRepository roleRepository,
            ISystemPermissionUserLogic permissionUserLogic,
            ISystemPermissionLogic permissionLogic,
            ISystemOrganizationRepository organizationRepository)
            : base(roleRepository)
        {
            _permissionUserLogic = permissionUserLogic;
            _permissionLogic = permissionLogic;
            _organizationRepository = organizationRepository;
            _roleRepository = roleRepository;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     根据组织机构Id查询角色信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemRoleOutput>> GetRolesByOrganizationId(SystemRolesGetByOrganizationId input)
        {
            var data = (await _roleRepository.GetRolesByOrganizationId(input)).ToList();
            var allOrgs = (await _organizationRepository.FindAllAsync()).ToList();
            foreach (var user in data)
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
        /// 获取用户角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SystemRoleChosenOutput> GetChosenRoles(IdInput input)
        {
            SystemRoleChosenOutput output = new SystemRoleChosenOutput();
            //获取所有角色
            IList<SystemRoleOutput> roleDtos = (await GetRolesByOrganizationId(new SystemRolesGetByOrganizationId())).ToList();
            //获取当前人员具有的角色
            IList<SystemRole> roles = (await GetHaveUserRole(EnumPrivilegeMaster.角色, input.Id)).ToList();
            var allOrgs = (await _organizationRepository.FindAllAsync()).ToList();
            IList<string> haveUser = new List<string>();
            IList<TransferDto> allUser = new List<TransferDto>();
            foreach (var role in roleDtos)
            {
                var permission = roles.Where(w => w.RoleId == role.RoleId).FirstOrDefault();
                if (permission != null)
                {
                    haveUser.Add(role.RoleId.ToString());
                }

                TransferDto dto = new TransferDto
                {
                    key = role.RoleId.ToString(),
                    label = role.Name
                };
                string description = string.Empty;
                var organization = allOrgs.FirstOrDefault(w => w.OrganizationId == role.OrganizationId);
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
            output.AllRole = allUser;
            output.HaveRole = haveUser;
            return output;
        }

        /// <summary>
        ///     保存岗位信息
        /// </summary>
        /// <param name="role">岗位信息</param>
        /// <returns></returns>
        public async Task<OperateStatus> SaveRole(SystemRole role)
        {
            role.CanbeDelete = true;
            if (role.RoleId.IsEmptyGuid())
            {
                role.CreateTime = DateTime.Now;
                role.RoleId = Guid.NewGuid();
                return await InsertAsync(role);
            }
            var systemRole = await GetByIdAsync(role.RoleId);
            role.CreateTime = systemRole.CreateTime;
            role.CreateUserId = systemRole.CreateUserId;
            role.CreateUserName = systemRole.CreateUserName;
            role.UpdateTime = DateTime.Now;
            role.UpdateUserId = role.CreateUserId;
            role.UpdateUserName = role.CreateUserName;
            return await UpdateAsync(role);
        }

        /// <summary>
        ///     角色复制
        /// </summary>
        /// <param name="input">角色Id</param>
        /// <returns></returns>
        public async Task<OperateStatus> CopyRole(SystemCopyInput input)
        {
            var operateStatus = new OperateStatus();
            try
            {
                //获取角色信息
                var role = await GetByIdAsync(input.Id);
                role.RoleId = CombUtil.NewComb();
                role.Name = input.Name;
                role.CreateTime=DateTime.Now;
                
                //获取该角色拥有的权限及人员
                var allUser = (await _permissionUserLogic.GetPermissionUsersByPrivilegeMasterAdnPrivilegeMasterValue(EnumPrivilegeMaster.角色,
                    input.Id)).ToList();
                var allPer = (await _permissionLogic.GetSystemPermissionsByPrivilegeMasterValueAndValue(EnumPrivilegeMaster.角色, input.Id)).ToList();
                foreach (var user in allUser)
                {
                    user.PrivilegeMasterValue = role.RoleId;
                }
                foreach (var per in allPer)
                {
                    per.PrivilegeMasterValue = role.RoleId;
                }
                //批量插入
                operateStatus = await _permissionUserLogic.InsertMultipleAsync(allUser);
                operateStatus = await _permissionLogic.InsertMultipleAsync(allPer);
                operateStatus = await InsertAsync(role);
                operateStatus.Message = Chs.Successful;
                operateStatus.ResultSign = ResultSign.Successful;
            }
            catch (Exception e)
            {
                operateStatus.Message = e.Message;
            }
            return operateStatus;
        }

        /// <summary>
        ///     获取该用户已经具有的角色信息
        /// </summary>
        /// <param name="privilegeMaster"></param>
        /// <param name="userId">需要查询的用户id</param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemRole>> GetHaveUserRole(EnumPrivilegeMaster privilegeMaster,
            Guid userId)
        {
            return await _roleRepository.GetHaveUserRole(privilegeMaster, userId);
        }

        /// <summary>
        ///     删除角色信息
        /// </summary>
        /// <param name="input">角色Id</param>
        /// <returns></returns>
        public async Task<OperateStatus> DeleteRole(IdInput input)
        {
            var operateStatus = new OperateStatus();
            //判断是否具有人员
            var permissionUsers = await
                _permissionUserLogic.GetPermissionUsersByPrivilegeMasterAdnPrivilegeMasterValue(EnumPrivilegeMaster.角色,
                    input.Id);
            if (permissionUsers.Any())
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message =  ResourceSystem.具有人员;
                return operateStatus;
            }
            //判断是否具有按钮权限
            var functionPermissions = await
                _permissionLogic.GetPermissionByPrivilegeMasterValue(
                    new SystemPermissionByPrivilegeMasterValueInput
                    {
                        PrivilegeAccess = EnumPrivilegeAccess.菜单按钮,
                        PrivilegeMasterValue = input.Id,
                        PrivilegeMaster = EnumPrivilegeMaster.角色
                    });
            if (functionPermissions.Any())
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message =  ResourceSystem.具有功能项权限;
                return operateStatus;
            }
            //判断是否具有菜单权限
            var menuPermissions = await
                _permissionLogic.GetPermissionByPrivilegeMasterValue(
                    new SystemPermissionByPrivilegeMasterValueInput
                    {
                        PrivilegeAccess = EnumPrivilegeAccess.菜单,
                        PrivilegeMasterValue = input.Id,
                        PrivilegeMaster = EnumPrivilegeMaster.角色
                    });
            if (menuPermissions.Any())
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message =  ResourceSystem.具有菜单权限;
                return operateStatus;
            }
            return await DeleteAsync(new SystemRole
            {
                RoleId = input.Id
            });
        }

        #endregion
    }
}