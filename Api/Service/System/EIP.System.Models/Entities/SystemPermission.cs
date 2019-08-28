using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIP.System.Models.Entities
{
    /// <summary>
    /// System_Permission表实体类
    /// </summary>
    [Serializable]
    [Table("System_Permission")]
    public class SystemPermission 
    {
        /// <summary>
        /// 权限归属(0菜单、1按钮等)
        /// </summary>		
        public short PrivilegeAccess { get; set; }

        /// <summary>
        /// 对应类型主键值(角色id,人员id,组id,岗位id,)
        /// </summary>		
        public Guid PrivilegeMasterValue { get; set; }

        /// <summary>
        /// 对应权限归属主键(菜单id,按钮id等)
        /// </summary>		
        public Guid PrivilegeAccessValue { get; set; }

        /// <summary>
        /// 给予权限类型
        /// </summary>
        public short PrivilegeMaster { get; set; }

        /// <summary>
        /// 对应菜单Id
        /// </summary>
        public Guid? PrivilegeMenuId { get; set; }
    }
}
