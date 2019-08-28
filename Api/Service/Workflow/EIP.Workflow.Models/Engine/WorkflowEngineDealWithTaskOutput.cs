using EIP.Common.Entities.Dtos;
using System;

namespace EIP.Workflow.Models.Engine
{
    /// <summary>
    ///     活动输出
    /// </summary>
    public class WorkflowEngineDealWithTaskOutput : IOutputDto
    {
        /// <summary>
        ///     流程Id
        /// </summary>
        public Guid ProcessId { get; set; }

        /// <summary>
        ///     开始活动Id
        /// </summary>
        public Guid ActivityId { get; set; }

        /// <summary>
        ///     表单地址
        /// </summary>
        public string FormUrl { get; set; }

        /// <summary>
        ///     按钮字符串
        /// </summary>
        public string Buttons { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 流程标题
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// 紧急程度
        /// </summary>
        public byte Urgency { get; set; }

        ///// <summary>
        /////     按钮集合
        ///// </summary>
        //public IEnumerable<WorkflowButton> ButtonList
        //{
        //    get { return Buttons.JsonStringToList<WorkflowButton>(); }
        //}
    }
}