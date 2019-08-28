using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Core.Resource;
using EIP.Common.Core.Utils;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.System.DataAccess.Config;
using EIP.System.Models.Dtos.Config;
using EIP.System.Models.Entities;
using EIP.System.Models.Enums;

namespace EIP.System.Business.Config
{
    public class SystemDataBaseBackUpLogic : DapperAsyncLogic<SystemDataBaseBackUp>, ISystemDataBaseBackUpLogic
    {
        #region 构造函数
        private readonly ISystemDataBaseBackUpRepository _systemDataBaseBackUpRepository;
        private readonly ISystemDataBaseRepository _systemDataBaseRepository;
        public SystemDataBaseBackUpLogic(ISystemDataBaseBackUpRepository systemDataBaseBackUpRepository,
         ISystemDataBaseRepository systemDataBaseRepository)
            : base(systemDataBaseBackUpRepository)
        {
            _systemDataBaseBackUpRepository = systemDataBaseBackUpRepository;
            _systemDataBaseRepository = systemDataBaseRepository;
        }

        #endregion

        /// <summary>
        /// 数据库备份
        /// </summary>
        /// <param name="doubleWay"></param>
        /// <returns></returns>
        public async Task<OperateStatus> SystemDataBaseBackUp(SystemDataBaseBackUpDoubleWay doubleWay)
        {
            OperateStatus operateStatus = new OperateStatus();
            //SystemDataBase systemDataBase = await _systemDataBaseRepository.FindByIdAsync(doubleWay.DataBaseId);
            //string bakPath = "\\DataUsers\\DataBaseBackUps\\" + systemDataBase.CatalogName + DateTime.Now.ToString("yyyyMMddhhssmm") + ".bak";
            ////开始进行数据库备份操作
            //DbBackUpAndRestore.Uid = systemDataBase.UserId;
            //DbBackUpAndRestore.Database = systemDataBase.CatalogName;
            //DbBackUpAndRestore.Pwd = systemDataBase.Password;
            //DbBackUpAndRestore.Server = systemDataBase.DataSource;
            //DbBackUpAndRestore.BackUpOrRestorePath = doubleWay.BackUpOrRestorePath + bakPath; //程序根目录地址
            //try
            //{
            //    if (DbBackUpAndRestore.Operate())
            //    {
            //        //添加备份记录
            //        SystemDataBaseBackUp backUp = new SystemDataBaseBackUp()
            //        {
            //            BackUpId = CombUtil.NewComb(),
            //            BackUpTime = DateTime.Now,
            //            DataBaseId = doubleWay.DataBaseId,
            //            From = (byte)EnumFrom.手动,
            //            Name = systemDataBase.CatalogName + DateTime.Now.ToString("yyyyMMddhhssmm") + ".bak",
            //            Path = DbBackUpAndRestore.BackUpOrRestorePath,
            //            Size = FileUtil.GetFileSize(DbBackUpAndRestore.BackUpOrRestorePath)
            //        };
            //        operateStatus = await InsertAsync(backUp);
            //    }
            //    else
            //    {
            //        operateStatus.Message = Chs.Error;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    operateStatus.Message = ex.Message;
            //}
            return operateStatus;
        }

        /// <summary>
        /// 数据库还原
        /// </summary>
        /// <param name="doubleWay"></param>
        /// <returns></returns>
        public async Task<OperateStatus> SystemDataBaseRestore(SystemDataBaseBackUpDoubleWay doubleWay)
        {
            OperateStatus operateStatus = new OperateStatus();
            //SystemDataBaseBackUp baseBackUp = await GetByIdAsync(doubleWay.Id);
            //SystemDataBase systemDataBase = await _systemDataBaseRepository.GetByIdAsync(baseBackUp.DataBaseId);
            ////开始进行数据库备份操作
            //DbBackUpAndRestore.Uid = systemDataBase.UserId;
            //DbBackUpAndRestore.Database = systemDataBase.CatalogName;
            //DbBackUpAndRestore.Pwd = systemDataBase.Password;
            //DbBackUpAndRestore.Server = systemDataBase.DataSource;
            //DbBackUpAndRestore.BackUpOrRestorePath = baseBackUp.Path; //程序根目录地址
            //try
            //{
            //    //查询所有备份表信息
            //    IList<SystemDataBaseBackUp> dataBaseBackUps = (await GetAllEnumerableAsync()).ToList();
            //    if (DbBackUpAndRestore.Operate(false))
            //    {
            //        //删除备份表
            //        await DeleteAllAsync();
            //        //更换备份表还原时间
            //        var ups = dataBaseBackUps.Where(s => s.BackUpId == baseBackUp.BackUpId).FirstOrDefault();
            //        if (ups != null)
            //            ups.RestoreTime = DateTime.Now;
            //        //批量插入备份表信息
            //        operateStatus =await InsertMultipleAsync(dataBaseBackUps);
            //    }
            //    else
            //    {
            //        operateStatus.Message = Chs.Error;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    operateStatus.Message = ex.Message;
            //}
            return operateStatus;
        }

        /// <summary>
        /// 获取数据库备份信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemDataBaseBackUpOutput>> GetDataBaseBackUpOrRestore(IdInput input)
        {
            return await _systemDataBaseBackUpRepository.GetDataBaseBackUpOrRestore(input);
        }
    }
}
