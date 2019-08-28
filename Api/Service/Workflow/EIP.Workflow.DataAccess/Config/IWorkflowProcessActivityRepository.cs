using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.DataAccess.Config
{
    public interface IWorkflowProcessActivityRepository : IAsyncRepository<WorkflowProcessActivity>
    {
        /// <summary>
        ///     根据实例Id获取对应活动信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<WorkflowProcessActivity>> GetWorkflowProcessActivityByProcessId(IdInput input);
    }
}
