using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Entities;
using EIP.System.Models.Enums;

namespace EIP.System.DataAccess.Identity
{
    public interface ISystemRoleRepository : IAsyncRepository<SystemRole>
    {
        /// <summary>
        ///     根据组织机构获取角色信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemRoleOutput>> GetRolesByOrganizationId(SystemRolesGetByOrganizationId input);
        
        /// <summary>
        ///     获取该用户已经具有的角色信息
        /// </summary>
        /// <param name="privilegeMaster"></param>
        /// <param name="userId">需要查询的用户id</param>
        /// <returns></returns>
        Task<IEnumerable<SystemRoleOutput>> GetHaveUserRole(EnumPrivilegeMaster privilegeMaster,
            Guid userId);
    }
}