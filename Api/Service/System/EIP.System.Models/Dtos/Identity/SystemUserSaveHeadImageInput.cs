using System;

namespace EIP.System.Models.Dtos.Identity
{
    /// <summary>
    /// 保存用户头像
    /// </summary>
    public class SystemUserSaveHeadImageInput
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImage { get; set; }

    }
}