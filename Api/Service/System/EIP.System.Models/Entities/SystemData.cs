using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIP.System.Models.Entities
{
    /// <summary>
    /// System_DataLog表实体类
    /// </summary>
    [Serializable]
    [Table( "System_Data")]
    public class SystemData 
    {
        /// <summary>
        /// 主键Id
        /// </summary>		
        [Key]
        public Guid DataId { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>		
        public Guid MenuId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 规则sql
        /// </summary>
        public string RuleSql { get; set; }

        /// <summary>
        /// 规则Json
        /// </summary>		
        public string RuleJson { get; set; }

        /// <summary>
        /// 规则Html
        /// </summary>
        public string RuleHtml { get; set; }

        /// <summary>
        /// 冻结
        /// </summary>
        public bool IsFreeze { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNo { get; set; }=0;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
