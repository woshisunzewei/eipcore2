using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EIP.Common.Core.Extensions;
using EIP.Common.Dapper;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.DataAccess.Config
{
    /// <summary>
    ///     工作流表单访问接口实现
    /// </summary>
    public class WorkflowFormRepository : DapperAsyncRepository<WorkflowForm>, IWorkflowFormRepository
    {
        /// <summary>
        /// 根据表单类型获取所有表单信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<IEnumerable<WorkflowForm>> GetFormByFormType(NullableIdInput input)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM Workflow_Form");
            if (!input.Id.IsNullOrEmptyGuid())
            {
                sql.Append(" WHERE FormType=@formType");
            }
            return SqlMapperUtil.SqlWithParams<WorkflowForm>(sql.ToString(), new { formType = input.Id });
        }
    }
}