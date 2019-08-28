using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.DataAccess.Config
{
    /// <summary>
    /// 工作流意见数据访问接口
    /// </summary>
    public interface IWorkflowCommentRepository : IAsyncRepository<WorkflowComment>
    {
        /// <summary>
        /// 根据用户Id查询对应的意见
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<WorkflowComment>> GetWorkflowCommentByCreateUserId(IdInput input);
    }
}