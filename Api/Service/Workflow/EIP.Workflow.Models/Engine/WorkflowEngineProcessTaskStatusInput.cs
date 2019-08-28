using EIP.Common.Core.Auth;
using EIP.Common.Entities.Dtos;
using EIP.Workflow.Models.Enums;
using System;

namespace EIP.Workflow.Models.Engine
{
    public class WorkflowEngineProcessTaskStatusInput : IInputDto
    {
        /// <summary>
        /// 用户
        /// </summary>
        public PrincipalUser PrincipalUser { get; set; }

        /// <summary>
        /// 任务Id
        /// </summary>
        public Guid TaskId { get; set; }

        /// <summary>
        /// 活动状态
        /// </summary>
        public EnumActivityStatus Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Comment { get; set; }
    }
}