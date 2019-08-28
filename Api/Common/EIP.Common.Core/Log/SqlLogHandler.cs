using EIP.Common.Core.Auth;
using EIP.Common.Core.Utils;
using System;

namespace EIP.Common.Core.Log
{
    public class SqlLogHandler : BaseHandler<SqlLog>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlLogHandler(string operateSql,
            DateTime endDateTime,
            double elapsedTime,
            string parameter
            ) : base("SqlLog")
        {
           
            Log = new SqlLog
            {
                SqlLogId = CombUtil.NewComb(),
                CreateTime = DateTime.Now,
                OperateSql = operateSql,
                ElapsedTime = elapsedTime,
                EndDateTime = endDateTime,
                Parameter = parameter
            };
        }
    }
}