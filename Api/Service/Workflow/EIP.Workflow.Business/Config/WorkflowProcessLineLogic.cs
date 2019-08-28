using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities.Dtos;
using EIP.Workflow.DataAccess.Config;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.Business.Config
{
    public class WorkflowProcessLineLogic : DapperAsyncLogic<WorkflowProcessLine>, IWorkflowProcessLineLogic
    {
        /// <summary>
        ///     根据实例Id获取对应连线信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<IEnumerable<WorkflowProcessLine>> GetWorkflowProcessLineByProcessId(IdInput input)
        {
            return _workflowProcessLineRepository.GetWorkflowProcessLineByProcessId(input);
        }

        #region 构造函数

        private readonly IWorkflowProcessLineRepository _workflowProcessLineRepository;

        public WorkflowProcessLineLogic(IWorkflowProcessLineRepository workflowProcessLineRepository)
            : base(workflowProcessLineRepository)
        {
            _workflowProcessLineRepository = workflowProcessLineRepository;
        }

        #endregion
    }
}