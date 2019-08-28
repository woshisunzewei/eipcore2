using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Workflow.DataAccess.Config;
using EIP.Workflow.Models.Engine;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.Business.Config
{
    public class WorkflowProcessInstanceLineLogic : DapperAsyncLogic<WorkflowProcessInstanceLine>,
        IWorkflowProcessInstanceLineLogic
    {
        /// <summary>
        ///     更新连线状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OperateStatus> UpdateLineMarked(WorkflowEngineActivityOrLineMarkedInput input)
        {
            var operateStatus = new OperateStatus();
            if ((await _repository.UpdateLineMarked(input)) > 0)
            {
                operateStatus.ResultSign = ResultSign.Successful;
                return operateStatus;
            }
            return operateStatus;
        }

        /// <summary>
        /// 获取连线监控数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<IEnumerable<WorkflowEngineTrackLineOutput>> GetWorkflowEngineTrackLineOutput(
            WorkflowEngineRunnerInput input)
        {
            return _repository.GetWorkflowEngineTrackLineOutput(input);
        }

        #region 构造函数

        private readonly IWorkflowProcessInstanceLineRepository _repository;

        public WorkflowProcessInstanceLineLogic(IWorkflowProcessInstanceLineRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        #endregion
    }
}