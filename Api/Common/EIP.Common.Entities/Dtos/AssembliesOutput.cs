namespace EIP.Common.Entities.Dtos
{
    /// <summary>
    /// 程序集
    /// </summary>
    public class AssembliesOutput
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Clr运行时
        /// </summary>
        public string ClrVersion { get; set; }

        /// <summary>
        /// 位置路径
        /// </summary>
        public string Location { get; set; }
    }
}