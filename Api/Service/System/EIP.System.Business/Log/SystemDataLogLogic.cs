using EIP.Common.Business;
using EIP.System.DataAccess.Log;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Log
{
    public class SystemDataLogLogic : MongoDbLogic<SystemDataLog>, ISystemDataLogLogic
    {
        #region ¹¹Ôìº¯Êý

        private readonly ISystemDataLogRepository _dataLogRepository;

        public SystemDataLogLogic(ISystemDataLogRepository dataLogRepository)
            : base(dataLogRepository)
        {
            _dataLogRepository = dataLogRepository;
        }

        #endregion
    }
}