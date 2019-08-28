using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Dapper;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.DataAccess.Config
{
    /// <summary>
    ///     工作流意见数据访问接口实现
    /// </summary>
    public class WorkflowCommentRepository : DapperAsyncRepository<WorkflowComment>, IWorkflowCommentRepository
    {
        /// <summary>
        /// 根据用户Id查询对应的意见
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<IEnumerable<WorkflowComment>> GetWorkflowCommentByCreateUserId(IdInput input)
        {
            const string sql = "SELECT * FROM Workflow_Comment WHERE CreateUserId=@userId";
            return SqlMapperUtil.SqlWithParams<WorkflowComment>(sql, new {userId = input.Id});
        }
    }
}