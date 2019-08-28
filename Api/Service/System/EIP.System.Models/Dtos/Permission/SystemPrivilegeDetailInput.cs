using EIP.Common.Entities.Dtos;
using EIP.System.Models.Enums;

namespace EIP.System.Models.Dtos.Permission
{
    /// <summary>
    /// 查询系统权限详情输入参数
    /// </summary>
    public class SystemPrivilegeDetailInput : IdInput
    {
        /// <summary>
        /// 权限归属类型
        /// </summary>
        public EnumPrivilegeAccess Access { get; set; }
    }
}