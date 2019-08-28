using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Dapper;
using EIP.Common.DataAccess;
using EIP.Workflow.Models.Engine;
using EIP.Workflow.Models.Engine;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.DataAccess.Config
{
    public class WorkflowProcessInstanceLineRepository : DapperAsyncRepository<WorkflowProcessInstanceLine>,
        IWorkflowProcessInstanceLineRepository
    {
        /// <summary>
        ///     更新连线状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<int> UpdateLineMarked(WorkflowEngineActivityOrLineMarkedInput input)
        {
            const string sql =
                "UPDATE Workflow_ProcessInstance_Line SET Marked='True' WHERE [From]=@from AND [To]=@to AND ProcessInstanceId=@processInstanceId";
            return SqlMapperUtil.InsertUpdateOrDeleteSql<WorkflowProcessInstanceLine>(sql, new
            {
                from = input.Activity,
                to = input.ToActivity,
                processInstanceId = input.ProcessInstanceId
            });
        }

        public Task<IEnumerable<WorkflowEngineTrackLineOutput>> GetWorkflowEngineTrackLineOutput(
            WorkflowEngineRunnerInput input)
        {
            const string sql = @"SELECT LineId,[Type],[From],[To],Name,M,Marked FROM [dbo].[Workflow_ProcessInstance_Line] WHERE ProcessInstanceId=@processInstanceId";
            return SqlMapperUtil.SqlWithParams<WorkflowEngineTrackLineOutput>(sql, new
            {
                processInstanceId = input.ProcessInstanceId
            });
        }
    }
}