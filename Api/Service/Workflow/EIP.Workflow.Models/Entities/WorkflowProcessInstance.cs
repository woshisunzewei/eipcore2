using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EIP.Common.Entities.CustomAttributes;

namespace EIP.Workflow.Models.Entities
{
    /// <summary>
    /// Workflow_ProcessInstance表实体类
    /// </summary>
    [Serializable]
    [Table("Workflow_ProcessInstance")]
    [Db("EIP_Workflow")]
    public class WorkflowProcessInstance 
    {
        /// <summary>
        /// 
        /// </summary>		
        [Key]
        public Guid ProcessInstanceId { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        public Guid ProcessId { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        public string Title { get; set; }
        
        /// <summary>
        /// 流程状态
        /// </summary>		
        public byte Status { get; set; }

        /// <summary>
        /// 紧急程度
        /// </summary>
        public byte Urgency { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        public Guid CreateUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        public string CreateUserName { get; set; }

        /// <summary>
        /// 创建者组织机构
        /// </summary>
        public string CreateUserOrganization { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        public Guid? UpdateUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        public string UpdateUserName { get; set; }

        /// <summary>
        /// 修改者组织机构
        /// </summary>
        public string UpdateUserOrganization { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        public Guid? EndUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        public string EndUserName { get; set; }

        /// <summary>
        /// 结束者组织机构
        /// </summary>
        public string EndUserOrganization { get; set; }
    }
}
