using EIP.Common.Entities.Dtos;
namespace EIP.System.Models.Dtos.Identity
{
    /// <summary>
    /// 修改密码
    /// </summary>
    public class SystemUserChangePasswordInput : IdInput
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        public string ConfirmNewPassword { get; set; }
    }
}