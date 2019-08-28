using System;
using EIP.Common.Entities.Dtos;

namespace EIP.Workflow.Models.Engine
{
    /// <summary>
    /// 所有发起过的流程(我的请求)
    /// </summary>
    public class WorkflowEngineHaveSendProcessOutput : IOutputDto
    {
        /// <summary>
        /// 流程实例Id
        /// </summary>
        public Guid ProcessInstanceId { get; set; }

        /// <summary>
        /// 流程Id
        /// </summary>
        public Guid ProcessId { get; set; }

        /// <summary>
        /// 流程归属
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 流程标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// 流程紧急程度
        /// </summary>
        public byte Urgency { get; set; }

        /// <summary>
        /// 发起时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 结束者
        /// </summary>
        public string EndUserName { get; set; }

        /// <summary>
        /// 结束者归属机构
        /// </summary>
        public string EndUserOrganization { get; set; }
    }
}