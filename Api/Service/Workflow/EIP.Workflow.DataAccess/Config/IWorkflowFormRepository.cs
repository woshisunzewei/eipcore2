using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.DataAccess.Config
{
    /// <summary>
    /// 工作流表单数据访问接口
    /// </summary>
    public interface IWorkflowFormRepository : IAsyncRepository<WorkflowForm>
    {
        /// <summary>
        /// 根据表单类型获取所有表单信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<WorkflowForm>> GetFormByFormType(NullableIdInput input);
    }
}