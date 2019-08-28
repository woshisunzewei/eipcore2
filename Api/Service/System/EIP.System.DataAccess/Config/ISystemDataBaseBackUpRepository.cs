using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Dtos.Config;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Config
{
    public interface ISystemDataBaseBackUpRepository : IAsyncRepository<SystemDataBaseBackUp>
    {
        /// <summary>
        /// 获取数据库备份信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemDataBaseBackUpOutput>> GetDataBaseBackUpOrRestore(IdInput input);
    }
}
