using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.Workflow.Models.Dtos;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.DataAccess.Config
{
    /// <summary>
    /// 工作流处理界面按钮接口定义
    /// </summary>
    public interface IWorkflowProcessRepository : IAsyncRepository<WorkflowProcess>
    {
        /// <summary>
        /// 根据流程类型获取所有流程
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<WorkflowProcessGetOutput>> GetWorkflow(WorkflowProcessGetInput input);

        /// <summary>
        /// 删除活动及连线
        /// </summary>
        /// <param name="input"></param>
        Task<int> DeleteWorkflowActivityAndLine(IdInput input);
    }
}