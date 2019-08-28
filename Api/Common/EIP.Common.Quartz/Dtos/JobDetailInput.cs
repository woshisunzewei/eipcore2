namespace EIP.Common.Quartz.Dtos
{
    /// <summary>
    /// JobDetail输入
    /// </summary>
    public class JobDetailInput
    {
        /// <summary>
        /// 作业名称
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 作业组
        /// </summary>
        public string JobGroup { get; set; }

        /// <summary>
        /// 触发器名称
        /// </summary>
        public string TriggerName { get; set; }

        /// <summary>
        /// 触发器组
        /// </summary>
        public string TriggerGroup { get; set; }
    }
}