using System;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Workflow.DataAccess.Config;
using EIP.Workflow.Models.Engine;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.Business.Config
{
    public class WorkflowProcessInstanceLogic : DapperAsyncLogic<WorkflowProcessInstance>, IWorkflowProcessInstanceLogic
    {
        /// <summary>
        /// 更新流程状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<OperateStatus> UpdateWorkflowProcessInstanceStatus(WorkflowEngineRunnerInput input)
        {
            throw new NotImplementedException();
        }

        #region 构造函数

        private readonly IWorkflowProcessInstanceRepository _repository;

        public WorkflowProcessInstanceLogic(IWorkflowProcessInstanceRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        #endregion
    }
}