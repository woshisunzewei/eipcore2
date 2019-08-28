namespace EIP.Common.Quartz.Dtos
{
    /// <summary>
    /// 作业类型
    /// </summary>
    public class JobTypeInput
    {
        /// <summary>
        /// 方法名
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 程序集名称
        /// </summary>
        public string NamespaceName{ get; set; }
    }
}