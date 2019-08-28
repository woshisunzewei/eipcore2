using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Core.Extensions;
using EIP.Common.Core.Utils;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.System.DataAccess.Config;
using EIP.System.Models.Dtos.Config;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Config
{
    /// <summary>
    ///     系统配置文件接口实现
    /// </summary>
    public class SystemDataBaseLogic : DapperAsyncLogic<SystemDataBase>, ISystemDataBaseLogic
    {
        #region 构造函数

        private readonly ISystemDataBaseRepository _dataBaseRepository;

        public SystemDataBaseLogic(ISystemDataBaseRepository dataBaseRepository)
            : base(dataBaseRepository)
        {
            _dataBaseRepository = dataBaseRepository;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     保存数据库配置
        /// </summary>
        /// <param name="app">数据库配置</param>
        /// <returns></returns>
        public async Task<OperateStatus> SaveDataBase(SystemDataBase app)
        {
            if (!app.DataBaseId.IsEmptyGuid())
                return await UpdateAsync(app);
            app.DataBaseId = CombUtil.NewComb();
            return  await InsertAsync(app);
        }

        /// <summary>
        /// 查看对应数据库空间占用情况
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemDataBaseSpaceOutput>> GetDataBaseSpaceused(IdInput input)
        {
            return await _dataBaseRepository.GetDataBaseSpaceused(input);
        }

        /// <summary>
        /// 获取对应数据库表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemDataBaseTableDoubleWay>> GetDataBaseTables(IdInput input)
        {
            return await _dataBaseRepository.GetDataBaseTables(input);
        }

        /// <summary>
        /// 获取对应表列信息
        /// </summary>
        /// <param name="doubleWayDto"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemDataBaseColumnDoubleWay>> GetDataBaseColumns(SystemDataBaseTableDoubleWay doubleWayDto)
        {
            return await _dataBaseRepository.GetDataBaseColumns(doubleWayDto);
        }

        /// <summary>
        /// 获取外键信息
        /// </summary>
        /// <param name="doubleWayDto"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemDataBaseFkColumnOutput>> GetdatabsefFkColumn(SystemDataBaseTableDoubleWay doubleWayDto)
        {
            return await _dataBaseRepository.GetdatabsefFkColumn(doubleWayDto);
        }

        #endregion
    }
}