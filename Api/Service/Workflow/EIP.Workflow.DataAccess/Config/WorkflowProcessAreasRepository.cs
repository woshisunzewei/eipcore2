using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Dapper;
using EIP.Common.DataAccess;
using EIP.Workflow.Models.Engine;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.DataAccess.Config
{
    /// <summary>
    ///     区块
    /// </summary>
    public class WorkflowProcessAreasRepository : DapperAsyncRepository<WorkflowProcessAreas>,
        IWorkflowProcessAreasRepository
    {
        /// <summary>
        /// 获取区域监控数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<IEnumerable<WorkflowProcessAreas>> GetWorkflowEngineTrackAreasOutput(WorkflowEngineRunnerInput input)
        {
            const string sql = "SELECT * FROM Workflow_ProcessAreas WHERE ProcessId=@processId";
            return SqlMapperUtil.SqlWithParams<WorkflowProcessAreas>(sql, new
            {
                processId = input.ProcessId
            });
        }
    }
}