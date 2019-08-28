using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.Business.Config
{
    /// <summary>
    ///     工作流表单业务逻辑接口
    /// </summary>
    public interface IWorkflowFormLogic : IAsyncLogic<WorkflowForm>
    {
        /// <summary>
        /// 根据表单类型获取所有表单信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<WorkflowForm>> GetFormByFormType(NullableIdInput input);

        /// <summary>
        /// 保存表单基本信息
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        Task<OperateStatus> Save(WorkflowForm process);

        /// <summary>
        /// 保存表单设计器基本信息
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        Task<OperateStatus> SaveWorkflowFromHtml(WorkflowForm form);

        /// <summary>
        /// 保存表单设计器基本信息
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        Task<OperateStatus> SaveWorkflowFromUrl(WorkflowForm form);

        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperateStatus> DeleteForm(IdInput input);
    }
}