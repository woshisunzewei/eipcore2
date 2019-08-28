using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Identity
{
    public interface ISystemPostLogic : IAsyncLogic<SystemPost>
    {
        /// <summary>
        ///     根据组织机构获取岗位信息
        /// </summary>
        /// <param name="input">组织机构Id</param>
        /// <returns></returns>
        Task<IEnumerable<SystemPostOutput>> GetPostByOrganizationId(SystemPostGetByOrganizationId input);

        /// <summary>
        ///     删除岗位信息
        /// </summary>
        /// <param name="input">岗位信息Id</param>
        /// <returns></returns>
        Task<OperateStatus> DeletePost(IdInput input);

        /// <summary>
        ///     保存岗位信息
        /// </summary>
        /// <param name="post">岗位信息</param>
        /// <returns></returns>
        Task<OperateStatus> SavePost(SystemPost post);

        /// <summary>
        ///     复制
        /// </summary>
        /// <param name="input">复制信息</param>
        /// <returns></returns>
        Task<OperateStatus> CopyPost(SystemCopyInput input);
    }
}