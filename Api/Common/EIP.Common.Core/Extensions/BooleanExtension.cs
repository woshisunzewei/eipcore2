namespace EIP.Common.Core.Extensions
{
    /// <summary>
    /// Boolean扩展类
    /// </summary>
    public static class BooleanExtension
    {
        /// <summary>
        /// 把布尔值转换为小写字符串
        /// </summary>
        public static string ToLower(this bool value)
        {
            return value.ToString().ToLower();
        }
    }
}