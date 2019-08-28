using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EIP.Common.Entities.CustomAttributes;

namespace EIP.Workflow.Models.Entities
{
    /// <summary>
    /// Workflow_Seal表实体类
    /// </summary>
	[Serializable]
	[Table("Workflow_Seal")]
	[Db("EIP_Workflow")]
    public class WorkflowSeal
    {
        /// <summary>
        /// 印章Id
        /// </summary>		
		[Key]
        public Guid SealId{ get; set; }
       
        /// <summary>
        /// 归属人员
        /// </summary>		
		public Guid CommandId{ get; set; }
       
        /// <summary>
        /// 类型
        /// </summary>		
		public int Type{ get; set; }
       
        /// <summary>
        /// 印章地址
        /// </summary>		
		public string Url{ get; set; }
       
        /// <summary>
        /// 排序
        /// </summary>		
		public int? Order{ get; set; }
       
        /// <summary>
        /// 备注
        /// </summary>		
		public string Remark{ get; set; }
       
        /// <summary>
        /// 创建时间
        /// </summary>		
		public DateTime CreateTime{ get; set; }
       
        /// <summary>
        /// 创建用户Id
        /// </summary>		
		public Guid CreateUserId{ get; set; }
       
        /// <summary>
        /// 创建用户名称
        /// </summary>		
		public string CreateUserName{ get; set; }
       
        /// <summary>
        /// 更新时间
        /// </summary>		
		public DateTime? UpdateTime{ get; set; }
       
        /// <summary>
        /// 更新用户Id
        /// </summary>		
		public Guid? UpdateUserId{ get; set; }
       
        /// <summary>
        /// 更新用户名称
        /// </summary>		
		public string UpdateUserName{ get; set; }
		
   } 
}
