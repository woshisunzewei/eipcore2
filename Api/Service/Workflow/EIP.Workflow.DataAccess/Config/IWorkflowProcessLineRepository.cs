using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.DataAccess.Config
{
    public interface IWorkflowProcessLineRepository : IAsyncRepository<WorkflowProcessLine>
    {
        /// <summary>
        ///     根据实例Id获取对应连线信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<WorkflowProcessLine>> GetWorkflowProcessLineByProcessId(IdInput input);
    }
}