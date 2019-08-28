using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Dapper;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.DataAccess.Config
{
    public class WorkflowProcessLineRepository : DapperAsyncRepository<WorkflowProcessLine>,
        IWorkflowProcessLineRepository
    {
        /// <summary>
        /// 根据实例Id获取流程连线信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<IEnumerable<WorkflowProcessLine>> GetWorkflowProcessLineByProcessId(IdInput input)
        {
            const string sql = "SELECT * FROM Workflow_ProcessLine WHERE ProcessId=@processId";
            return SqlMapperUtil.SqlWithParams<WorkflowProcessLine>(sql, new
            {
                processId=input.Id
            });
        }
    }
}