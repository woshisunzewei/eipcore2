using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Core.Extensions;
using EIP.Common.Core.Utils;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.System.DataAccess.Config;
using EIP.System.Models.Entities;
using EIP.Workflow.DataAccess.Config;
using EIP.Workflow.Models.Dtos;
using EIP.Workflow.Models.Entities;
using MongoDB.Driver;

namespace EIP.Workflow.Business.Config
{
    /// <summary>
    ///     工作流处理界面按钮接口实现
    /// </summary>
    public class WorkflowProcessLogic : DapperAsyncLogic<WorkflowProcess>, IWorkflowProcessLogic
    {
        #region 构造函数

        private readonly IWorkflowProcessRepository _processRepository;
        private readonly IWorkflowProcessActivityRepository _activityRepository;
        private readonly IWorkflowProcessLineRepository _lineRepository;
        private readonly IWorkflowProcessAreasLogic _areasLogic;
        private readonly ISystemDictionaryMongoDbRepository _dictionaryMongoDbRepository;
        public WorkflowProcessLogic(IWorkflowProcessRepository processRepository,
            IWorkflowProcessActivityRepository activityRepository,
            IWorkflowProcessLineRepository lineRepository,
            IWorkflowProcessAreasLogic areasLogic,
            ISystemDictionaryMongoDbRepository dictionaryMongoDbRepository)
            : base(processRepository)
        {
            _processRepository = processRepository;
            _activityRepository = activityRepository;
            _lineRepository = lineRepository;
            _areasLogic = areasLogic;
            _dictionaryMongoDbRepository = dictionaryMongoDbRepository;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     根据流程类型获取所有流程
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IEnumerable<WorkflowProcessGetOutput>> GetWorkflow(WorkflowProcessGetInput input)
        {
            var workflows = (await _processRepository.GetWorkflow(input)).ToList();
            var list = new List<FilterDefinition<SystemDictionary>>
            {
                Builders<SystemDictionary>.Filter.Lt("CreateTime", DateTime.Now)
            };
            var filter = Builders<SystemDictionary>.Filter.And(list);
            var dic = (await _dictionaryMongoDbRepository.GetAllEnumerableAsync(filter)).ToList();
            foreach (var workflow in workflows)
            {
                var type = dic.FirstOrDefault(f => f.DictionaryId == workflow.Type);
                if (type != null)
                {
                    workflow.TypeName = type.Name;
                }
            }
            return workflows;
        }

        /// <summary>
        ///     保存
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public async Task<OperateStatus> Save(WorkflowProcess process)
        {
            if (process.ProcessId.IsEmptyGuid())
            {
                process.CreateTime = DateTime.Now;
                process.DesignJson = process.DesignJson.Replace("{0}", process.Name);
                process.DesignJson = process.DesignJson.Replace("{1}", CombUtil.NewComb().ToString());
                process.DesignJson = process.DesignJson.Replace("{2}", CombUtil.NewComb().ToString());
                process.ProcessId = CombUtil.NewComb();
                return await InsertAsync(process);
            }
            var workflowProcess = await GetByIdAsync(process.ProcessId);
            process.CreateTime = workflowProcess.CreateTime;
            process.CreateUserId = workflowProcess.CreateUserId;
            process.CreateUserName = workflowProcess.CreateUserName;
            process.UpdateTime = DateTime.Now;
            process.UpdateUserId = process.CreateUserId;
            process.UpdateUserName = process.CreateUserName;
            process.DesignJson = workflowProcess.DesignJson;
            return await UpdateAsync(process);
        }

        /// <summary>
        ///     保存流程设计图
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public async Task<OperateStatus> SaveWorkflowProcessJson(WorkflowSaveProcessInput process)
        {
            var operateStatus = new OperateStatus();
            var workflowProcess = await GetByIdAsync(process.ProcessId);
            try
            {
                workflowProcess.DesignJson = process.DesignJson;
                workflowProcess.UpdateUserId = process.UpdateUserId;
                workflowProcess.UpdateUserName = process.UpdateUserName;
                workflowProcess.UpdateTime = process.UpdateTime;
                await _processRepository.DeleteWorkflowActivityAndLine(new IdInput { Id = process.ProcessId });
                process.Activities = process.Activity == null ? new List<WorkflowProcessActivity>() : process.Activity.JsonStringToList<WorkflowProcessActivity>();
                process.Lines = process.Line == null ? new List<WorkflowProcessLine>() : process.Line.JsonStringToList<WorkflowProcessLine>();
                process.Areases = process.Areas == null ? new List<WorkflowProcessAreas>() : process.Areas.JsonStringToList<WorkflowProcessAreas>();
                foreach (var ac in process.Activities)
                {
                    ac.Buttons = JsonExtension.ListToJsonString(ac.ButtonList);
                    ac.ProcessId = process.ProcessId;
                }
                foreach (var ac in process.Lines)
                {
                    ac.ProcessId = process.ProcessId;
                }
                foreach (var ac in process.Areases)
                {
                    ac.ProcessId = process.ProcessId;
                }
                if (process.Activities.Any())
                    await _activityRepository.BulkInsertAsync(process.Activities);
                if (process.Lines.Any())
                    await _lineRepository.BulkInsertAsync(process.Lines);
                if (process.Areases.Any())
                    await _areasLogic.InsertMultipleDapperAsync(process.Areases);

                operateStatus = await UpdateAsync(workflowProcess);
            }
            catch (Exception exception)
            {
                operateStatus.Message = exception.Message;
            }
            return operateStatus;
        }

        #endregion
    }
}