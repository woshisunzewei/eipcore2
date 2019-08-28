using System;
using System.Collections.Generic;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Enums;

namespace EIP.System.Models.Dtos.Permission
{
    /// <summary>
    /// 保存权限输入参数
    /// </summary>
    public class SystemPermissionSaveInput : IInputDto
    {
        /// <summary>
        /// 添加类型:菜单、功能项
        /// </summary>
        public EnumPrivilegeAccess PrivilegeAccess { get; set; }

        /// <summary>
        /// 添加的类型:角色、组织机构、组、岗位、人员
        /// </summary>
        public EnumPrivilegeMaster PrivilegeMaster { get; set; }

        /// <summary>
        /// 权限信息
        /// </summary>
        public IList<Guid> Permissiones { get; set; }

        /// <summary>
        /// 角色、组织机构、组、岗位、人员对应的主键Id
        /// </summary>
        public Guid PrivilegeMasterValue { get; set; }

        /// <summary>
        /// 对应菜单Id(字段权限、数据权限才有该字段)
        /// </summary>
        public Guid? PrivilegeMenuId { get; set; }

        /// <summary>
        /// 权限信息
        /// </summary>
        public string MenuPermissions { get; set; }
    }
}