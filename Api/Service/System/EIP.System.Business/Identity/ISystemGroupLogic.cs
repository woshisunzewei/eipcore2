using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Enums;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Identity
{
    public interface ISystemGroupLogic : IAsyncLogic<SystemGroup>
    {
        /// <summary>
        ///     根据组织机构获取组信息
        /// </summary>
        /// <param name="input">组织机构</param>
        /// <returns></returns>
        Task<IEnumerable<SystemGroupOutput>> GetGroupByOrganizationId(SystemGroupGetGroupByOrganizationIdInput input);

        /// <summary>
        ///     删除组信息
        /// </summary>
        /// <param name="input">组Id</param>
        /// <returns></returns>
       Task< OperateStatus> DeleteGroup(IdInput input);

        /// <summary>
        ///     保存组信息
        /// </summary>
        /// <param name="group">岗位信息</param>
        /// <param name="belongTo">归属</param>
        /// <returns></returns>
        Task<OperateStatus> SaveGroup(SystemGroup group,
            EnumGroupBelongTo belongTo);

        /// <summary>
        ///     复制
        /// </summary>
        /// <param name="input">复制信息</param>
        /// <returns></returns>
        Task<OperateStatus> CopyGroup(SystemCopyInput input);
    }
}