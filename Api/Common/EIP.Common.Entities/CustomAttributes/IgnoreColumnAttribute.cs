using System;

namespace EIP.Common.Entities.CustomAttributes
{
    /// <summary>
    /// 忽略字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class IgnoreColumnAttribute : BaseAttribute
    {
    }
}
