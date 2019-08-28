using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EIP.Common.Entities.CustomAttributes;

namespace EIP.Workflow.Models.Entities
{
    /// <summary>
    /// Workflow_Form表实体类
    /// </summary>
	[Serializable]
	[Table("Workflow_Form")]
	[Db("EIP_Workflow")]
    public class WorkflowForm
    {
        /// <summary>
        /// 主键id
        /// </summary>		
		[Key]
        public Guid FormId{ get; set; }
       
        /// <summary>
        /// 表单名称
        /// </summary>		
		public string Name{ get; set; }
       
        /// <summary>
        /// Html
        /// </summary>		
		public string Html{ get; set; }
       
        /// <summary>
        /// 从表设置数据:明细表使用
        /// </summary>		
		public string SubTableJson{ get; set; }
       
        /// <summary>
        /// 
        /// </summary>		
		public bool IsFreeze{ get; set; }
       
        /// <summary>
        /// 
        /// </summary>		
		public string Url{ get; set; }
       
        /// <summary>
        /// 表单地址
        /// </summary>		
		public Guid? DataBaseId{ get; set; }
       
        /// <summary>
        /// 
        /// </summary>		
        public string DataBaseTable { get; set; }
       
        /// <summary>
        /// 
        /// </summary>		
        public string DataBaseKey { get; set; }
       
        /// <summary>
        /// 
        /// </summary>		
        public string DataBaseTitle { get; set; }
       
        /// <summary>
        /// 
        /// </summary>		
		public string Remark{ get; set; }
       
        /// <summary>
        /// 备注
        /// </summary>		
		public int OrderNo{ get; set; }
       
        /// <summary>
        /// 排序
        /// </summary>		
		public DateTime? CreateTime{ get; set; }
       
        /// <summary>
        /// 创建时间
        /// </summary>		
		public Guid? CreateUserId{ get; set; }
       
        /// <summary>
        /// 创建者Id
        /// </summary>		
		public string CreateUserName{ get; set; }
       
        /// <summary>
        /// 创建人员名字
        /// </summary>		
		public DateTime? UpdateTime{ get; set; }
       
        /// <summary>
        /// 最有一次修改人员时间
        /// </summary>		
		public Guid? UpdateUserId{ get; set; }
       
        /// <summary>
        /// 最后一次修改人员Id
        /// </summary>		
		public string UpdateUserName{ get; set; }
		
   } 
}
