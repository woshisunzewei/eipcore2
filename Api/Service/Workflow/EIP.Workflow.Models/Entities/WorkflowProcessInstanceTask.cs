using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EIP.Common.Entities.CustomAttributes;

namespace EIP.Workflow.Models.Entities
{
    /// <summary>
    /// Workflow_ProcessInstance_Task表实体类
    /// </summary>
    [Serializable]
    [Table("Workflow_ProcessInstance_Task")]
    [Db("EIP_Workflow")]
    public class WorkflowProcessInstanceTask
    {

        /// <summary>
        /// 任务Id
        /// </summary>		
        [Key]
        public Guid TaskId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>		
        public string Title { get; set; }

        /// <summary>
        /// 实例Id
        /// </summary>		
        public Guid ProcessInstanceId { get; set; }

        /// <summary>
        /// 上一个任务Id
        /// </summary>		
        public Guid? PrevTaskId { get; set; }

        /// <summary>
        /// 上一个活动Id
        /// </summary>		
        public Guid? PrevActivityId { get; set; }

        /// <summary>
        /// 上一个活动名称
        /// </summary>
        public string PrevActivityName { get; set; }

        /// <summary>
        /// 当前活动Id
        /// </summary>		
        public Guid CurrentActivityId { get; set; }

        /// <summary>
        /// 当前活动名称
        /// </summary>		
        public string CurrentActivityName { get; set; }

        /// <summary>
        /// 业务Id
        /// </summary>		
        public Guid? BusinessInstanceId { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        public bool? Type { get; set; }

        /// <summary>
        /// 发送人Id
        /// </summary>		
        public Guid SendUserId { get; set; }

        /// <summary>
        /// 发送人名字
        /// </summary>		
        public string SendUserName { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>		
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 接收人Id
        /// </summary>		
        public Guid? ReceiveUserId { get; set; }

        /// <summary>
        /// 接收人名字
        /// </summary>		
        public string ReceiveUserName { get; set; }

        /// <summary>
        /// 接收时间
        /// </summary>		
        public DateTime? ReceiveTime { get; set; }

        /// <summary>
        /// 处理人Id
        /// </summary>		
        public Guid? DoUserId { get; set; }

        /// <summary>
        /// 处理人名字
        /// </summary>		
        public string DoUserName { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>		
        public DateTime? DoTime { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        public DateTime? OpenTime { get; set; }

        /// <summary>
        /// 规定完成时间
        /// </summary>		
        public DateTime? ProvisionsComplateTime { get; set; }

        /// <summary>
        /// 实际完成时间
        /// </summary>		
        public DateTime? ActualComplateTime { get; set; }

        /// <summary>
        /// 意见
        /// </summary>		
        public string Comment { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        public bool? IsSign { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        public string Note { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        public int? Sort { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        public Guid? SubFlowGroupId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

    }
}
