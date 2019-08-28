using System;
using EIP.Common.Entities.Dtos;

namespace EIP.Workflow.Models.Engine
{
    /// <summary>
    /// 流程运行轨迹:表格形式
    /// </summary>
    public class WorkflowEngineTrackForTableOutput : IOutputDto
    {
        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }

        /// <summary>
        /// 处理人姓名
        /// </summary>
        public string DoUserName { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? DoTime { get; set; }

        /// <summary>
        /// 到达时间
        /// </summary>
        public DateTime ReceiveTime { get; set; }

        /// <summary>
        /// 意见
        /// </summary>
        public string Comment { get; set; }
    }
}