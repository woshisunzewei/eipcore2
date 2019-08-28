using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities.Dtos;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.Business.Config
{
    public interface IWorkflowProcessActivityLogic : IAsyncLogic<WorkflowProcessActivity>
    {
        /// <summary>
        ///     根据实例Id获取对应活动信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<WorkflowProcessActivity>> GetWorkflowProcessActivityByProcessId(IdInput input);
    }
}