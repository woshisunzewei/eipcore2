using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EIP.Common.Entities.CustomAttributes;

namespace EIP.Workflow.Models.Entities
{
    /// <summary>
    /// Workflow_Archives表实体类
    /// </summary>
	[Serializable]
    [Table("Workflow_Archives")]
    [Db("EIP_Workflow")]
    public class WorkflowArchives
    {
        /// <summary>
        /// 归档Id
        /// </summary>		
		[Key]
        public Guid ArchivesId { get; set; }

        /// <summary>
        /// 流程实例Id
        /// </summary>		
        public Guid ProcessInstanceId { get; set; }

        /// <summary>
        /// 流程实例名称
        /// </summary>		
        public string ProcessName { get; set; }

        /// <summary>
        /// 步骤Id
        /// </summary>		
        public Guid StepId { get; set; }

        /// <summary>
        /// 步骤名称
        /// </summary>		
        public string StepName { get; set; }

        /// <summary>
        /// 任务Id
        /// </summary>		
        public Guid TaskId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>		
        public string Title { get; set; }

        /// <summary>
        /// 正文
        /// </summary>		
        public string Contents { get; set; }

        /// <summary>
        /// 审批内容
        /// </summary>		
        public string Comments { get; set; }

        /// <summary>
        /// 此时运行的流程Json图
        /// </summary>		
        public string RunningJson { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateTime { get; set; }

    }
}
