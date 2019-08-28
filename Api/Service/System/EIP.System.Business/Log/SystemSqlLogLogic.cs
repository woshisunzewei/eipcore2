using EIP.Common.Business;
using EIP.System.DataAccess.Log;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Log
{
    public class SystemSqlLogLogic : MongoDbLogic<SystemSqlLog>, ISystemSqlLogLogic
    {
        #region ¹¹Ôìº¯Êý

        private readonly ISystemSqlLogRepository _sqlLogRepository;

        public SystemSqlLogLogic(ISystemSqlLogRepository sqlLogRepository)
            : base(sqlLogRepository)
        {
            _sqlLogRepository = sqlLogRepository;
        }

        #endregion
    }
}