using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Core.Extensions;
using EIP.Common.Core.Utils;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.Workflow.DataAccess.Config;
using EIP.Workflow.Models.Entities;
using EIP.Workflow.Models.Enums;
using EIP.Workflow.Models.Resx;

namespace EIP.Workflow.Business.Config
{
    /// <summary>
    ///     工作流意见数据访问接口实现
    /// </summary>
    public class WorkflowCommentLogic : DapperAsyncLogic<WorkflowComment>, IWorkflowCommentLogic
    {
        #region 构造函数

        private readonly IWorkflowCommentRepository _workflowCommentRepository;

        public WorkflowCommentLogic(IWorkflowCommentRepository workflowCommentRepository)
            : base(workflowCommentRepository)
        {
            _workflowCommentRepository = workflowCommentRepository;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     保存工作流意见信息
        /// </summary>
        /// <param name="comment">工作流意见信息</param>
        /// <returns></returns>
        public async Task<OperateStatus> SaveComment(WorkflowComment comment)
        {
            comment.Type = (byte) EnumCommentType.人员;
            comment.UpdateTime = DateTime.Now;
            comment.UpdateUserId = comment.CreateUserId;
            comment.UpdateUserName = comment.CreateUserName;
            if (!comment.CommentId.IsEmptyGuid())
            {
                var flowComment = await GetByIdAsync(comment.CommentId);
                comment.CreateTime = flowComment.CreateTime;
                comment.CreateUserId = flowComment.CreateUserId;
                comment.CreateUserName = flowComment.CreateUserName;
                comment.Type = flowComment.Type;
                return await UpdateAsync(comment);
            }
            comment.CreateTime = DateTime.Now;
            comment.CommentId = CombUtil.NewComb();
            return await InsertAsync(comment);
        }

        /// <summary>
        ///     删除意见
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OperateStatus> DeleteComment(IdInput input)
        {
            var status = new OperateStatus();
            //判断该意见是否为系统默认
            var comment = await GetByIdAsync(input.Id);
            if (comment.Type == (byte) EnumCommentType.系统)
            {
                status.Message = ResourceWorkflow.系统默认意见;
                return status;
            }
            return await DeleteAsync(comment);
        }

        /// <summary>
        /// 根据用户Id查询对应的意见
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IEnumerable<WorkflowComment>> GetWorkflowCommentByCreateUserId(IdInput input)
        {
            return await _workflowCommentRepository.GetWorkflowCommentByCreateUserId(input);
        }
        #endregion
    }
}