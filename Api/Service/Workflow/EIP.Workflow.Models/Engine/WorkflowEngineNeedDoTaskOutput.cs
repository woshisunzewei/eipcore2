using System;
using EIP.Common.Entities.Dtos;

namespace EIP.Workflow.Models.Engine
{
    /// <summary>
    /// 需要处理的流程
    /// </summary>
    public class WorkflowEngineNeedDoTaskOutput : IOutputDto
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        public Guid TaskId { get; set; }

        /// <summary>
        /// 实例Id
        /// </summary>
        public Guid ProcessInstanceId { get; set; }
        
        /// <summary>
        /// 流程标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 当前活动Id
        /// </summary>
        public Guid CurrentActivityId { get; set; }

        /// <summary>
        /// 当前流程步骤名称
        /// </summary>
        public string CurrentActivityName { get; set; }

        /// <summary>
        /// 发送人员名字
        /// </summary>
        public string SendUserName { get; set; }

        /// <summary>
        /// 紧急程度
        /// </summary>
        public byte Urgency { get; set; }
        
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// 流程Id
        /// </summary>
        public Guid ProcessId { get; set; }

        /// <summary>
        /// 停留时间
        /// </summary>
        public string StayTime
        {
            get
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(SendTime);
                return timeSpan.Days + "天" + timeSpan.Hours + "小时";
            }
        }
    }
}