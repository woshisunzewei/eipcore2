using EIP.Common.Core.Extensions;
using EIP.Common.Core.Utils;
using EIP.Common.Quartz.Dtos;
using EIP.Common.Quartz.Enums;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Calendar;
using Quartz.Impl.Matchers;
using Quartz.Spi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EIP.Common.Quartz
{
    /// <summary>
    /// 调度框架管理
    /// </summary>
    public class StdSchedulerManager
    {
        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public static async Task Init()
        {
            NameValueCollection properties = new NameValueCollection
            {
                ["quartz.scheduler.instanceName"] = "EIPScheduler",
                ["quartz.scheduler.instanceId"] = "instance_one",
                ["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz",
                ["quartz.threadPool.threadCount"] = ConfigurationUtil.GetSection("Quartz:ThreadCount"),
                ["quartz.jobStore.misfireThreshold"] = "60000",
                ["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz",
                ["quartz.jobStore.useProperties"] = "false",
                ["quartz.jobStore.dataSource"] = "default",
                ["quartz.jobStore.tablePrefix"] = "QRTZ_",
                ["quartz.jobStore.clustered"] = "true",
                ["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz",
                ["quartz.dataSource.default.connectionString"] = ConfigurationUtil.GetSection("Quartz:ConnectionString"),
                ["quartz.dataSource.default.provider"] = ConfigurationUtil.GetSection("Quartz:DataSource"),
                ["quartz.serializer.type"] = "json"
            };
            SchedulerFactory = new StdSchedulerFactory(properties);
            Scheduler = await SchedulerFactory.GetScheduler();
        }

        /// <summary>
        /// 调度工厂
        /// </summary>
        private static StdSchedulerFactory SchedulerFactory { get; set; }

        /// <summary>
        /// 调度接口
        /// </summary>
        private static IScheduler Scheduler { get; set; }

        public IListenerManager ListenerManager => Scheduler.ListenerManager;

        /// <summary>
        /// 调度器名称
        /// </summary>
        public string SchedulerName => Scheduler.SchedulerName;

        public bool InStandbyMode => Scheduler.InStandbyMode;

        /// <summary>
        /// 初始化配置参数
        /// </summary>
        /// <param name="props"></param>
        public static void Initialize(NameValueCollection props)
        {
            SchedulerFactory.Initialize(props);
        }

        /// <summary>
        /// 调用开启方法
        /// </summary>
        public static void Start()
        {
            try
            {
                Scheduler.Start();
            }
            catch (Exception)
            {
                throw new Exception("确定配置的参数是否有错误");
            }
        }

        #endregion

        #region 添加

        /// <summary>
        /// 添加Job
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static async Task ScheduleJob(ScheduleJobInput input)
        {
            #region JobDetail
            JobBuilder jobBuilder = JobBuilder
                .Create()
                .OfType(Type.GetType(input.NamespaceName + "." + input.ClassName + "," + input.NamespaceName, true))
                .WithDescription(input.JobDescription)
                .WithIdentity(new JobKey(input.JobName, input.JobGroup))
                .UsingJobData(GetJobDataMap(input));

            if (input.IsRequest)
            {
                //在服务器异常时候,重启调度之后,接着执行调度
                jobBuilder = jobBuilder.RequestRecovery();
            }
            if (input.IsSave)
            {
                //保存到数据库中
                jobBuilder.StoreDurably();
            }
            IJobDetail detail = jobBuilder.Build();
            #endregion

            #region trigger
            var triggerBuilder = TriggerBuilder
                .Create()
                .ForJob(detail);

            if (!input.ChoicedCalendar.IsNullOrEmpty())
                triggerBuilder.ModifiedByCalendar(input.ChoicedCalendar);
            if (!input.TriggerName.IsNullOrEmpty() && !input.TriggerGroup.IsNullOrEmpty())
            {
                triggerBuilder.WithDescription(input.TriggerDescription)
                   .WithIdentity(new TriggerKey(input.TriggerName, input.TriggerGroup));
            }
            #endregion

            //是否替换
            if (input.ReplaceExists)
            {
                var triggers = new HashSet<ITrigger>();
                //如果是Cron触发器
                if (input.TriggerType == "CronTriggerImpl")
                {
                    triggers.Add(triggerBuilder.WithCronSchedule(input.Expression).Build());
                }
                else
                {
                    var simpleBuilder = SimpleScheduleBuilder.Create();
                    if (input.Repeat)
                    {
                        simpleBuilder.RepeatForever();
                    }
                    simpleBuilder.WithInterval(input.Interval);
                    triggers.Add(triggerBuilder.WithSchedule(simpleBuilder).Build());
                }
                await ScheduleJob(detail, triggers, true);
            }
            else
            {
                //如果是Cron触发器
                if (input.TriggerType == "CronTriggerImpl")
                {
                    await ScheduleJob(detail, triggerBuilder.WithCronSchedule(input.Expression).Build());
                }
                else
                {
                    var simpleBuilder = SimpleScheduleBuilder.Create();
                    if (input.Repeat)
                    {
                        simpleBuilder.RepeatForever();
                    }
                    simpleBuilder.WithInterval(input.Interval);
                    await ScheduleJob(detail, triggerBuilder.WithSchedule(simpleBuilder).Build());
                }
            }
        }

        /// <summary>
        /// 获取请求参数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static JobDataMap GetJobDataMap(ScheduleJobInput input)
        {
            JobDataMap map = new JobDataMap();
            foreach (var param in input.Parameters)
            {
                map.Add(param.Key, param.Value);
            }
            return map;
        }

        /// <summary>
        /// 触发Job
        /// </summary>
        /// <param name="jobDetail">jobDetail</param>
        /// <param name="trigger">trigger触发器</param>
        /// <returns></returns>
        public static Task<DateTimeOffset> ScheduleJob(IJobDetail jobDetail,
            ITrigger trigger)
        {
            return Scheduler.ScheduleJob(jobDetail, trigger);
        }

        /// <summary>
        /// 添加作业
        /// </summary>
        /// <param name="jobDetail"></param>
        /// <param name="triggers"></param>
        /// <param name="replace"></param>
        public static async Task ScheduleJob(IJobDetail jobDetail,
            IReadOnlyCollection<ITrigger> triggers,
            bool replace = false)
        {
            await Scheduler.ScheduleJob(jobDetail, triggers, replace);
        }

        /// <summary>
        /// 把作业与触发器添加到调度里去
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        public static void TriggerJob(string jobName, string groupName)
        {
            Scheduler.TriggerJob(new JobKey(jobName, groupName));
        }
        #endregion

        #region 开启
        /// <summary>
        /// 开启所有
        /// </summary>
        public static void ResumeAll()
        {
            Scheduler.ResumeAll();
        }
        /// <summary>
        /// 根据组名开启作业
        /// </summary>
        /// <param name="groupName">组名</param>
        public static void ResumeJobGroup(string groupName)
        {
            Scheduler.ResumeJobs(GroupMatcher<JobKey>.GroupEquals(groupName));
        }
        /// <summary>
        /// 根据触发器组名开启作业
        /// </summary>
        /// <param name="groupName">组名</param>
        public static void ResumeTriggerGroup(string groupName)
        {
            Scheduler.ResumeTriggers(GroupMatcher<TriggerKey>.GroupEquals(groupName));
        }

        /// <summary>
        /// 根据名称开启作业
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        public static void ResumeJob(string jobName, string groupName)
        {
            Scheduler.ResumeJob(new JobKey(jobName, groupName));
        }

        /// <summary>
        /// 恢复触发器
        /// </summary>
        /// <param name="triggerName"></param>
        /// <param name="groupName"></param>
        public static void ResumeTrigger(string triggerName, string groupName)
        {
            Scheduler.ResumeTrigger(new TriggerKey(triggerName, groupName));
        }
        #endregion

        #region 暂停
        /// <summary>
        /// 暂停所有
        /// </summary>
        public static async Task PauseAll()
        {
            await Scheduler.PauseAll();
        }

        /// <summary>
        /// 根据组名暂停作业
        /// </summary>
        /// <param name="groupName">组名</param>
        public static async Task PauseJobGroup(string groupName)
        {
            await Scheduler.PauseJobs(GroupMatcher<JobKey>.GroupEquals(groupName));
        }

        /// <summary>
        /// 根据组名暂停触发器
        /// </summary>
        /// <param name="groupName">组名</param>
        public static async Task PauseTriggerGroup(string groupName)
        {
            await Scheduler.PauseTriggers(GroupMatcher<TriggerKey>.GroupEquals(groupName));
        }
        /// <summary>
        /// 通过名称分组暂停作业
        /// </summary>
        /// <param name="jobName">作业名</param>
        /// <param name="groupName">分组名</param>
        public static async Task PauseJob(string jobName, string groupName)
        {
            await Scheduler.PauseJob(new JobKey(jobName, groupName));
        }

        /// <summary>
        /// 暂停触发器
        /// </summary>
        /// <param name="triggerName"></param>
        /// <param name="groupName"></param>
        public static async Task PauseTrigger(string triggerName, string groupName)
        {
            await Scheduler.PauseTrigger(new TriggerKey(triggerName, groupName));
        }

        /// <summary>
        /// 判断某组作业是否被暂停
        /// </summary>
        /// <param name="jobGroupName">组名</param>
        /// <returns></returns>
        public static async Task<bool?> IsJobGroupPaused(string jobGroupName)
        {
            try
            {
                return await Scheduler.IsJobGroupPaused(jobGroupName);
            }
            catch (NotImplementedException)
            {
                return null;
            }
        }

        /// <summary>
        /// 判断某组触发器是否暂停
        /// </summary>
        /// <param name="triggerGroupName"></param>
        /// <returns></returns>
        public static async Task<bool?> IsTriggerGroupPaused(string triggerGroupName)
        {
            try
            {
                return await Scheduler.IsTriggerGroupPaused(triggerGroupName);
            }
            catch (NotImplementedException)
            {
                return null;
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 移除全局作业监听
        /// </summary>
        /// <param name="name">监听名称</param>
        public static void RemoveGlobalJobListener(string name)
        {
            Scheduler.ListenerManager.RemoveJobListener(name);
        }

        /// <summary>
        /// 移除全局触发器监听
        /// </summary>
        /// <param name="name">监听名称</param>
        public static void RemoveGlobalTriggerListener(string name)
        {
            Scheduler.ListenerManager.RemoveTriggerListener(name);
        }

        /// <summary>
        /// 通过名称分组删除作业
        /// </summary>
        /// <param name="jobName">作业名</param>
        /// <param name="groupName">分组名</param>
        /// <returns></returns>
        public static async Task<bool> DeleteJob(string jobName, string groupName)
        {
            return await Scheduler.DeleteJob(new JobKey(jobName, groupName));
        }
        #endregion

        #region 停止
        /// <summary>
        /// 停止调度
        /// </summary>
        public static async Task Shutdown()
        {
            await Scheduler.Shutdown();
        }

        /// <summary>
        /// 取消任务
        /// </summary>
        /// <param name="triggerName"></param>
        /// <param name="groupName"></param>
        public static async Task UnscheduleJob(string triggerName, string groupName)
        {
            await Scheduler.UnscheduleJob(new TriggerKey(triggerName, groupName));
        }
        #endregion

        #region 日历
        /// <summary>
        /// 获取所有日历名称
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> GetCalendarNames()
        {
            return await Scheduler.GetCalendarNames();
        }

        /// <summary>
        /// 根据名称获取日历
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<ICalendar> GetCalendar(string name)
        {
            return await Scheduler.GetCalendar(name);
        }

        /// <summary>
        /// 根据日历关键字删除日历
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<bool> DeleteCalendar(string key)
        {
            return await Scheduler.DeleteCalendar(key);
        }

        /// <summary>
        /// 添加日历
        /// </summary>
        /// <param name="input"></param>
        public static async Task AddCalendar(CalendarInput input)
        {
            var calendar = new BaseCalendar();
            switch (input.EnumCalendar)
            {
                case EnumCalendar.Cron表达式:
                    calendar = new CronCalendar(input.Expression);
                    break;
            }
            calendar.TimeZone = null;
            calendar.Description = input.Description;
            await Scheduler.AddCalendar(input.CalendarName, calendar, input.ReplaceExists, input.UpdateTriggers);
        }
        #endregion

        #region 备用
        /// <summary>
        /// 备用
        /// </summary>
        public static async Task Standby()
        {
            await Scheduler.Standby();
        }
        #endregion

        #region 中断
        /// <summary>
        /// 中断
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        public static async Task Interrupt(string jobName, string groupName)
        {
            await Scheduler.Interrupt(new JobKey(jobName, groupName));
        }
        #endregion

        #region 方法
        /// <summary>
        /// 返回运行作业集合
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable> GetCurrentlyExecutingJobs()
        {
            return await Scheduler.GetCurrentlyExecutingJobs();
        }

        /// <summary>
        /// 获取所有作业键集合
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<IReadOnlyCollection<JobKey>> GetJobKeys(GroupMatcher<JobKey> key)
        {
            return await Scheduler.GetJobKeys(key);
        }

        /// <summary>
        /// 获取某个作业
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<IJobDetail> GetJobDetail(JobKey key)
        {
            return await Scheduler.GetJobDetail(key);
        }

        /// <summary>
        /// 获取调度器数据
        /// </summary>
        /// <returns></returns>
        public static async Task<SchedulerMetaData> GetMetaData()
        {
            return await Scheduler.GetMetaData();
        }

        public static async Task<IEnumerable> GetTriggersOfJob(JobKey jobKey)
        {
            try
            {
                return await Scheduler.GetTriggersOfJob(jobKey);
            }
            catch (NotImplementedException)
            {
                return null;
            }
        }

        public static async Task<IReadOnlyCollection<TriggerKey>> GetTriggerKeys(GroupMatcher<TriggerKey> matcher)
        {
            try
            {
                return await Scheduler.GetTriggerKeys(matcher);
            }
            catch (NotImplementedException)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取触发器组名
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable> GetTriggerGroupNames()
        {
            return await Scheduler.GetTriggerGroupNames();
        }

        /// <summary>
        /// 获取工作名
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable> GetJobGroupNames()
        {
            return await Scheduler.GetJobGroupNames();
        }

        /// <summary>
        /// 获取Triiger信息
        /// </summary>
        /// <param name="triggerKey"></param>
        /// <returns></returns>
        public static async Task<ITrigger> GetTrigger(TriggerKey triggerKey)
        {
            return await Scheduler.GetTrigger(triggerKey);
        }

        /// <summary>
        /// 检查触发器是否存在
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> CheckExists(TriggerKey triggerKey)
        {
            return await Scheduler.CheckExists(triggerKey);
        }

        /// <summary>
        /// 检查任务是否存在
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> CheckExists(JobKey jobKey)
        {
            return await Scheduler.CheckExists(jobKey);
        }

        /// <summary>
        /// 获取触发器状态
        /// </summary>
        /// <param name="triggerKey"></param>
        /// <returns></returns>
        public static async Task<TriggerState> GetTriggerState(TriggerKey triggerKey)
        {
            return await Scheduler.GetTriggerState(triggerKey);
        }

        /// <summary>
        ///     获取任务在未来周期内哪些时间会运行
        /// </summary>
        /// <param name="cronExpressionString">Cron表达式</param>
        /// <param name="numTimes">运行次数</param>
        /// <returns>运行时间段</returns>
        public static List<string> GetTaskeFireTime(string cronExpressionString, int numTimes)
        {
            if (numTimes < 0)
            {
                throw new Exception("参数numTimes值大于等于0");
            }
            //时间表达式
            var trigger = TriggerBuilder.Create().WithCronSchedule(cronExpressionString).Build();
            var dates = TriggerUtils.ComputeFireTimes(trigger as IOperableTrigger, null, numTimes);
            return dates.Select(dtf => TimeZoneInfo.ConvertTimeFromUtc(dtf.DateTime, TimeZoneInfo.Local).ToString(CultureInfo.InvariantCulture)).ToList();
        }
        #endregion

    }
}