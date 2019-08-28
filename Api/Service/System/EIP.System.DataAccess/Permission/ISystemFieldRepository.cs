using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Paging;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Permission
{
    public interface ISystemFieldRepository : IAsyncRepository<SystemField>
    {
        /// <summary>
        ///     根据菜单Id获取对应的字段信息
        /// </summary>
        /// <param name="fieldPagingDto">字段分页信息</param>
        /// <returns></returns>
        Task<PagedResults<SystemFieldOutput>> GetFieldByMenuId(SystemFieldPagingInput fieldPagingDto);
    }
}