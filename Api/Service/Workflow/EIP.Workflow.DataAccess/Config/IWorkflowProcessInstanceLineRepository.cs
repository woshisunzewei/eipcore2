using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Workflow.Models.Engine;
using EIP.Workflow.Models.Engine;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.DataAccess.Config
{
    public interface IWorkflowProcessInstanceLineRepository : IAsyncRepository<WorkflowProcessInstanceLine>
    {
        /// <summary>
        /// 更新连线状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> UpdateLineMarked(WorkflowEngineActivityOrLineMarkedInput input);

        /// <summary>
        /// 获取连线监控数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<WorkflowEngineTrackLineOutput>> GetWorkflowEngineTrackLineOutput(WorkflowEngineRunnerInput input);
    }
}
