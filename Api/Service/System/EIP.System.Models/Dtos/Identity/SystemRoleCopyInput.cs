using System;
using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Identity
{
    /// <summary>
    /// 复制参数
    /// </summary>
    public class SystemCopyInput:IdInput
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建用户Id
        /// </summary>		
        public Guid CreateUserId { get; set; }

        /// <summary>
        /// 创建人员名称
        /// </summary>
        public string CreateUserName { get; set; }
    }
}