using System;

namespace EIP.Common.Core.Extensions
{
    /// <summary>
    /// Guid扩展类
    /// </summary>
    public static class GuidExtension
    {
        /// <summary>
        /// 是否为空Guid
        /// </summary>
        /// <param name="guid">guid</param>
        /// <returns>是否为空Guid</returns>
        public static bool IsEmptyGuid(this Guid guid)
        {
            return guid == Guid.Empty;
        }

        /// <summary>
        /// 是否为空或者null的Guid
        /// </summary>
        /// <param name="guid">guid值</param>
        /// <returns>是否为空或者null的Guid</returns>
        public static bool IsNullOrEmptyGuid(this Guid? guid)
        {
            return guid == null || guid == Guid.Empty;
        }
    }
}