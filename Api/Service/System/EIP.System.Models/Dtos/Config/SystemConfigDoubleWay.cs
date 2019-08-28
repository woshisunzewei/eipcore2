using System;

namespace EIP.System.Models.Dtos.Config
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SystemConfigDoubleWay
    {
        /// <summary>
        /// ConfigId
        /// </summary>
        public Guid C{ get; set; }

        /// <summary>
        /// 值：Value
        /// </summary>
        public string V { get; set; }
    }
}