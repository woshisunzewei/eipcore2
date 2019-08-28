using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using EIP.System.Models.Enums;

namespace EIP.System.DataAccess.Permission
{
    public interface ISystemPermissionUserRepository : IAsyncRepository<SystemPermissionUser>
    {
        /// <summary>
        ///     根据用户Id删除权限用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> DeletePermissionUser(IdInput input);

        /// <summary>
        ///     根据用户Id删除权限用户信息
        /// </summary>
        /// <param name="privilegeMaster"></param>
        /// <param name="privilegeMasterValue"></param>
        /// <returns></returns>
        Task<int> DeletePermissionUser(EnumPrivilegeMaster privilegeMaster,
            Guid privilegeMasterValue);

        /// <summary>
        ///     删除用户
        /// </summary>
        /// <param name="privilegeMasterUserId">用户Id</param>
        /// <param name="privilegeMasterValue">归属类型Id:组织机构、角色、岗位、组</param>
        /// <param name="privilegeMaster">归属人员类型:组织机构、角色、岗位、组</param>
        /// <returns></returns>
        Task<int> DeletePrivilegeMasterUser(Guid privilegeMasterUserId,
            Guid privilegeMasterValue,
            EnumPrivilegeMaster privilegeMaster);

        /// <summary>
        ///     删除用户
        /// </summary>
        /// <param name="privilegeMasterUserId">用户Id</param>
        /// <param name="privilegeMaster">归属人员类型:组织机构、角色、岗位、组</param>
        /// <returns></returns>
        Task<int> DeletePrivilegeMasterUser(Guid privilegeMasterUserId,
            EnumPrivilegeMaster privilegeMaster);

        /// <summary>
        ///     根据用户Id删除用户归属类型
        /// </summary>
        /// <param name="privilegeMaster">归属人员类型:组织机构、角色、岗位、组</param>
        /// <param name="privilegeMasterUserId">用户Id</param>
        /// <returns></returns>
        Task<int> DeletePermissionMaster(EnumPrivilegeMaster privilegeMaster,
            Guid privilegeMasterUserId);

        /// <summary>
        ///     根据用户Id获取角色、组、岗位信息
        /// </summary>
        /// <param name="input">人员Id</param>
        /// <returns></returns>
        Task<IEnumerable<SystemPrivilegeDetailListOutput>> GetSystemPrivilegeDetailOutputsByUserId(IdInput input);

        /// <summary>
        ///     根据特权类型及特权id获取特权用户信息
        /// </summary>
        /// <param name="privilegeMaster">特权类型</param>
        /// <param name="privilegeMasterValue">特权id</param>
        /// <returns></returns>
        Task<IEnumerable<SystemPermissionUser>> GetPermissionUsersByPrivilegeMasterAdnPrivilegeMasterValue(
            EnumPrivilegeMaster privilegeMaster,
            Guid? privilegeMasterValue = null);

        /// <summary>
        ///     获取菜单、字段对应拥有者信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemPrivilegeDetailListOutput>> GetSystemPrivilegeDetailOutputsByAccessAndValue(
            SystemPrivilegeDetailInput input);
    }
}