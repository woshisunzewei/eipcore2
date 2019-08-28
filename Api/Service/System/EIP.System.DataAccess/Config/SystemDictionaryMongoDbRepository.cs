using EIP.Common.DataAccess;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Config
{
    /// <summary>
    ///     字典数据访问接口实现
    /// </summary>
    public class SystemDictionaryMongoDbRepository : MongoDbAsyncRepository<SystemDictionary>, ISystemDictionaryMongoDbRepository
    {
        
    }
}