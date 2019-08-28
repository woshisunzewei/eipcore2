using EIP.Common.Core.Auth;
using EIP.Common.Core.Log;
using EIP.Common.Restful.Attribute;
using EIP.Common.Restful.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace EIP.Common.Restful.Filter
{
    public class ActionFilter : ActionFilterAttribute
    {
        /// <summary>
        ///     用户操作日志
        /// </summary>
        private OperationLogHandler _operationLogHandler;
        private readonly PrincipalUser _currentUser;
        private readonly IHttpContextAccessor _accessor;
        public ActionFilter(
            IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            _currentUser = accessor.CurrentUser();
        }

        /// <summary>
        /// 执行完毕
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            var response = context.HttpContext.Response;
            _operationLogHandler.Log.ResponseStatus = response.StatusCode.ToString();
            _operationLogHandler.ActionExecuted();
            _operationLogHandler.WriteLog();
        }

        /// <summary>
        /// 开始执行
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var request = context.HttpContext.Request;
            _operationLogHandler = new OperationLogHandler(_accessor, request, _currentUser)
            {
                Log =
                {
                    ControllerName = ((ControllerActionDescriptor) context.ActionDescriptor).ControllerName,
                    ActionName = ((ControllerActionDescriptor) context.ActionDescriptor).ActionName
                }
            };
            var descriptionAttribute = context.ActionDescriptor.FilterDescriptors;
            if (!descriptionAttribute.Any())
                return;
            foreach (var attr in descriptionAttribute)
            {
                var info = attr.Filter.ToString();
                if (info == "EIP.Common.Restful.Attribute.RemarkAttribute")
                {
                    _operationLogHandler.Log.Describe = ((RemarkAttribute)attr.Filter).Describe;
                }
            }
        }
    }
}