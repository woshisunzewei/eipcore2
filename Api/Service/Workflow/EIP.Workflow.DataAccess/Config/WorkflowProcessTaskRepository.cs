using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EIP.Common.Dapper;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Paging;
using EIP.Workflow.Models.Engine;
using EIP.Workflow.Models.Entities;
using EIP.Workflow.Models.Enums;

namespace EIP.Workflow.DataAccess.Config
{
    /// <summary>
    ///     流程任务
    /// </summary>
    public class WorkflowProcessTaskRepository : DapperAsyncRepository<WorkflowProcessInstanceTask>,
        IWorkflowProcessTaskRepository
    {
        /// <summary>
        ///     更新任务状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<int> UpdateProcessTaskStatus(WorkflowEngineProcessTaskStatusInput input)
        {
            const string sql =
                "UPDATE Workflow_ProcessInstance_Task SET Status=@status,DoUserId=@doUserId,DoUserName=@doUserName,DoTime=@doTime,Comment=@comment WHERE TaskId=@taskId";
            return SqlMapperUtil.InsertUpdateOrDeleteSql<WorkflowProcessInstanceTask>(sql, new
            {
                status = (byte)input.Status,
                doUserId = input.PrincipalUser.UserId,
                doUserName = input.PrincipalUser.Name,
                doTime = DateTime.Now,
                comment = input.Comment,
                taskId = input.TaskId
            });
        }

        /// <summary>
        /// 获取任务历史轨迹
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<IEnumerable<WorkflowEngineTrackForTableOutput>> GetWorkflowEngineTrackForTable(WorkflowEngineRunnerInput input)
        {
            const string sql = "select CurrentActivityName ActivityName,DoUserName,DoTime,ReceiveTime,Comment from Workflow_ProcessInstance_Task where ProcessInstanceId=@processInstanceId order by ReceiveTime asc ";
            return SqlMapperUtil.SqlWithParams<WorkflowEngineTrackForTableOutput>(sql, new
            {
                processInstanceId = input.ProcessInstanceId
            });
        }

        /// <summary>
        ///     已处理的任务(已处理事项)
        /// </summary>
        /// <returns></returns>
        public Task<PagedResults<WorkflowEngineHaveDoTaskOutput>> GetWorkflowEngineHaveDoTaskOutput(WorkflowEngineRunnerInput input)
        {
            StringBuilder sql = new StringBuilder(string.Format(
               @"  select a.*,b.ReceiveUserName,b.CurrentActivityName, @rowNumber, @recordCount  from  (
								(
									select task.TaskId,process.ProcessId,task.ProcessInstanceId,task.Title,process.Name,task.CurrentActivityName DoCurrentActivityName,instance.Status,instance.Urgency,task.SendTime,task.SendUserName from  [dbo].[Workflow_ProcessInstance_Task] task
									left join [dbo].[Workflow_ProcessInstance] instance on task.ProcessInstanceId=instance.ProcessInstanceId
									left join [dbo].[Workflow_ProcessInstance_Activity] activity on activity.ActivityId=task.CurrentActivityId
									left join [dbo].[Workflow_Process] process on instance.ProcessId=process.ProcessId
									where task.DoUserId='{0}' and activity.[Type]!='start round' and activity.[Type]!='end round'--排除起始节点
									group by task.TaskId,process.ProcessId,task.ProcessInstanceId,task.Title,process.Name,task.CurrentActivityName,instance.Status,instance.Urgency,task.SendTime,task.SendUserName
								) a
								left join (
									select task.ProcessInstanceId,task.ReceiveUserName,task.CurrentActivityName  from Workflow_ProcessInstance_Task task	where task.Status={1}
								) b
								    on a. ProcessInstanceId=b.ProcessInstanceId   ) @where ", input.CurrentUser.UserId,(byte)EnumTask.正在处理));
            return PagingQueryAsync<WorkflowEngineHaveDoTaskOutput>(sql.ToString(), input);
        }
    }
}