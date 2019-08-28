using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http;

namespace EIP.Common.Core.Utils
{
    public static class IpBrowserUtil
    {
        /// <summary>
        /// 获取客户端Ip
        /// </summary>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public static string GetRemoteIp(IHttpContextAccessor accessor)
        {
            var remoteIp = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            if (remoteIp == "::1")
            {
                remoteIp = "127.0.0.1";
            }
            return remoteIp;
        }
        /// <summary>
        /// 获取客户端Ip物理地址
        /// </summary>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public static string GetRemoteIpAddress(IHttpContextAccessor accessor)
        {
            var apiUrl = "http://whois.pconline.com.cn/ip.jsp";
            var str = new ConcurrentDictionary<string, string>();
            str.TryAdd("ip", GetRemoteIp(accessor));
            return RequestUtil.DoGet(apiUrl, str).Replace("\n", string.Empty).Replace("\r", string.Empty);
        }
    }
}