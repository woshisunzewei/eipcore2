using System;
using System.Collections.Generic;
using EIP.Common.Entities.Dtos;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.Models.Dtos
{
    public class WorkflowSaveProcessInput : IInputDto
    {
         public Guid ProcessId { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>		
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 更新人员id
        /// </summary>		
        public Guid UpdateUserId { get; set; }

        /// <summary>
        /// 更新人员名称
        /// </summary>		
        public string UpdateUserName { get; set; }

        /// <summary>
        /// 设计Json
        /// </summary>		
        public string DesignJson { get; set; }

        /// <summary>
        /// 活动字符串
        /// </summary>
        public string Activity { get; set; }

        /// <summary>
        /// 连线字符串
        /// </summary>
        public string Line { get; set; }

        /// <summary>
        /// 连线字符串
        /// </summary>
        public string Areas { get; set; }

        /// <summary>
        /// 活动
        /// </summary>
        public IEnumerable<WorkflowProcessActivity> Activities { get; set; }

        /// <summary>
        /// 连线
        /// </summary>
        public IEnumerable<WorkflowProcessLine> Lines { get; set; }

        /// <summary>
        /// 区块
        /// </summary>
        public IEnumerable<WorkflowProcessAreas> Areases { get; set; }
    }
}