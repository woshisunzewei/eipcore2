using EIP.Common.DataAccess;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EIP.System.DataAccess.Permission
{
    public interface ISystemDataRepository : IAsyncRepository<SystemData>
    {
        /// <summary>
        ///     根据菜单Id获取数据权限规则
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemDataOutput>> GetDataByMenuId(SystemDataGetDataByMenuIdInput input=null);
    }
}