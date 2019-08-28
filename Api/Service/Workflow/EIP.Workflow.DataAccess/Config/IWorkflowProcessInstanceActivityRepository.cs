using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Workflow.Models.Engine;
using EIP.Workflow.Models.Engine;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.DataAccess.Config
{
    public interface IWorkflowProcessInstanceActivityRepository : IAsyncRepository<WorkflowProcessInstanceActivity>
    {
        /// <summary>
        ///     获取下一步所有活动
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<WorkflowEngineNextActivitysDoubleWay>> GetWorkflowEngineNextActivitysDoubleWay(
            WorkflowEngineRunnerInput input);

        /// <summary>
        /// 更新活动状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> UpdateActivityMarked(WorkflowEngineActivityOrLineMarkedInput input);

        /// <summary>
        /// 获取活动监控数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<WorkflowEngineTrackActivityOutput>> GetWorkflowEngineTrackActivityOutput(WorkflowEngineRunnerInput input);

        /// <summary>
        ///     获取处理任务信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WorkflowEngineDealWithTaskOutput> GetWorkflowEngineDealWithTaskOutput(WorkflowEngineRunnerInput input);

        /// <summary>
        ///     获取处理任务信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WorkflowProcessInstanceActivity> GetProcessInstanceActivityByActivityIdAndProcessInstanceId(WorkflowEngineRunnerInput input);
    }
}
