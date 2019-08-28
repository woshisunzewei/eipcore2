using System;

namespace EIP.Common.Entities.CustomAttributes
{
    /// <summary>
    /// 列字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ColumnAttribute : BaseAttribute
    {
        /// <summary>
        /// 自增长
        /// </summary>
        public bool AutoIncrement { get; set; }
        public ColumnAttribute()
        {
            AutoIncrement = false;
        }
        /// <summary>
        /// 是否是自增长
        /// </summary>
        /// <param name="autoIncrement"></param>
        public ColumnAttribute(bool autoIncrement)
        {
            AutoIncrement = autoIncrement;
        }
    }
}
