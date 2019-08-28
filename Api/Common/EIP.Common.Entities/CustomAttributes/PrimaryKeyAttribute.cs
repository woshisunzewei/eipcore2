using System;

namespace EIP.Common.Entities.CustomAttributes
{
    /// <summary>
    /// 主键
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class PrimaryKeyAttribute : BaseAttribute
    {
        /// <summary>
        /// 是否为自动主键
        /// </summary>
        public bool CheckAutoId { get; set; }
       
        public PrimaryKeyAttribute()
        {
            this.CheckAutoId = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkAutoId">是否为自动主键</param>
        public PrimaryKeyAttribute(bool checkAutoId)
        {
            this.CheckAutoId = checkAutoId;
        }
    }
}
