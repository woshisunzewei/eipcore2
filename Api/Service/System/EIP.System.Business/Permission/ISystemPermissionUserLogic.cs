using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using EIP.System.Models.Enums;

namespace EIP.System.Business.Permission
{
    /// <summary>
    ///     权限用户业务逻辑接口
    /// </summary>
    public interface ISystemPermissionUserLogic : IAsyncLogic<SystemPermissionUser>
    {
        /// <summary>
        ///     保存各种用户:组织机构、岗位、组、人员
        /// </summary>
        /// <param name="master">类型</param>
        /// <param name="value">业务表Id：如组织机构Id、组Id、岗位Id、人员Id等</param>
        /// <param name="userIds">权限类型:组织机构、组、岗位、人员Id</param>
        /// <returns></returns>
        Task<OperateStatus> SavePermissionUser(EnumPrivilegeMaster master,
            Guid value,
            IList<Guid> userIds);

        /// <summary>
        ///     保存各种用户:组织机构、岗位、组、人员【先进行删除,再进行添加】
        /// </summary>
        /// <param name="master">类型</param>
        /// <param name="value">业务表Id：如组织机构Id、组Id、岗位Id、人员Id等</param>
        /// <param name="userIds">权限类型:组织机构、组、岗位、人员Id</param>
        /// <returns></returns>
        Task<OperateStatus> SavePermissionUserBeforeDelete(EnumPrivilegeMaster master,
            Guid value,
            IList<Guid> userIds);

        /// <summary>
        ///     删除用户
        /// </summary>
        /// <param name="input">权限类型:组织机构、组、岗位、人员Id</param>
        /// <returns></returns>
        Task<OperateStatus> DeletePermissionUser(IdInput input);

        /// <summary>
        ///     保存用户权限类型
        /// </summary>
        /// <param name="privilegeMaster">类型</param>
        /// <param name="privilegeMasterUserId">业务表Id：如组织机构Id、组Id、岗位Id、人员Id等</param>
        /// <param name="privilegeMasterValue">权限类型:角色Id</param>
        /// <returns></returns>
        Task<OperateStatus> SavePermissionMasterValueBeforeDelete(EnumPrivilegeMaster privilegeMaster,
            Guid privilegeMasterUserId,
            IList<Guid> privilegeMasterValue);

        /// <summary>
        ///     删除用户
        /// </summary>
        /// <param name="privilegeMasterUserId">用户Id</param>
        /// <param name="privilegeMasterValue">归属类型Id:组织机构、角色、岗位、组</param>
        /// <param name="privilegeMaster">归属人员类型:组织机构、角色、岗位、组</param>
        /// <returns></returns>
        Task<OperateStatus> DeletePrivilegeMasterUser(Guid privilegeMasterUserId,
            Guid privilegeMasterValue,
            EnumPrivilegeMaster privilegeMaster);

        /// <summary>
        ///     删除用户对应权限数据
        /// </summary>
        /// <param name="privilegeMasterUserId">用户Id</param>
        /// <param name="privilegeMaster">归属人员类型:组织机构、角色、岗位、组</param>
        /// <returns></returns>
        Task<OperateStatus> DeletePrivilegeMasterUser(Guid privilegeMasterUserId,
            EnumPrivilegeMaster privilegeMaster);

        /// <summary>
        ///     根据用户Id获取角色、组、岗位信息
        /// </summary>
        /// <param name="input">人员Id</param>
        /// <returns></returns>
        Task<IEnumerable<SystemPrivilegeDetailListOutput>> GetSystemPrivilegeDetailOutputsByUserId(IdInput input);

        /// <summary>
        ///     获取菜单、字段对应拥有者信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SystemPrivilegeDetailOutput> GetSystemPrivilegeDetailOutputsByAccessAndValue(
            SystemPrivilegeDetailInput input);

        /// <summary>
        ///     根据特权类型及特权id获取特权用户信息
        /// </summary>
        /// <param name="privilegeMaster">特权类型</param>
        /// <param name="privilegeMasterValue">特权id</param>
        /// <returns></returns>
        Task<IEnumerable<SystemPermissionUser>> GetPermissionUsersByPrivilegeMasterAdnPrivilegeMasterValue(
            EnumPrivilegeMaster privilegeMaster,
            Guid? privilegeMasterValue = null);
    }
}