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

namespace EIP.Workflow.Business.Config
{
    /// <summary>
    ///     工作流表单业务逻辑接口实现
    /// </summary>
    public class WorkflowFormLogic : DapperAsyncLogic<WorkflowForm>, IWorkflowFormLogic
    {
        #region 构造函数

        private readonly IWorkflowProcessLogic _workflowProcessLogic;
        private readonly IWorkflowFormRepository _workflowFormRepository;

        public WorkflowFormLogic(IWorkflowFormRepository workflowFormRepository,
            IWorkflowProcessLogic workflowProcessLogic)
            : base(workflowFormRepository)
        {
            _workflowFormRepository = workflowFormRepository;
            _workflowProcessLogic = workflowProcessLogic;
        }

        #endregion

        #region 方法
        /// <summary>
        /// 根据表单类型获取所有表单信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IEnumerable<WorkflowForm>> GetFormByFormType(NullableIdInput input)
        {
            return await _workflowFormRepository.GetFormByFormType(input);
        }

        /// <summary>
        /// 保存基本信息
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public async Task<OperateStatus> Save(WorkflowForm form)
        {
            if (form.FormId.IsEmptyGuid())
            {
                form.CreateTime = DateTime.Now;
                form.CreateUserId = form.UpdateUserId;
                form.CreateUserName = form.UpdateUserName;
                form.FormId = CombUtil.NewComb();
                return await InsertAsync(form);
            }
            WorkflowForm workflowProcess =await GetByIdAsync(form.FormId);
            form.CreateTime = workflowProcess.CreateTime;
            form.CreateUserId = workflowProcess.CreateUserId;
            form.CreateUserName = workflowProcess.CreateUserName;
            form.Html = workflowProcess.Html;
            form.SubTableJson = workflowProcess.SubTableJson;
            return await UpdateAsync(form);
        }

        /// <summary>
        /// 保存流程设计器基本信息
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public async Task<OperateStatus> SaveWorkflowFromHtml(WorkflowForm form)
        {
            WorkflowForm workflowProcess =await GetByIdAsync(form.FormId);
            workflowProcess.Html = form.Html;
            workflowProcess.UpdateUserId = form.UpdateUserId;
            workflowProcess.UpdateUserName = form.UpdateUserName;
            workflowProcess.UpdateTime = form.UpdateTime;
            workflowProcess.Url = form.Url;
            return await UpdateAsync(workflowProcess);
        }


        /// <summary>
        /// 保存流程地址
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public async Task<OperateStatus> SaveWorkflowFromUrl(WorkflowForm form)
        {
            WorkflowForm workflowProcess =await GetByIdAsync(form.FormId);
            workflowProcess.UpdateUserId = form.UpdateUserId;
            workflowProcess.UpdateUserName = form.UpdateUserName;
            workflowProcess.UpdateTime = form.UpdateTime;
            workflowProcess.Url = form.Url;
            return await UpdateAsync(workflowProcess);
        }

        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OperateStatus> DeleteForm(IdInput input)
        {
            //根据表单查询是否已被使用
            //获取流程设计信息
            var workflowProcess =await _workflowProcessLogic.GetAllEnumerableAsync();
            //序列化对应流程设计的Json字符串
            return new OperateStatus();
        }
        #endregion
    }
}