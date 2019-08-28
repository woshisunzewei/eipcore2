using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Entities;
using EIP.System.Models.Enums;

namespace EIP.System.Business.Identity
{
    public interface ISystemRoleLogic : IAsyncLogic<SystemRole>
    {
        /// <summary>
        ///     根据组织机构Id查询角色信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemRoleOutput>> GetRolesByOrganizationId(SystemRolesGetByOrganizationId input);

        /// <summary>
        /// 获取角色用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SystemRoleChosenOutput> GetChosenRoles(IdInput input);

        /// <summary>
        ///     保存岗位信息
        /// </summary>
        /// <param name="role">岗位信息</param>
        /// <returns></returns>
        Task<OperateStatus> SaveRole(SystemRole role);

        /// <summary>
        ///     删除角色信息
        /// </summary>
        /// <param name="input">角色Id</param>
        /// <returns></returns>
        Task<OperateStatus> DeleteRole(IdInput input);

        /// <summary>
        ///     角色复制
        /// </summary>
        /// <param name="input">角色Id</param>
        /// <returns></returns>
        Task<OperateStatus> CopyRole(SystemCopyInput input);

        /// <summary>
        ///     获取该用户已经具有的角色信息
        /// </summary>
        /// <param name="privilegeMaster"></param>
        /// <param name="userId">需要查询的用户id</param>
        /// <returns></returns>
        Task<IEnumerable<SystemRole>> GetHaveUserRole(EnumPrivilegeMaster privilegeMaster,
            Guid userId);
    }
}