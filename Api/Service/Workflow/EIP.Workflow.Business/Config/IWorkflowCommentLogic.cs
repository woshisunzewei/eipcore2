using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.Business.Config
{
    /// <summary>
    ///     工作流意见数据访问接口
    /// </summary>
    public interface IWorkflowCommentLogic : IAsyncLogic<WorkflowComment>
    {
        /// <summary>
        /// 保存工作流意见信息
        /// </summary>
        /// <param name="comment">工作流意见信息</param>
        /// <returns></returns>
        Task<OperateStatus> SaveComment(WorkflowComment comment);

        /// <summary>
        /// 删除工作流意见信息
        /// </summary>
        /// <param name="input">工作流意见信息</param>
        /// <returns></returns>
        Task<OperateStatus> DeleteComment(IdInput input);

        /// <summary>
        /// 根据用户Id查询对应的意见
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<WorkflowComment>> GetWorkflowCommentByCreateUserId(IdInput input);
    }
}