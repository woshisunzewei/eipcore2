using EIP.Common.Core.Utils;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Entities;
using EIP.System.Models.Enums;

namespace EIP.System.Models.Dtos.Identity
{
    public class SystemOrganizationOutput : SystemOrganization, IOutputDto
    {
        /// <summary>
        /// 性质名称
        /// </summary>
        public string NatureName => Nature != null ? EnumUtil.GetEnumNameByIndex<EnumOrgNature>((int)Nature) : string.Empty;

        /// <summary>
        /// 父级名称
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 父级所有
        /// </summary>
        public string ParentNames { get; set; }


    }
}