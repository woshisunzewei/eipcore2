using EIP.Common.Entities.Dtos;
using EIP.Workflow.Models.Enums;
using System;

namespace EIP.Workflow.Models.Engine
{
    /// <summary>
    /// 下一步活动信息
    /// </summary>
    public class WorkflowEngineNextActivitysDoubleWay : IDoubleWayDto
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public Guid ActivityId { get; set; }

        /// <summary>
        /// 活动名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 处理类型
        /// </summary>
        public EnumActivityProcessorType ProcessorType { get; set; }

        /// <summary>
        /// 处理者:可为角色,若多个则用逗号分割
        /// </summary>
        public string ProcessorHandler { get; set; }

        /// <summary>
        /// 流程类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 流程活动类型
        /// </summary>
        public EnumAcitvityType AcitvityType
        {
            get
            {
                switch (Type)
                {
                    case "start round":
                        return EnumAcitvityType.开始;
                    case "end round":
                        return EnumAcitvityType.结束;
                    default:
                        return EnumAcitvityType.审批;
                }
            }
        }
    }
}