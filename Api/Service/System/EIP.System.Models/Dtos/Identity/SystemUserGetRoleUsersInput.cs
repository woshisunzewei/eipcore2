using System;
using EIP.System.Models.Enums;

namespace EIP.System.Models.Dtos.Identity
{
    public class SystemUserGetChosenPrivilegeMasterUser
    {
        /// <summary>
        /// 归属人员类型:组织机构、角色、岗位、组
        /// </summary>
        public EnumPrivilegeMaster PrivilegeMaster { get; set; }

        /// <summary>
        /// 组织机构Id、角色Id、岗位Id、组Id
        /// </summary>
        public Guid PrivilegeMasterValue { get; set; }
    }
}