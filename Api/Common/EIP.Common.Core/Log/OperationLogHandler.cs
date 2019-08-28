using EIP.Common.Core.Auth;
using EIP.Common.Core.Utils;
using Microsoft.AspNetCore.Http;
using System;

namespace EIP.Common.Core.Log
{
    /// <summary>
    /// 操作处理
    /// </summary>
    public class OperationLogHandler : BaseHandler<OperateLog>
    {
        /// <summary>
        /// 操作日志
        /// </summary>
        public OperationLogHandler(IHttpContextAccessor accessor,
            HttpRequest request,
            PrincipalUser principalUser) : base("SystemOperationLog")
        {
            Log = new OperateLog
            {
                OperationLogId = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                RemoteIp = IpBrowserUtil.GetRemoteIp(accessor),
                RequestContentLength = request.ContentLength,
                RequestType = request.Method,
                RequestData = RequestData(request),
                Url = request.Path.Value,
                CreateUserName = principalUser.Name,
                CreateUserCode = principalUser.Code,
                CreateUserId = principalUser.UserId,
            };
        }

        /// <summary>
        /// 执行时间
        /// </summary>
        public void ActionExecuted()
        {
            Log.ActionExecutionTime = (DateTime.Now - Log.CreateTime).TotalSeconds;
        }
    }
}