using System;
using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Identity
{
    public class SystemOrganizationInput:IdInput
    {
        /// <summary>
        /// 父级Id
        /// </summary>
        public Guid ParentId { get; set; }
    }
}