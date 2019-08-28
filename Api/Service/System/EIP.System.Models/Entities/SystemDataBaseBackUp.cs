using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIP.System.Models.Entities
{
    /// <summary>
    /// System_DataBaseBackUp表实体类
    /// </summary>
	[Serializable]
	[Table("System_DataBaseBackUp")]
    public class SystemDataBaseBackUp
    {
        /// <summary>
        /// 主键
        /// </summary>		
		[Key]
        public Guid BackUpId{ get; set; }

        /// <summary>
        /// 数据库外键
        /// </summary>
        public Guid DataBaseId { get; set; }

        /// <summary>
        /// 备份名称
        /// </summary>		
		public string Name{ get; set; }
       
        /// <summary>
        /// 备份时间
        /// </summary>		
		public DateTime BackUpTime{ get; set; }
       
        /// <summary>
        /// 还原时间
        /// </summary>		
		public DateTime? RestoreTime{ get; set; }
       
        /// <summary>
        /// 来源:0自动备份，1手工备份
        /// </summary>		
		public short From{ get; set; }
       
        /// <summary>
        /// 大小
        /// </summary>		
		public string Size{ get; set; }
       
        /// <summary>
        /// 备份文件路径
        /// </summary>		
		public string Path{ get; set; }
   } 
}
