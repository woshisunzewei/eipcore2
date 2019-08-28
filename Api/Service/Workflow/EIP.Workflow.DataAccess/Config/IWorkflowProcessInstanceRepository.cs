using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Workflow.Models.Engine;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.DataAccess.Config
{
    public interface IWorkflowProcessInstanceRepository : IAsyncRepository<WorkflowProcessInstance>
    {
        /// <summary>
        /// 更新流程状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> UpdateWorkflowProcessInstanceStatus(WorkflowEngineRunnerInput input);
    }
}
