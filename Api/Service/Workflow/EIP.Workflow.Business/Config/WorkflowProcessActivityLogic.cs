using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities.Dtos;
using EIP.Workflow.DataAccess.Config;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.Business.Config
{
    public class WorkflowProcessActivityLogic : DapperAsyncLogic<WorkflowProcessActivity>, IWorkflowProcessActivityLogic
    {
        /// <summary>
        ///     获取活动
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<IEnumerable<WorkflowProcessActivity>> GetWorkflowProcessActivityByProcessId(IdInput input)
        {
            return _repository.GetWorkflowProcessActivityByProcessId(input);
        }

        #region 构造函数

        private readonly IWorkflowProcessActivityRepository _repository;

        public WorkflowProcessActivityLogic(IWorkflowProcessActivityRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        #endregion
    }
}