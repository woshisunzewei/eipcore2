using EIP.Common.Entities.Dtos;
using EIP.System.Models.Entities;

namespace EIP.System.Models.Dtos.Identity
{
    /// <summary>
    /// 角色Dto
    /// </summary>
    public class SystemRoleOutput : SystemRole, IOutputDto
    {
        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string OrganizationNames { get; set; }
    }
}