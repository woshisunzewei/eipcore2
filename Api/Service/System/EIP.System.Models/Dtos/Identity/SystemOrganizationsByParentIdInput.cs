using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Identity
{
    public class SystemOrganizationsByParentIdInput: NullableIdInput
    {
        /// <summary>
        /// 包含本级
        /// </summary>
        public bool HaveSelf { get; set; } = true;
    }
}