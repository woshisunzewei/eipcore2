using EIP.Common.DataAccess;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EIP.System.DataAccess.Identity
{
    public interface ISystemGroupRepository : IAsyncRepository<SystemGroup>
    {
        /// <summary>
        ///     查询归属某组织下的组信息
        /// </summary>
        /// <param name="input">组织机构PostId</param>
        /// <returns>组信息</returns>
        Task<IEnumerable<SystemGroupOutput>> GetGroupByOrganizationId(SystemGroupGetGroupByOrganizationIdInput input);

    }
}