using EIP.Common.Core.Auth;
using EIP.Common.Core.Utils;
using System;

namespace EIP.Common.Core.Log
{
    /// <summary>
    /// 数据日志记录
    /// </summary>
    public class DataLogHandler : BaseHandler<DataLog>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataLogHandler(byte operateType,
            string operateTable,
            string operateData=null,
            string operateAfterData=null) : base("DataLog")
        {
            PrincipalUser principalUser = new PrincipalUser
            {
                Name = "匿名用户",
                UserId = Guid.Empty
            };
            //var current = HttpContext.Current;
            //if (current != null)
            //{
            //    principalUser = FormAuthenticationExtension.Current(HttpContext.Current.Request);
            //}
            //if (principalUser == null)
            //{
            //    principalUser = new PrincipalUser()
            //    {
            //        Name = "匿名用户",
            //        UserId = Guid.Empty
            //    };
            //}
            Log = new DataLog()
            {
                OperateType = operateType,
                OperateTable = operateTable,
                OperateData = operateData,
                OperateAfterData = operateAfterData,
                CreateTime = DateTime.Now,
                CreateUserId = principalUser.UserId,
                CreateUserCode = principalUser.Code,
                CreateUserName = principalUser.Name,
                DataLogId = CombUtil.NewComb()
            };
        }
    }
}