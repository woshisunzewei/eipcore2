using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIP.System.Models.Entities
{
    /// <summary>
    /// System_PermissionUser表实体类
    /// </summary>
    [Serializable]
    [Table("System_PermissionUser")]
    public class SystemPermissionUser 
    {
        /// <summary>
        /// 人员归属类型:角色0,组织机构1,岗位2,组3,人员4(用于查询某用户具有哪些岗位、组等)
        /// </summary>		
        public short PrivilegeMaster { get; set; }

        /// <summary>
        /// 对应类型Id(角色Id,岗位Id,组Id,人员Id)
        /// </summary>		
        public Guid PrivilegeMasterValue { get; set; }

        /// <summary>
        /// 人员Id
        /// </summary>		
        public Guid PrivilegeMasterUserId { get; set; }
    }
}
