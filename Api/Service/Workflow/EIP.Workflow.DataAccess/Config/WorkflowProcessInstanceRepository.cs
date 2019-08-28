using System.Threading.Tasks;
using EIP.Common.Dapper;
using EIP.Common.DataAccess;
using EIP.Workflow.Models.Engine;
using EIP.Workflow.Models.Engine;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.DataAccess.Config
{
    public class WorkflowProcessInstanceRepository : DapperAsyncRepository<WorkflowProcessInstance>,IWorkflowProcessInstanceRepository
    {
        /// <summary>
        /// 更新流程状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<int> UpdateWorkflowProcessInstanceStatus(WorkflowEngineRunnerInput input)
        {
            const string sql = "UPDATE Workflow_ProcessInstance SET Status=@status,UpdateTime=@updateTime,UpdateUserId";
            return SqlMapperUtil.InsertUpdateOrDeleteSql<WorkflowProcessInstance>(sql, new { });
        }
    }
}