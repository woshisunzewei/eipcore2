using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Core.Extensions;
using EIP.Common.Core.Utils;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.Common.Core.Resource;
using EIP.System.Business.Permission;
using EIP.System.DataAccess.Identity;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using EIP.System.Models.Enums;
using EIP.System.Models.Resx;

namespace EIP.System.Business.Identity
{
    public class SystemGroupLogic : DapperAsyncLogic<SystemGroup>, ISystemGroupLogic
    {
        #region 构造函数

        private readonly ISystemGroupRepository _groupRepository;
        private readonly ISystemPermissionUserLogic _permissionUserLogic;
        private readonly ISystemPermissionLogic _permissionLogic;
        private readonly ISystemOrganizationRepository _organizationRepository;
        public SystemGroupLogic(ISystemGroupRepository groupRepository,
            ISystemPermissionUserLogic permissionUserLogic,
            ISystemPermissionLogic permissionLogic, ISystemOrganizationRepository organizationRepository)
            : base(groupRepository)
        {
            _permissionUserLogic = permissionUserLogic;
            _permissionLogic = permissionLogic;
            _organizationRepository = organizationRepository;
            _groupRepository = groupRepository;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     根据组织机构获取组信息
        /// </summary>
        /// <param name="input">组织机构</param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemGroupOutput>> GetGroupByOrganizationId(SystemGroupGetGroupByOrganizationIdInput input)
        {
            var groups = (await _groupRepository.GetGroupByOrganizationId(input)).ToList();
            var allOrgs = (await _organizationRepository.FindAllAsync()).ToList();
            foreach (var group in groups)
            {
                group.BelongToName = EnumUtil.GetName(typeof(EnumGroupBelongTo), group.BelongTo);
                var organization = allOrgs.FirstOrDefault(w => w.OrganizationId == group.OrganizationId);
                if (organization != null && !organization.ParentIds.IsNullOrEmpty())
                {
                    foreach (var parent in organization.ParentIds.Split(','))
                    {
                        //查找上级
                        var dicinfo = allOrgs.FirstOrDefault(w => w.OrganizationId.ToString() == parent);
                        if (dicinfo != null) group.OrganizationNames += dicinfo.Name + ">";
                    }
                    if (!group.OrganizationNames.IsNullOrEmpty())
                        group.OrganizationNames = group.OrganizationNames.TrimEnd('>');
                }
            }
            return groups;
        }

        /// <summary>
        ///     保存组信息
        /// </summary>
        /// <param name="group">岗位信息</param>
        /// <param name="belongTo">归属</param>
        /// <returns></returns>
        public async Task<OperateStatus> SaveGroup(SystemGroup group,
            EnumGroupBelongTo belongTo)
        {
            group.BelongTo = (byte)belongTo;
            if (group.GroupId.IsEmptyGuid())
            {
                group.CreateTime = DateTime.Now;
                group.GroupId = CombUtil.NewComb();
                return await InsertAsync(group);
            }
            var systemGroup = await GetByIdAsync(group.GroupId);
            group.CreateTime = systemGroup.CreateTime;
            group.CreateUserId = systemGroup.CreateUserId;
            group.CreateUserName = systemGroup.CreateUserName;
            group.UpdateTime = DateTime.Now;
            group.UpdateUserId = group.CreateUserId;
            group.UpdateUserName = group.CreateUserName;
            return await UpdateAsync(group);
        }

        /// <summary>
        ///     删除组信息
        /// </summary>
        /// <param name="input">组Id</param>
        /// <returns></returns>
        public async Task<OperateStatus> DeleteGroup(IdInput input)
        {
            var operateStatus = new OperateStatus();
            //判断是否具有人员
            var permissionUsers = await
                _permissionUserLogic.GetPermissionUsersByPrivilegeMasterAdnPrivilegeMasterValue(EnumPrivilegeMaster.组,
                    input.Id);
            if (permissionUsers.Any())
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message = ResourceSystem.具有人员;
                return operateStatus;
            }
            //判断是否具有按钮权限
            var functionPermissions = await
                _permissionLogic.GetPermissionByPrivilegeMasterValue(
                    new SystemPermissionByPrivilegeMasterValueInput
                    {
                        PrivilegeAccess = EnumPrivilegeAccess.菜单按钮,
                        PrivilegeMasterValue = input.Id,
                        PrivilegeMaster = EnumPrivilegeMaster.组
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
                        PrivilegeMaster = EnumPrivilegeMaster.组
                    });

            if (menuPermissions.Any())
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message =  ResourceSystem.具有菜单权限;
                return operateStatus;
            }
            return await DeleteAsync(new SystemGroup { GroupId = input.Id });
        }


        /// <summary>
        ///     复制
        /// </summary>
        /// <param name="input">复制信息</param>
        /// <returns></returns>
        public async Task<OperateStatus> CopyGroup(SystemCopyInput input)
        {
            var operateStatus = new OperateStatus();
            try
            {
                //获取信息
                var role = await GetByIdAsync(input.Id);
                role.GroupId = CombUtil.NewComb();
                role.Name = input.Name;
                role.CreateTime = DateTime.Now;

                //获取拥有的权限及人员
                var allUser = (await _permissionUserLogic.GetPermissionUsersByPrivilegeMasterAdnPrivilegeMasterValue(EnumPrivilegeMaster.组,
                    input.Id)).ToList();
                var allPer = (await _permissionLogic.GetSystemPermissionsByPrivilegeMasterValueAndValue(EnumPrivilegeMaster.组, input.Id)).ToList();
                foreach (var user in allUser)
                {
                    user.PrivilegeMasterValue = role.GroupId;
                }
                foreach (var per in allPer)
                {
                    per.PrivilegeMasterValue = role.GroupId;
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
        #endregion
    }
}