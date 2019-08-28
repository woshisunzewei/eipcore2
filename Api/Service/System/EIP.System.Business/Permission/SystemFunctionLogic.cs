using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Core.Extensions;
using EIP.Common.Core.Resource;
using EIP.Common.Core.Utils;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.System.DataAccess.Permission;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Permission
{
    /// <summary>
    /// 功能项业务逻辑
    /// </summary>
    public class SystemFunctionLogic : DapperAsyncLogic<SystemFunction>, ISystemFunctionLogic
    {
        #region 构造函数

        private readonly ISystemFunctionRepository _functionRepository;
        private readonly ISystemMenuButtonFunctionRepository _menuButtonFunctionRepository;
        public SystemFunctionLogic(ISystemFunctionRepository functionRepository, ISystemMenuButtonFunctionRepository menuButtonFunctionRepository)
            : base(functionRepository)
        {
            _menuButtonFunctionRepository = menuButtonFunctionRepository;
            _functionRepository = functionRepository;
        }

        #endregion

        /// <summary>
        /// 保存功能项信息
        /// </summary>
        /// <param name="rotes"></param>
        /// <returns></returns>
        public async Task<OperateStatus> SaveFunction(IList<MvcRote> rotes)
        {
            OperateStatus operateStatus = new OperateStatus();
            IList<SystemFunction> insertFunctions = new List<SystemFunction>();
            IList<SystemFunction> deleteFunctions = new List<SystemFunction>();
            IList<SystemFunction> updateFunctions = new List<SystemFunction>();
            if (rotes.Any())
            {
                try
                {

                    //获取该系统的功能项信息
                    IList<SystemFunction> functions = (await _functionRepository.GetSystemFunctionsByAppCode(new IdInput<string>(rotes[0].AppCode))).ToList();
                    //新增功能项信息
                    foreach (var rote in rotes)
                    {
                        //判断该功能项是否已存在
                        SystemFunction function = functions.Where(w =>
                                    w.IsPage == rote.IsPage && w.Area == rote.Area && w.Controller == rote.Controller &&
                                    w.Action == rote.Action && w.AppCode == rote.AppCode).FirstOrDefault();

                        //若存在则不进行操作
                        if (function == null)
                        {
                            SystemFunction insertFunction = rote.MapTo<SystemFunction>();
                            insertFunction.FunctionId = CombUtil.NewComb();
                            insertFunctions.Add(insertFunction);
                        }
                        else
                        {
                            //如果描述不一样则进行修改
                            if (function.Description != rote.Description ||
                                function.ByDeveloperCode != rote.ByDeveloperCode ||
                                function.ByDeveloperTime != rote.ByDeveloperTime)
                            {
                                function.ByDeveloperCode = rote.ByDeveloperCode;
                                function.ByDeveloperTime = rote.ByDeveloperTime;
                                function.Description = rote.Description;
                                updateFunctions.Add(function);
                            }
                        }
                    }
                    //需要删除的功能项信息
                    foreach (var function in functions)
                    {
                        //判断该功能项是否已存在
                        MvcRote rote = rotes.Where(w =>
                                    w.IsPage == function.IsPage && w.Area == function.Area && w.Controller == function.Controller &&
                                    w.Action == function.Action && w.AppCode == function.AppCode).FirstOrDefault();
                        //若存在则不进行操作
                        if (rote == null)
                        {
                            deleteFunctions.Add(function);
                        }
                    }
                    //删除
                    if (deleteFunctions.Any())
                    {
                        //需要进行删除的字符串
                        foreach (var delete in deleteFunctions)
                        {
                            //删除关联项信息
                            await _menuButtonFunctionRepository.DeleteMenuButtonFunction(new SystemMenuButtonFunction()
                            {
                                FunctionId = delete.FunctionId
                            });
                            //删除功能项信息
                            if ((await DeleteAsync(delete)).ResultSign == ResultSign.Successful)
                            {
                                operateStatus.ResultSign = ResultSign.Successful;
                            }
                        }
                    }
                    //更新
                    if (updateFunctions.Any())
                    {
                        foreach (var update in updateFunctions)
                        {
                            if ((await UpdateAsync(update)).ResultSign == ResultSign.Successful)
                            {
                                operateStatus.ResultSign = ResultSign.Successful;
                            }
                        }
                    }

                    //新增
                    if (insertFunctions.Any() && await _functionRepository.BulkInsertAsync(insertFunctions) > 0)
                    {
                        operateStatus.ResultSign = ResultSign.Successful;
                    }

                    if (operateStatus.ResultSign == ResultSign.Successful)
                    {
                        operateStatus.Message = Chs.Successful;
                    }
                }
                catch (Exception ex)
                {
                    operateStatus.ResultSign = ResultSign.Error;
                    operateStatus.Message =  ex.Message;
                }

            }
            else
            {
                operateStatus.ResultSign = ResultSign.Successful;
                operateStatus.Message = Chs.Successful;
            }
            return operateStatus;
        }

        /// <summary>
        /// 根据代码获取该系统的功能项信息
        /// </summary>
        /// <param name="input">代码值</param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemFunction>> GetFunctionsByAppCode(IdInput<string> input = null)
        {
            return (await _functionRepository.GetSystemFunctionsByAppCode(input)).ToList();
        }
    }
}