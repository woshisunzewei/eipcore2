using EIP.Common.Core.Auth;
using EIP.Common.Core.Utils;
using Microsoft.AspNetCore.Http;
using System;
namespace EIP.Common.Core.Log
{
    /// <summary>
    /// 登录日志
    /// </summary>
    public class LoginLogHandler : BaseHandler<LoginLog>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="principalUser">登录用户</param>
        /// <param name="loginLogId">登录用户</param>
        /// <param name="accessor">登录用户</param>
        public LoginLogHandler(PrincipalUser principalUser,
            IHttpContextAccessor accessor,
            Guid loginLogId) : base("SystemLoginLog")
        {
            Log = new LoginLog
            {
                LoginLogId = loginLogId,
                RemoteIp = IpBrowserUtil.GetRemoteIp(accessor),
                RemoteIpAddress = IpBrowserUtil.GetRemoteIpAddress(accessor),
                CreateUserId = principalUser.UserId,
                CreateUserName = principalUser.Name,
                CreateTime = DateTime.Now,
                CreateUserCode = principalUser.Code,
                LoginTime = DateTime.Now
            };
        }
    }
}