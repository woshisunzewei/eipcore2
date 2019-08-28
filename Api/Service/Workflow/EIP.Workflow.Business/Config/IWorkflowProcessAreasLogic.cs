using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Workflow.Models.Engine;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.Business.Config
{
    /// <summary>
    ///     区块
    /// </summary>
    public interface IWorkflowProcessAreasLogic : IAsyncLogic<WorkflowProcessAreas>
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