using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Identity
{
    public interface ISystemPostRepository : IAsyncRepository<SystemPost>
    {
        /// <summary>
        ///     查询归属某组织下的岗位信息
        /// </summary>
        /// <param name="input">组织机构PostId</param>
        /// <returns>岗位信息</returns>
        Task<IEnumerable<SystemPostOutput>> GetPostByOrganizationId(SystemPostGetByOrganizationId input);

    }
}