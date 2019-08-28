using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Paging;
using EIP.Workflow.DataAccess.Config;
using EIP.Workflow.Models.Engine;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.Business.Config
{
    /// <summary>
    ///     流程任务
    /// </summary>
    public class WorkflowProcessTaskLogic : DapperAsyncLogic<WorkflowProcessInstanceTask>, IWorkflowProcessTaskLogic
    {
        public async Task<OperateStatus> UpdateProcessTaskStatus(WorkflowEngineProcessTaskStatusInput input)
        {
            var operateStatus = new OperateStatus();
            if ((await _workflowProcessTaskRepository.UpdateProcessTaskStatus(input)) > 0)
            {
                operateStatus.ResultSign = ResultSign.Successful;
            }
            return operateStatus;
        }

        public Task<IEnumerable<WorkflowEngineTrackForTableOutput>> GetWorkflowEngineTrackForTable(
            WorkflowEngineRunnerInput input)
        {
            return _workflowProcessTaskRepository.GetWorkflowEngineTrackForTable(input);
        }

        /// <summary>
        ///     已处理的任务(已处理事项)
        /// </summary>
        /// <returns></returns>
        public Task<PagedResults<WorkflowEngineHaveDoTaskOutput>> GetWorkflowEngineHaveDoTaskOutput(
            WorkflowEngineRunnerInput input)
        {
            return _workflowProcessTaskRepository.GetWorkflowEngineHaveDoTaskOutput(input);
        }

        #region 构造函数

        private readonly IWorkflowProcessTaskRepository _workflowProcessTaskRepository;

        public WorkflowProcessTaskLogic(IWorkflowProcessTaskRepository workflowProcessTaskRepository)
            : base(workflowProcessTaskRepository)
        {
            _workflowProcessTaskRepository = workflowProcessTaskRepository;
        }

        #endregion
    }
}