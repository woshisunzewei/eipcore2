using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Dapper;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.DataAccess.Config
{
    public class WorkflowProcessActivityRepository : DapperAsyncRepository<WorkflowProcessActivity>,
        IWorkflowProcessActivityRepository
    {
        /// <summary>
        ///     获取活动信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<IEnumerable<WorkflowProcessActivity>> GetWorkflowProcessActivityByProcessId(IdInput input)
        {
            const string sql = "SELECT * FROM Workflow_ProcessActivity WHERE ProcessId=@processId";
            return SqlMapperUtil.SqlWithParams<WorkflowProcessActivity>(sql, new
            {
                processId = input.Id
            });
        }
    }
}