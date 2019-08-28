using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EIP.Common.Quartz;
using EIP.Common.Quartz.Dtos;
using EIP.Common.Restful;
using EIP.Common.Restful.Attribute;
using EIP.Common.Core.Extensions;
using EIP.Common.Core.Resource;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using Quartz;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;

namespace EIP.Controllers
{
    /// <summary>
    /// 作业
    /// </summary>
    public class JobController : BaseController
    {
        /// <summary>
        /// 获取所有作业
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("任务调度-方法-列表-获取所有作业")]
        public async Task<JsonResult> GetAllJobs()
        {
            IList<QuartzOutput> models = new List<QuartzOutput>();
            var jobGroups = await StdSchedulerManager.GetJobGroupNames();
            foreach (string group in jobGroups)
            {
                var groupMatcher = GroupMatcher<JobKey>.GroupContains(group);
                var jobKeys = await StdSchedulerManager.GetJobKeys(groupMatcher);
                foreach (var jobKey in jobKeys)
                {
                    var detail = await StdSchedulerManager.GetJobDetail(jobKey);
                    var triggers = await StdSchedulerManager.GetTriggersOfJob(jobKey);

                    foreach (ITrigger trigger in triggers)
                    {
                        var model = new QuartzOutput
                        {
                            JobGroup = group,
                            JobName = jobKey.Name,
                            JobDescription = detail.Description,
                            TriggerState = "Complete",
                            NamespaceName = detail.JobType.Namespace,
                            ClassName = detail.JobType.FullName
                        };
                        model.TriggerName = trigger.Key.Name;
                        model.TriggerGroup = trigger.Key.Group;
                        model.TriggerType = trigger.GetType().Name;
                        model.TriggerState =(await StdSchedulerManager.GetTriggerState(trigger.Key)).ToString();
                        var nextFireTime = trigger.GetNextFireTimeUtc();
                        if (nextFireTime.HasValue)
                        {
                            model.NextFireTime = TimeZone.CurrentTimeZone.ToLocalTime(nextFireTime.Value.DateTime);
                        }

                        var previousFireTime = trigger.GetPreviousFireTimeUtc();
                        if (previousFireTime.HasValue)
                        {
                            model.PreviousFireTime =
                                TimeZone.CurrentTimeZone.ToLocalTime(previousFireTime.Value.DateTime);
                        }
                        models.Add(model);
                    }
                }
            }
            return JsonForGridLoadOnce(models.OrderBy(o => o.NextFireTime));
        }

        /// <summary>
        ///     编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("任务调度-视图-编辑")]
        public async Task<JsonResult> JobEdit(JobDetailInput input)
        {
            var output = new QuartzOutput();
            if (!input.JobName.IsNullOrEmpty() && !input.JobGroup.IsNullOrEmpty())
            {
                var key = new JobKey(input.JobName, input.JobGroup);
                //作业详情
                var detal = await StdSchedulerManager.GetJobDetail(key);
                output.NamespaceName = detal.JobType.Namespace;
                output.ClassName = detal.JobType.Name;
                //触发器
                var triggerKey = new TriggerKey(input.TriggerName, input.TriggerGroup);
                var trigger = await StdSchedulerManager.GetTrigger(triggerKey);

                output.JobType = detal.JobType.FullName;
                output.JobGroup = detal.Key.Group;
                output.JobName = detal.Key.Name;
                output.JobDescription = detal.Description;

                output.TriggerType = trigger.GetType().Name;
                output.TriggerName = trigger.Key.Name;
                output.TriggerGroup = trigger.Key.Group;
                output.TriggerDescription = trigger.Description;

                //获取trigger类型
                switch (trigger.GetType().Name)
                {
                    case "SimpleTriggerImpl":
                        var simpleTriggerImpl = (SimpleTriggerImpl)trigger;
                        output.Interval = simpleTriggerImpl.RepeatInterval;
                        break;
                    case "CronTriggerImpl":
                        //获取表达式
                        var cronTriggerImpl = (CronTriggerImpl)trigger;
                        output.Expression = cronTriggerImpl.CronExpressionString;
                        break;
                }
                output.ReplaceExists = true;
            }
            return Json(output);
        }

        /// <summary>
        ///     通过名称分组删除作业
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("任务调度-方法-列表-通过名称分组删除作业")]
        public async Task<JsonResult> DeleteJob(JobDetailInput input)
        {
            var status = new OperateStatus();
            if (await StdSchedulerManager.DeleteJob(input.JobName, input.JobGroup))
            {
                status.ResultSign = ResultSign.Successful;
                status.Message = Chs.Successful;
            }
            return Json(status);
        }

        /// <summary>
        ///     通过作业名称及组名称获取作业参数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("任务调度-方法-新增/编辑-通过名称分组删除作业")]
        public async Task<JsonResult> GetDetailJobDataMap(JobDetailInput input)
        {
            if (!input.JobGroup.IsNullOrEmpty() && !input.JobName.IsNullOrEmpty())
            {
                var detail = await StdSchedulerManager.GetJobDetail(new JobKey(input.JobName, input.JobGroup));
                var maps = detail.JobDataMap;
                return Json(maps.Select(map => new Parameters { Key = map.Key, Value = map.Value }).ToList());
            }
            return null;
        }

        /// <summary>
        ///     通过名称分组暂停作业
        /// </summary>
        /// <param name="input"></param>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("任务调度-方法-列表-通过名称分组暂停作业")]
        public async Task<JsonResult> PauseJob(JobDetailInput input)
        {
            var status = new OperateStatus();
            try
            {
                await StdSchedulerManager.PauseJob(input.JobName, input.JobGroup);
                status.ResultSign = ResultSign.Successful;
                status.Message = "暂停作业成功";
            }
            catch (Exception ex)
            {
                status.Message = ex.Message;
            }
            return Json(status);
        }

        /// <summary>
        ///     通过名称分组暂停所有作业
        /// </summary>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("任务调度-方法-列表-暂停所有作业")]
        public async Task<JsonResult> PauseAll()
        {
            var status = new OperateStatus();
            try
            {
                await StdSchedulerManager.PauseAll();
                status.ResultSign = ResultSign.Successful;
                status.Message = "暂停所有作业成功";
            }
            catch (Exception ex)
            {
                status.Message = ex.Message;
            }
            return Json(status);
        }

        /// <summary>
        ///     通过名称分组恢复作业
        /// </summary>
        /// <param name="input"></param>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("任务调度-方法-列表-通过名称分组启动作业")]
        public JsonResult ResumeJob(JobDetailInput input)
        {
            var status = new OperateStatus();
            try
            {
                StdSchedulerManager.ResumeJob(input.JobName, input.JobGroup);
                status.ResultSign = ResultSign.Successful;
                status.Message = "恢复作业成功";
            }
            catch (Exception ex)
            {
                status.Message = ex.Message;
            }
            return Json(status);
        }

        /// <summary>
        ///    启动所有作业
        /// </summary>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("任务调度-方法-列表-启动所有作业")]
        public JsonResult ResumeAll()
        {
            var status = new OperateStatus();
            try
            {
                StdSchedulerManager.ResumeAll();
                status.ResultSign = ResultSign.Successful;
                status.Message = "恢复作业成功";
            }
            catch (Exception ex)
            {
                status.Message = ex.Message;
            }
            return Json(status);
        }

        /// <summary>
        ///     保存调度作业
        /// </summary>
        /// <param name="input">调度作业实体</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("任务调度-方法-列表-保存调度作业")]
        public async Task<JsonResult> ScheduleJob(ScheduleJobInput input)
        {
            var status = new OperateStatus();
            try
            {
                if (!input.ReplaceExists)
                {
                    //if (StdSchedulerManager.CheckExists(new TriggerKey(input.TriggerName, input.TriggerGroup)))
                    //{
                    //    status.Message = "指定的触发器已经存在，请重新指定名称";
                    //    return Json(status);
                    //}
                    if (await StdSchedulerManager.CheckExists(new JobKey(input.JobName, input.JobGroup)))
                    {
                        status.Message = "指定的作业名已经存在，请重新指定名称";
                        return Json(status);
                    }
                }
                input.IsSave = true;
                await StdSchedulerManager.ScheduleJob(input);
                status.ResultSign = ResultSign.Successful;
                status.Message = "保存调度作业成功";
            }
            catch (Exception ex)
            {
                status.Message = ex.Message;
            }
            return Json(status);
        }

        /// <summary>
        ///     获得Corn表达式
        /// </summary>
        /// <param name="cronExpression"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("任务调度-方法-Cron-获得Corn表达式")]
        public JsonResult CalcRunTime(string cronExpression)
        {
            try
            {
                return Json(StdSchedulerManager.GetTaskeFireTime(cronExpression, 5));
            }
            catch
            {
                return null;
            }
        }

        #region 日历

        /// <summary>
        ///     日历编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("任务调度-视图-日历编辑")]
        public async Task<JsonResult> CalendarEdit(string calendarName)
        {
            var model = new CalendarInput();
            if (!calendarName.IsNullOrEmpty())
            {
                model.ReplaceExists = true;
                var calendar =await StdSchedulerManager.GetCalendar(calendarName);
                //model.Expression = ((CronCalendar)calendar).CronExpression.ToString();
                model.Description = calendar.Description;
            }
            return Json(model);
        }

        /// <summary>
        ///     获取所有日历
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("任务调度-方法-日历-获得所有日历")]
        public async Task<JsonResult> GetCalendar()
        {
            var getCalendarNames = (await StdSchedulerManager.GetCalendarNames()).ToList();
            var calendars = getCalendarNames.Select(n => new
            {
                Name = n,
                Calendar = StdSchedulerManager.GetCalendar(n)
            }).ToDictionary(n => n.Name, n => n.Calendar);
            IList<CalendarInput> calendarModels = calendars.Select(cal => new CalendarInput
            {
                Description = cal.Value.Result.Description,
                CalendarName = cal.Key
            }).ToList();
            return JsonForGridLoadOnce(calendarModels);
        }

        /// <summary>
        ///     删除日历
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("任务调度-方法-日历-根据日历名称删除日历")]
        public async Task<JsonResult> DeleteCalendar(IdInput<string> input)
        {
            var status = new OperateStatus();
            try
            {
                foreach (var id in input.Id.Split(','))
                {
                    if (await StdSchedulerManager.DeleteCalendar(id))
                    {
                        status.ResultSign = ResultSign.Successful;
                        status.Message = "删除日历成功";
                    }
                }
            }
            catch (Exception ex)
            {
                status.Message = ex.Message;
            }
            return Json(status);
        }

        /// <summary>
        ///     保存日历
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("任务调度-方法-日历-保存日历")]
        public async Task<JsonResult> SaveCalendar(CalendarInput input)
        {
            var status = new OperateStatus();
            try
            {
                if (!input.ReplaceExists && await StdSchedulerManager.GetCalendar(input.CalendarName) != null)
                {
                    status.Message = "日历已存在，请换个其它名称或选择替换现有日历";
                    return Json(status);
                }
                await StdSchedulerManager.AddCalendar(input);
                status.ResultSign = ResultSign.Successful;
                status.Message = "保存日历成功";
            }
            catch (Exception ex)
            {
                status.Message = ex.Message;
            }
            return Json(status);
        }
        #endregion
    }
}