using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Workflow.Models.Engine;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.Business.Config
{
    public interface IWorkflowProcessInstanceLineLogic : IAsyncLogic<WorkflowProcessInstanceLine>
    {
        /// <summary>
        /// 更新连线状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperateStatus> UpdateLineMarked(WorkflowEngineActivityOrLineMarkedInput input);

        /// <summary>
        /// 获取连线监控数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<WorkflowEngineTrackLineOutput>> GetWorkflowEngineTrackLineOutput(
            WorkflowEngineRunnerInput input);
    }
}
