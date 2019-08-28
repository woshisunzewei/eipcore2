using System;
using EIP.Common.Entities.Dtos;

namespace EIP.Workflow.Models.Engine
{
    /// <summary>
    /// 已处理事项(已办事项)
    /// </summary>
    public class WorkflowEngineHaveDoTaskOutput : IOutputDto
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        public Guid TaskId { get; set; }

        /// <summary>
        /// 流程实例Id
        /// </summary>
        public Guid ProcessInstanceId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 流程实例名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 处理步骤名称
        /// </summary>
        public string DoCurrentActivityName { get; set; }

        /// <summary>
        /// 流程状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 流程实例紧急程度
        /// </summary>
        public int Urgency { get; set; }

        /// <summary>
        /// 流程处理时发生时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 流程处理时发送人员
        /// </summary>
        public string SendUserName { get; set; }

        /// <summary>
        /// 接受任务人员
        /// </summary>
        public string ReceiveUserName { get; set; }

        /// <summary>
        /// 当前任务活动名称
        /// </summary>
        public string CurrentActivityName { get; set; }

        /// <summary>
        /// 流程Id
        /// </summary>
        public Guid ProcessId { get; set; }

        /// <summary>
        /// 总个数
        /// </summary>
        public int RecordCount { get; set; }

    }
}