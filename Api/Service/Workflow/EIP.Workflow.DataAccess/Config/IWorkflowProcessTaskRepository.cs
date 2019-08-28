using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Paging;
using EIP.Workflow.Models.Engine;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.DataAccess.Config
{
    /// <summary>
    /// 流程任务
    /// </summary>
    public interface IWorkflowProcessTaskRepository : IAsyncRepository<WorkflowProcessInstanceTask>
    {
        /// <summary>
        /// 修改任务状态
        /// </summary>
        /// <returns></returns>
        Task<int> UpdateProcessTaskStatus(WorkflowEngineProcessTaskStatusInput input);

        /// <summary>
        /// 获取任务历史轨迹
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<WorkflowEngineTrackForTableOutput>> GetWorkflowEngineTrackForTable(
            WorkflowEngineRunnerInput input);

        /// <summary>
        ///     已处理的任务(已处理事项)
        /// </summary>
        /// <returns></returns>
        Task<PagedResults<WorkflowEngineHaveDoTaskOutput>> GetWorkflowEngineHaveDoTaskOutput(WorkflowEngineRunnerInput input);
    }
}