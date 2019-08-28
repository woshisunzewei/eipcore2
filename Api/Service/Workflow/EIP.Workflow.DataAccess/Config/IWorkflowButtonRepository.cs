using EIP.Common.DataAccess;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.DataAccess.Config
{
    /// <summary>
    /// 工作流处理界面按钮接口定义
    /// </summary>
    public interface IWorkflowButtonRepository : IAsyncRepository<WorkflowButton>
    {
       
    }
}