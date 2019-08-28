using System;
using EIP.Common.Entities;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EIP.Common.Restful.Filter
{
    public class ModelStateFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 验证模型认证是否通过
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            //OperateStatus operateStatus = new OperateStatus();
            //if (context.ModelState.IsValid)
            //    return;
            //if (!context.ModelState.IsValid)
            //{
            //    var msg = string.Empty;
            //    foreach (var value in context.ModelState.Values)
            //    {
            //        if (value.Errors.Count > 0)
            //        {
            //            foreach (var error in value.Errors)
            //            {
            //                msg = msg + error.ErrorMessage + ",";
            //            }
            //        }
            //    }
            //    operateStatus.Message = msg.TrimEnd(',');
            //    throw new Exception(operateStatus.Message.ToString());
            //}
        }
    }
}