using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Workflow.Models.Engine;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.DataAccess.Config
{
    /// <summary>
    ///     区块
    /// </summary>
    public interface IWorkflowProcessAreasRepository : IAsyncRepository<WorkflowProcessAreas>
    {
        /// <summary>
        /// 获取区域监控数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<WorkflowProcessAreas>> GetWorkflowEngineTrackAreasOutput(
            WorkflowEngineRunnerInput input);
    }
}