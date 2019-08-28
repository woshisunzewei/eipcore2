using System;
using EIP.Common.Core.Auth;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Paging;
using EIP.Workflow.Models.Enums;

namespace EIP.Workflow.Models.Engine
{
    /// <summary>
    ///     运行时参数
    /// </summary>
    public class WorkflowEngineRunnerInput : QueryParam, IInputDto
    {
        /// <summary>
        ///     流程Id
        /// </summary>
        public Guid ProcessId { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     实例Id
        /// </summary>
        public Guid ProcessInstanceId { get; set; }

        /// <summary>
        ///     当前活动Id
        /// </summary>
        public Guid CurrentActivityId { get; set; }

        /// <summary>
        ///     当前活动名称
        /// </summary>
        public string CurrentActivityName { get; set; }

        /// <summary>
        ///     当前任务Id
        /// </summary>
        public Guid CurrentTaskId { get; set; }

        /// <summary>
        /// 当前用户
        /// </summary>
        public PrincipalUser CurrentUser { get; set; }

        /// <summary>
        /// 流程意见
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 流程状态
        /// </summary>
        public EnumProcessStatu ProcessStatu { get; set; }
    }
}