using System;
using EIP.Common.Entities.Dtos;

namespace EIP.Workflow.Models.Engine
{
    public class WorkflowEngineActivityOrLineMarkedInput:IInputDto
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public Guid Activity { get; set; }

        /// <summary>
        /// 执行活动
        /// </summary>
        public Guid ToActivity { get; set; }

        /// <summary>
        /// 实例Id
        /// </summary>
        public Guid ProcessInstanceId { get; set; }
    }
}