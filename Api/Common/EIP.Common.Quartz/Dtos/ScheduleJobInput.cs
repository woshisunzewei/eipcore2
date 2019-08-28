using System;
using System.Collections.Generic;
using EIP.Common.Core.Extensions;
using Newtonsoft.Json;

namespace EIP.Common.Quartz.Dtos
{
    /// <summary>
    /// 创建一个Job
    /// </summary>
    public class ScheduleJobInput : JobTypeInput
    {

        /// <summary>
        /// 修改之前任务名称
        /// </summary>
        public string EditBeforeJobName { get; set; }

        /// <summary>
        /// 修改之前任务组名称
        /// </summary>
        public string EditBeforeJobGroup { get; set; }

        /// <summary>
        /// Job组名称
        /// </summary>
        public string JobGroup { get; set; }

        /// <summary>
        /// Job名称
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// Job描述
        /// </summary>
        public string JobDescription { get; set; }

        /// <summary>
        /// 触发器类型
        /// </summary>
        public string TriggerType { get; set; }

        /// <summary>
        /// 触发器组:必须和Job组名称一样
        /// </summary>
        public string TriggerGroup { get; set; }

        /// <summary>
        /// 触发器组名称
        /// </summary>
        public string TriggerName { get; set; }

        /// <summary>
        /// 触发器组描述
        /// </summary>
        public string TriggerDescription { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// true:服务器异常,调度重启之后再执行作业,false为不执行(默认true)
        /// </summary>
        public bool IsRequest { get; set; }

        /// <summary>
        /// 作业传入参数
        /// </summary>
        public IList<Parameters> Parameters => ParametersJson.IsNullOrEmpty() 
            ? new List<Parameters>() 
            : JsonConvert.DeserializeObject<IList<Parameters>>(ParametersJson);

        /// <summary>
        /// 作业传入参数
        /// </summary>
        public string ParametersJson { get; set; }

        /// <summary>
        /// 把作业信息保存到数据库中,在执行后不删除,false为不保存，注意持久化容易出现名字重复导致异常
        /// </summary>
        public bool IsSave { get; set; }

        /// <summary>
        /// 作业详细说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否重复执行
        /// </summary>
        public bool Repeat { get; set; }

        /// <summary>
        /// 选择好的日历
        /// </summary>
        public string ChoicedCalendar { get; set; }

        /// <summary>
        /// 时间轴
        /// </summary>
        public TimeSpan Interval { get; set; }

        /// <summary>
        /// 根据JobKey/TriggerKey替换现有任务
        /// </summary>
        public bool ReplaceExists { get; set; }
    }

    /// <summary>
    /// 参数
    /// </summary>
    public class Parameters
    {
        public string Key { get; set; }

        public Object Value { get; set; }
    }
}