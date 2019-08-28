using System.Threading.Tasks;
using EIP.Common.Core.Auth;
using EIP.Common.Core.Log;
using EIP.Common.Entities;
using EIP.Common.Restful.Extension;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace EIP.Common.Restful.Filter
{
    /// <summary>
    /// 全局异常过滤器
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IHostingEnvironment _env;
        private readonly PrincipalUser _currentUser;
        private readonly IHttpContextAccessor _accessor;
        public ExceptionFilter(ILoggerFactory loggerFactory,
            IHostingEnvironment env,
            IHttpContextAccessor accessor)
        {
            _loggerFactory = loggerFactory;
            _env = env;
            _accessor = accessor;
            _currentUser = accessor.CurrentUser();
        }

        public void OnException(ExceptionContext context)
        {
            var logger = _loggerFactory.CreateLogger(context.Exception.TargetSite.ReflectedType);
            logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);
            var json = new OperateStatus();
            if (_env.IsDevelopment())
                json.Message = context.Exception;
            //记录日志到MongoDb
            ExceptionLogHandler handler = new ExceptionLogHandler(context.Exception, _accessor,
                _currentUser);
            handler.WriteLog();
            
        }
    }
}
