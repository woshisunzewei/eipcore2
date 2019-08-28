using System;
using EIP.Common.Entities.Dtos;

namespace EIP.Workflow.Models.Engine
{
    /// <summary>
    /// 流程接受人员
    /// </summary>
    public class WorkflowEngineReceiveUserOutput : IOutputDto
    {
        /// <summary>
        /// 接收者Id
        /// </summary>
        public Guid ReceiveUserId { get; set; }

        /// <summary>
        /// 接收者名字
        /// </summary>
        public string ReceiveUserName { get; set; }

        /// <summary>
        /// 接收者组织机构
        /// </summary>
        public string ReceiveUserOrganization { get; set; }
    }
}