using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Log
{
    public interface ISystemLoginLogRepository : IAsyncMongoDbRepository<SystemLoginLog>
    {
        /// <summary>
        /// 获取日志分析报表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SystemLoginLog>> GetBrowserAnalysis();
    }
}