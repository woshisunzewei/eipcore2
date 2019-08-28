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
    public class SystemPostLogic : DapperAsyncLogic<SystemPost>, ISystemPostLogic
    {
        #region 构造函数

        private readonly ISystemPostRepository _postRepository;
        private readonly ISystemPermissionUserLogic _permissionUserLogic;
        private readonly ISystemPermissionLogic _permissionLogic;
        private readonly ISystemOrganizationRepository _organizationRepository;
        public SystemPostLogic(ISystemPostRepository postRepository,
            ISystemPermissionUserLogic permissionUserLogic,
            ISystemPermissionLogic permissionLogic, ISystemOrganizationRepository organizationRepository)
            : base(postRepository)
        {
            _permissionUserLogic = permissionUserLogic;
            _permissionLogic = permissionLogic;
            _organizationRepository = organizationRepository;
            _postRepository = postRepository;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     根据组织机构获取岗位信息
        /// </summary>
        /// <param name="input">组织机构Id</param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemPostOutput>> GetPostByOrganizationId(SystemPostGetByOrganizationId input)
        {
            var data = (await _postRepository.GetPostByOrganizationId(input)).ToList();
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
        ///     复制
        /// </summary>
        /// <param name="input">复制信息</param>
        /// <returns></returns>
        public async Task<OperateStatus> CopyPost(SystemCopyInput input)
        {
            var operateStatus = new OperateStatus();
            try
            {
                //获取信息
                var role = await GetByIdAsync(input.Id);
                role.PostId = CombUtil.NewComb();
                role.Name = input.Name;
                role.CreateTime = DateTime.Now;

                //获取拥有的权限及人员
                var allUser = (await _permissionUserLogic.GetPermissionUsersByPrivilegeMasterAdnPrivilegeMasterValue(EnumPrivilegeMaster.岗位,
                    input.Id)).ToList();
                var allPer = (await _permissionLogic.GetSystemPermissionsByPrivilegeMasterValueAndValue(EnumPrivilegeMaster.岗位, input.Id)).ToList();
                foreach (var user in allUser)
                {
                    user.PrivilegeMasterValue = role.PostId;
                }
                foreach (var per in allPer)
                {
                    per.PrivilegeMasterValue = role.PostId;
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
        ///     保存岗位信息
        /// </summary>
        /// <param name="post">岗位信息</param>
        /// <returns></returns>
        public async Task<OperateStatus> SavePost(SystemPost post)
        {
            if (post.PostId.IsEmptyGuid())
            {
                post.CreateTime = DateTime.Now;
                post.PostId = CombUtil.NewComb();
                return await InsertAsync(post);
            }
            SystemPost systemPost =await GetByIdAsync(post.PostId);
            post.CreateTime = systemPost.CreateTime;
            post.CreateUserId = systemPost.CreateUserId;
            post.CreateUserName = systemPost.CreateUserName;
            post.UpdateTime = DateTime.Now;
            post.UpdateUserId = post.CreateUserId;
            post.UpdateUserName = post.CreateUserName;
            return await UpdateAsync(post);
        }

        /// <summary>
        ///     删除岗位信息
        /// </summary>
        /// <param name="input">岗位信息Id</param>
        /// <returns></returns>
        public async Task<OperateStatus> DeletePost(IdInput input)
        {
            var operateStatus = new OperateStatus();
            //判断是否具有人员
            var permissionUsers =await  _permissionUserLogic.GetPermissionUsersByPrivilegeMasterAdnPrivilegeMasterValue(EnumPrivilegeMaster.岗位,
                    input.Id);
            if (permissionUsers.Any())
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message = ResourceSystem.具有人员;
                return operateStatus;
            }
            //判断是否具有按钮权限
            var functionPermissions =await 
                _permissionLogic.GetPermissionByPrivilegeMasterValue(
                new SystemPermissionByPrivilegeMasterValueInput
                {
                    PrivilegeAccess = EnumPrivilegeAccess.菜单按钮,
                    PrivilegeMasterValue = input.Id,
                    PrivilegeMaster = EnumPrivilegeMaster.岗位
                });
            if (functionPermissions.Any())
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message = ResourceSystem.具有功能项权限;
                return operateStatus;
            }
            //判断是否具有菜单权限
            var menuPermissions =await 
                _permissionLogic.GetPermissionByPrivilegeMasterValue(
                new SystemPermissionByPrivilegeMasterValueInput
                {
                    PrivilegeAccess = EnumPrivilegeAccess.菜单,
                    PrivilegeMasterValue = input.Id,
                    PrivilegeMaster = EnumPrivilegeMaster.岗位
                });
            if (menuPermissions.Any())
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message =  ResourceSystem.具有菜单权限;
                return operateStatus;
            }
            return await DeleteAsync(new SystemPost
            {
                PostId = input.Id
            });
        }

        #endregion
    }
}