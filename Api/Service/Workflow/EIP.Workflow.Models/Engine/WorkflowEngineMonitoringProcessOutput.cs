using EIP.Common.Entities.Dtos;
namespace EIP.Workflow.Models.Engine
{
    /// <summary>
    /// 流程监控输出实体
    /// </summary>
    public class WorkflowEngineMonitoringProcessOutput : IOutputDto
    {
        /// <summary>
        /// 监控Json字符串(流程图根据此Json字符串显示流程过程图)
        /// </summary>
        public string MonitoringJson { get; set; }
    }
}