using System;
using Microsoft.AspNetCore.Builder;

namespace EIP.Common.Restful.Middlewares
{
    /// <summary>
    /// 中间件扩展
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Authorization权限中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRequestProviderMiddleware(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            return builder.UseMiddleware<RequestProviderMiddleware>();
        }

        /// <summary>
        /// 错误中间件注册
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}