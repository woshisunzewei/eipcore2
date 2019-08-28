using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Core.Extensions;
using EIP.Common.Core.Utils;
using EIP.Common.Entities;
using EIP.Workflow.DataAccess.Config;
using EIP.Workflow.Models.Entities;

namespace EIP.Workflow.Business.Config
{
    /// <summary>
    ///     工作流处理界面按钮接口实现
    /// </summary>
    public class WorkflowButtonLogic : DapperAsyncLogic<WorkflowButton>, IWorkflowButtonLogic
    {
        #region 构造函数

        private readonly IWorkflowButtonRepository _workflowButtonRepository;

        public WorkflowButtonLogic(IWorkflowButtonRepository workflowButtonRepository)
            : base(workflowButtonRepository)
        {
            _workflowButtonRepository = workflowButtonRepository;
        }

        #endregion

        #region 方法
        /// <summary>
        /// 保存按钮信息
        /// </summary>
        /// <param name="button">按钮信息</param>
        /// <returns></returns>
        public async Task<OperateStatus> SaveButton(WorkflowButton button)
        {
            if (!button.ButtonId.IsEmptyGuid())
                return await UpdateAsync(button);
            button.ButtonId = CombUtil.NewComb();
            return await InsertAsync(button);
        }
        #endregion
    }
}