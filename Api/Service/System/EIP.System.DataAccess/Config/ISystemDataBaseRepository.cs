using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Dtos.Config;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Config
{
    /// <summary>
    /// 数据库
    /// </summary>
    public interface ISystemDataBaseRepository : IAsyncRepository<SystemDataBase>
    {
        /// <summary>
        /// 查看对应数据库空间占用情况
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemDataBaseSpaceOutput>> GetDataBaseSpaceused(IdInput input);

        /// <summary>
        /// 获取对应数据库表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemDataBaseTableDoubleWay>> GetDataBaseTables(IdInput input);

        /// <summary>
        /// 获取对应表列信息
        /// </summary>
        /// <param name="doubleWayDto"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemDataBaseColumnDoubleWay>> GetDataBaseColumns(SystemDataBaseTableDoubleWay doubleWayDto);

        /// <summary>
        /// 获取外键信息
        /// </summary>
        /// <param name="doubleWayDto"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemDataBaseFkColumnOutput>> GetdatabsefFkColumn(SystemDataBaseTableDoubleWay doubleWayDto);
        
        /// <summary>
        /// 重置数据库连接
        /// </summary>
        /// <param name="input"></param>
        void ResetConnection(IdInput input);
    }
}