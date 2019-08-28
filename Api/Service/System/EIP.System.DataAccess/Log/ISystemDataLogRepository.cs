using EIP.Common.DataAccess;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Log
{
    public interface ISystemDataLogRepository : IAsyncMongoDbRepository<SystemDataLog>
    {
       
    }
}