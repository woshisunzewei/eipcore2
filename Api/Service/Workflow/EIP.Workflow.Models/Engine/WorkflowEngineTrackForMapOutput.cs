using EIP.Common.Entities.Dtos;

namespace EIP.Workflow.Models.Engine
{
    /// <summary>
    /// 流程运行轨迹:流程图
    /// </summary>
    public class WorkflowEngineTrackForMapOutput : IOutputDto
    {
        /// <summary>
        /// 正在执行的Json字符串
        /// </summary>
        public string DesignJson { get; set; }
    }
}