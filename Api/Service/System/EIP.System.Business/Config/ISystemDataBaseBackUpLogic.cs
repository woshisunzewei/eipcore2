using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Dtos.Config;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Config
{
    /// <summary>
    /// 数据库备份操作
    /// </summary>
    public interface ISystemDataBaseBackUpLogic : IAsyncLogic<SystemDataBaseBackUp>
    {
        /// <summary>
        /// 数据库备份
        /// </summary>
        /// <param name="doubleWay"></param>
        /// <returns></returns>
        Task<OperateStatus> SystemDataBaseBackUp(SystemDataBaseBackUpDoubleWay doubleWay);

        /// <summary>
        /// 数据库还原
        /// </summary>
        /// <param name="doubleWay"></param>
        /// <returns></returns>
        Task<OperateStatus> SystemDataBaseRestore(SystemDataBaseBackUpDoubleWay doubleWay);

        /// <summary>
        /// 获取数据库备份信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemDataBaseBackUpOutput>> GetDataBaseBackUpOrRestore(IdInput input);
    }
}
