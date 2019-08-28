using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Dapper;
using EIP.Common.DataAccess;
using EIP.Workflow.Models.Engine;
using EIP.Workflow.Models.Engine;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.DataAccess.Config
{
    public class WorkflowProcessInstanceActivityRepository : DapperAsyncRepository<WorkflowProcessInstanceActivity>, IWorkflowProcessInstanceActivityRepository
    {
        /// <summary>
        ///     获取下一步流程信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<IEnumerable<WorkflowEngineNextActivitysDoubleWay>> GetWorkflowEngineNextActivitysDoubleWay(
            WorkflowEngineRunnerInput input)
        {
            const string sql =
                @"SELECT activity.ActivityId,activity.Name,activity.ProcessorType,activity.ProcessorHandler,activity.Type FROM Workflow_ProcessInstance_Activity activity WHERE 
                         activity.ActivityId IN (SELECT line.[To] FROM Workflow_ProcessInstance_Line line WHERE line.[From]=@from) AND activity.ProcessInstanceId=@processInstanceId";
            return SqlMapperUtil.SqlWithParams<WorkflowEngineNextActivitysDoubleWay>(sql,
                new { from = input.CurrentActivityId, processInstanceId = input.ProcessInstanceId });
        }

        /// <summary>
        ///     更新活动状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<int> UpdateActivityMarked(WorkflowEngineActivityOrLineMarkedInput input)
        {
            const string sql =
                "UPDATE Workflow_ProcessInstance_Activity SET Marked='True' WHERE ActivityId=@activity AND ProcessInstanceId=@processInstanceId";
            return SqlMapperUtil.InsertUpdateOrDeleteSql<WorkflowEngineActivityOrLineMarkedInput>(sql, new
            {
                activity = input.Activity,
                processInstanceId = input.ProcessInstanceId
            });
        }

        /// <summary>
        /// 获取活动监控数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<IEnumerable<WorkflowEngineTrackActivityOutput>> GetWorkflowEngineTrackActivityOutput(WorkflowEngineRunnerInput input)
        {
            const string sql = @"SELECT ActivityId,Name,[Left],[Top],[Type],Width,Height,Marked FROM [dbo].[Workflow_ProcessInstance_Activity] WHERE ProcessInstanceId=@processInstanceId";
            return SqlMapperUtil.SqlWithParams<WorkflowEngineTrackActivityOutput>(sql, new
            {
                processInstanceId = input.ProcessInstanceId
            });
        }

        /// <summary>
        ///     获取处理任务信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<WorkflowEngineDealWithTaskOutput> GetWorkflowEngineDealWithTaskOutput(WorkflowEngineRunnerInput input)
        {
            const string sql = @"select ActivityId,form.Url FormUrl,activity.Buttons,activity.Name,instance.Title ProcessName,instance.Urgency from Workflow_ProcessInstance_Activity activity
                                 left join Workflow_Form form on activity.FormId=form.FormId
								 left join Workflow_ProcessInstance_Task task on task.CurrentActivityId=activity.ActivityId
                                 left join Workflow_ProcessInstance instance on task.ProcessInstanceId=instance.ProcessInstanceId
                                 where activity.ProcessInstanceId=@processInstanceId and task.TaskId=@taskId";
            return SqlMapperUtil.SqlWithParamsSingle<WorkflowEngineDealWithTaskOutput>(sql, new
            {
                processInstanceId = input.ProcessInstanceId,
                taskId = input.CurrentTaskId
            });
        }

        /// <summary>
        ///     获取处理任务信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<WorkflowProcessInstanceActivity> GetProcessInstanceActivityByActivityIdAndProcessInstanceId(WorkflowEngineRunnerInput input)
        {
            const string sql = @"SELECT * FROM Workflow_ProcessInstance_Activity activity
                                 where activity.ProcessInstanceId=@processInstanceId and task.ActivityId=@activity AND task.ProcessId=@processId";
            return SqlMapperUtil.SqlWithParamsSingle<WorkflowProcessInstanceActivity>(sql, new
            {
                processInstanceId = input.ProcessInstanceId,
                activity = input.CurrentActivityId,
                processId = input.ProcessId
            });
        }
    }
}