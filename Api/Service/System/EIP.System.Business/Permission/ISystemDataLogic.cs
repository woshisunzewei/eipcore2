using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EIP.System.Business.Permission
{
    /// <summary>
    ///     数据权限业务逻辑
    /// </summary>
    public interface ISystemDataLogic : IAsyncLogic<SystemData>
    {
        /// <summary>
        ///     根据菜单Id获取数据权限规则
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemDataOutput>> GetDataByMenuId(SystemDataGetDataByMenuIdInput input);

        /// <summary>
        ///     保存数据权限规则
        /// </summary>
        /// <param name="data">数据权限规则</param>
        /// <returns></returns>
        Task<OperateStatus> SaveData(SystemDataOutput data);

        /// <summary>
        ///     删除数据权限规则信息
        /// </summary>
        /// <param name="input">数据权限规则Id</param>
        /// <returns></returns>
        Task<OperateStatus> DeleteByDataId(IdInput input);
    }
}