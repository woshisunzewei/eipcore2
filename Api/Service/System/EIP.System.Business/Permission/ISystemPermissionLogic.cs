using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Tree;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using EIP.System.Models.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EIP.System.Business.Permission
{
    public interface ISystemPermissionLogic : IAsyncLogic<SystemPermission>
    {
        /// <summary>
        /// 根据状态为True的菜单信息
        /// </summary>
        /// <param name="input">权限类型</param>
        /// <returns></returns>
        Task<IEnumerable<SystemPermission>> GetPermissionByPrivilegeMasterValue(SystemPermissionByPrivilegeMasterValueInput input);

        /// <summary>
        ///     保存权限信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperateStatus> SavePermission(SystemPermissionSaveInput input);

        /// <summary>
        ///     根据用户Id获取用户具有的菜单权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetSystemPermissionMenuByUserId(Guid userId);

        /// <summary>
        ///     根据角色Id,岗位Id,组Id,人员Id获取具有的菜单信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns>树形菜单信息</returns>
        Task<IEnumerable<JsTreeEntity>> GetMenuHavePermissionByPrivilegeMasterValue(SystemPermissiontMenuHaveByPrivilegeMasterValueInput input);

        /// <summary>
        ///     根据特权类型查询对应拥有的功能项菜单信息
        /// </summary>
        /// <param name="privilegeMasterValue">特权id</param>
        /// <param name="privilegeMaster">特权枚举</param>
        /// <returns></returns>
        Task<IEnumerable<SystemMenuButton>> GetFunctionByPrivilegeMaster(Guid privilegeMasterValue,
            EnumPrivilegeMaster privilegeMaster);

        /// <summary>
        ///     根据特权类型查询对应拥有的数据信息
        /// </summary>
        /// <param name="privilegeMasterValue">特权id</param>
        /// <param name="privilegeMaster">特权枚举</param>
        /// <returns></returns>
        Task<IEnumerable<SystemData>> GetDataByPrivilegeMaster(Guid privilegeMasterValue,
            EnumPrivilegeMaster privilegeMaster);

        /// <summary>
        ///     获取登录人员对应菜单下的功能项
        /// </summary>
        /// <param name="mvcRote">路由信息</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        Task<IEnumerable<SystemMenuButton>> GetFunctionByMenuIdAndUserId(MvcRote mvcRote,
            Guid userId);

        /// <summary>
        /// 获取菜单、功能项等被使用的权限信息
        /// </summary>
        /// <param name="privilegeAccess">类型:菜单、功能项</param>
        /// <param name="privilegeAccessValue">对应值</param>
        /// <returns></returns>
        Task<IEnumerable<SystemPermission>> GetSystemPermissionsByPrivilegeAccessAndValue(EnumPrivilegeAccess privilegeAccess,
            Guid? privilegeAccessValue = null);

        /// <summary>
        /// 获取角色，组等具有的权限
        /// </summary>
        /// <param name="privilegeMaster">类型:角色，人员，组等</param>
        /// <param name="privilegeMasterValue">对应值</param>
        /// <returns></returns>
        Task<IEnumerable<SystemPermission>> GetSystemPermissionsByPrivilegeMasterValueAndValue(
            EnumPrivilegeMaster privilegeMaster,
            Guid? privilegeMasterValue = null);

        /// <summary>
        /// 删除获取菜单、功能项等被使用的权限信息
        /// </summary>
        /// <param name="privilegeAccess">类型:菜单、功能项</param>
        /// <param name="privilegeAccessValue">对应值</param>
        /// <returns></returns>
        Task<int> DeleteSystemPermissionsByPrivilegeAccessAndValue(EnumPrivilegeAccess privilegeAccess,
            Guid? privilegeAccessValue = null);

        /// <summary>
        /// 根据功能项获取权限信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemPermission>> GetSystemPermissionsMvcRote(SystemPermissionsByMvcRoteInput input);

        /// <summary>
        /// 获取字段权限Sql
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> GetFieldPermissionSql(SystemPermissionSqlInput input);

        /// <summary>
        /// 获取数据权限Sql
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> GetDataPermissionSql(SystemPermissionSqlInput input);
    }
}