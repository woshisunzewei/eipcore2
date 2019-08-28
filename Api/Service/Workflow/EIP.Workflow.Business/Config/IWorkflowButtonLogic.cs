using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.Business.Config
{
    /// <summary>
    ///     工作流处理界面按钮接口定义
    /// </summary>
    public interface IWorkflowButtonLogic : IAsyncLogic<WorkflowButton>
    {
        /// <summary>
        /// 保存按钮信息
        /// </summary>
        /// <param name="button">按钮信息</param>
        /// <returns></returns>
        Task<OperateStatus> SaveButton(WorkflowButton button);
    }
}