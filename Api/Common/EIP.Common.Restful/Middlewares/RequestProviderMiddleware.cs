using EIP.Common.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace EIP.Common.Restful.Middlewares
{
    /// <summary>
    /// 请求中间件验证:必须设置为Post请求,都要携带token
    /// </summary>
    public class RequestProviderMiddleware
    {
        public readonly RequestDelegate Next;
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="next"></param>
        public RequestProviderMiddleware(RequestDelegate next)
        {
            Next = next;
        }
        /// <summary>
        /// 引入
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method.Equals("OPTIONS"))
            {
                await Next(context);
            }
            else
            {
                //是否请求的获取Token接口
                //if (!context.Request.Path.Equals("/api/Account/Login", StringComparison.Ordinal))
                //{
                //    var tokenHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                //    if (tokenHeader == null)
                //    {
                //        await ReturnBadRequest(context, "请设置头Authorization");
                //    }
                //    else
                //    {
                //        await Next(context);
                //    }
                //}
                //else
                //{
                    await Next(context);
                //}
            }
        }

        /// <summary>
        /// 请求时错误
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task ReturnBadRequest(HttpContext context, string message)
        {
            //返回代码401
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(
                new OperateStatus
                {
                    Message = message
                }));
        }
    }
}