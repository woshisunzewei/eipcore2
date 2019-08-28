using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.Workflow.Models.Dtos;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.Business.Config
{
    /// <summary>
    ///     工作流处理界面按钮接口定义
    /// </summary>
    public interface IWorkflowProcessLogic : IAsyncLogic<WorkflowProcess>
    {
        /// <summary>
        /// 根据流程类型获取流程信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<WorkflowProcessGetOutput>> GetWorkflow(WorkflowProcessGetInput input);

        /// <summary>
        /// 保存流程基本信息
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        Task<OperateStatus> Save(WorkflowProcess process);

        /// <summary>
        /// 保存流程设计器基本信息
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        Task<OperateStatus> SaveWorkflowProcessJson(WorkflowSaveProcessInput process);
    }
}