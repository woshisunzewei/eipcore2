using EIP.Common.Entities.Dtos;
using EIP.System.Models.Entities;

namespace EIP.System.Models.Dtos.Identity
{
    public class SystemGroupOutput : SystemGroup, IOutputDto
    {
        /// <summary>
        /// 归属名称
        /// </summary>
        public string BelongToName { get; set; }

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