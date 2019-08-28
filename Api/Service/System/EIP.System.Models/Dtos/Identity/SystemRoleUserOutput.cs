using System;
using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Identity
{
    /// <summary>
    /// 系统角色用户实体
    /// </summary>
    public class SystemChosenUserOutput : IOutputDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户帐号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 归属组织机构
        /// </summary>
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// 是否已存在
        /// </summary>
        public bool Exist { get; set; }
    }
}