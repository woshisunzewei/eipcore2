using System;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Enums;

namespace EIP.System.Models.Dtos.Permission
{
    /// <summary>
    ///  根据角色Id,岗位Id,组Id,人员Id获取具有的菜单信息
    /// </summary>
    public class SystemPermissiontMenuHaveByPrivilegeMasterValueInput : IInputDto
    {
        /// <summary>
        /// 根据角色Id,岗位Id,组Id,人员Id
        /// </summary>
        public Guid PrivilegeMasterValue { get; set; }

        /// <summary>
        /// 权限类型:角色、岗位、组、人员
        /// </summary>
        public EnumPrivilegeMaster PrivilegeMaster { get; set; }

        /// <summary>
        /// 权限归属:菜单,功能项,字段,数据权限
        ///     需要排除菜单信息:不在此类型范围类的不给与显示
        /// </summary>
        public EnumPrivilegeAccess? PrivilegeAccess { get; set; }
    }
}