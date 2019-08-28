using System;
using EIP.Common.Entities.Dtos;

namespace EIP.Workflow.Models.Engine
{
    /// <summary>
    /// 流程跟踪-活动
    /// </summary>
    public class WorkflowEngineTrackActivityOutput : IOutputDto
    {
        public Guid ActivityId { get; set; }
        public string Name { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public string Type { get; set; }
        public int Width { get; set; }

        public int Height { get; set; }

        public bool Marked { get; set; }
    }

    /// <summary>
    /// 流程跟踪-连线
    /// </summary>
    public class WorkflowEngineTrackLineOutput : IOutputDto
    {
        public Guid LineId { get; set; }
        public string Type { get; set; }
        public Guid From { get; set; }
        public Guid To { get; set; }
        public string Name { get; set; }
        public decimal M { get; set; }
        public bool Marked { get; set; }
    }
}