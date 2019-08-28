using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Workflow.DataAccess.Config;
using EIP.Workflow.Models.Engine;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.Business.Config
{
    public class WorkflowProcessInstanceActivityLogic : DapperAsyncLogic<WorkflowProcessInstanceActivity>,
        IWorkflowProcessInstanceActivityLogic
    {
        /// <summary>
        ///     获取下一步所有活动
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IEnumerable<WorkflowEngineNextActivitysDoubleWay>> GetWorkflowEngineNextActivitysDoubleWay(
            WorkflowEngineRunnerInput input)
        {
            return await _repository.GetWorkflowEngineNextActivitysDoubleWay(input);
        }

        /// <summary>
        ///     更新活动状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OperateStatus> UpdateActivityMarked(WorkflowEngineActivityOrLineMarkedInput input)
        {
            var operateStatus = new OperateStatus();
            if ((await _repository.UpdateActivityMarked(input)) > 0)
            {
                operateStatus.ResultSign = ResultSign.Successful;
                return operateStatus;
            }
            return operateStatus;
        }

        /// <summary>
        ///     获取活动监控数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IEnumerable<WorkflowEngineTrackActivityOutput>> GetWorkflowEngineTrackActivityOutput(
            WorkflowEngineRunnerInput input)
        {
            return await _repository.GetWorkflowEngineTrackActivityOutput(input);
        }

        /// <summary>
        ///     获取处理任务信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<WorkflowEngineDealWithTaskOutput> GetWorkflowEngineDealWithTaskOutput(
            WorkflowEngineRunnerInput input)
        {
            return await _repository.GetWorkflowEngineDealWithTaskOutput(input);
        }

        /// <summary>
        ///     获取流程步骤信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<WorkflowProcessInstanceActivity> GetProcessInstanceActivityByActivityIdAndProcessInstanceId(
            WorkflowEngineRunnerInput input)
        {
            return await _repository.GetProcessInstanceActivityByActivityIdAndProcessInstanceId(input);
        }

        #region 构造函数

        private readonly IWorkflowProcessInstanceActivityRepository _repository;

        public WorkflowProcessInstanceActivityLogic(IWorkflowProcessInstanceActivityRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        #endregion
    }
}