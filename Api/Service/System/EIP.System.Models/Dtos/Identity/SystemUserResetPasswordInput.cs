using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Identity
{
    /// <summary>
    /// 重置密码input
    /// </summary>
    public class SystemUserResetPasswordInput : IdInput
    {
        /// <summary>
        /// 加密后密码
        /// </summary>
        public string EncryptPassword { get; set; }
    }
}