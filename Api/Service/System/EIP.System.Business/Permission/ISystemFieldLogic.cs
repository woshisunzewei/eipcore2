using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Paging;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Permission
{
    /// <summary>
    ///     系统字段项业务逻辑
    /// </summary>
    public interface ISystemFieldLogic : IAsyncLogic<SystemField>
    {
        /// <summary>
        ///     根据菜单Id获取字段信息
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        Task<PagedResults<SystemFieldOutput>> GetFieldByMenuId(SystemFieldPagingInput paging);

        /// <summary>
        ///     保存字段信息
        /// </summary>
        /// <param name="field">字段信息</param>
        /// <returns></returns>
        Task<OperateStatus> SaveField(SystemField field);

        /// <summary>
        ///     删除字段信息
        /// </summary>
        /// <param name="input">字段Id</param>
        /// <returns></returns>
        Task<OperateStatus> DeleteField(IdInput input);
    }
}