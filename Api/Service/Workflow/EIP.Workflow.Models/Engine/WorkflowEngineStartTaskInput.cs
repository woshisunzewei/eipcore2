using System;
using EIP.Common.Core.Auth;
using EIP.Common.Entities.Dtos;

namespace EIP.Workflow.Models.Engine
{
    /// <summary>
    /// 流程运行开始输入
    /// </summary>
    public class WorkflowEngineStartTaskInput : IInputDto
    {
        /// <summary>
        /// 流程Id
        /// </summary>
        public Guid ProcessId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 当前人员
        /// </summary>
        public PrincipalUser PrincipalUser { get; set; }

        /// <summary>
        /// 对应业务表Id  
        /// </summary>
        public Guid? AppInstanceId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 紧急程度
        /// </summary>
        public byte Urgency { get; set; }
    }
}