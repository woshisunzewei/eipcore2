using EIP.Common.Core.Auth;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Enums;

namespace EIP.System.Models.Dtos.Permission
{
    /// <summary>
    /// 获取权限sql输入参数
    /// </summary>
    public class SystemPermissionSqlInput : IInputDto
    {
        /// <summary>
        /// Mvc路由信息
        /// </summary>
        public EnumPermissionRoteConvert EnumPermissionRoteConvert { get; set; }

        /// <summary>
        /// 登录人信息
        /// </summary>
        public PrincipalUser PrincipalUser { get; set; }
    }
}