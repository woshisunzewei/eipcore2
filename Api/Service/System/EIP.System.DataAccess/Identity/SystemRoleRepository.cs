using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EIP.Common.Core.Extensions;
using EIP.Common.Dapper;
using EIP.Common.DataAccess;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Entities;
using EIP.System.Models.Enums;

namespace EIP.System.DataAccess.Identity
{
    public class SystemRoleRepository : DapperAsyncRepository<SystemRole>, ISystemRoleRepository
    {
        /// <summary>
        ///     根据组织机构获取角色信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<IEnumerable<SystemRoleOutput>> GetRolesByOrganizationId(SystemRolesGetByOrganizationId input)
        {
            var sql =
                new StringBuilder(
                    @"SELECT role.*,org.CreateUserId,org.CreateUserName,org.UpdateTime,org.UpdateUserId,org.UpdateUserName,org.Name OrganizationName FROM System_Role role
                                                    LEFT JOIN System_Organization org ON org.OrganizationId=role.OrganizationId WHERE 1=1");
        
            if (input != null && !input.Id.IsNullOrEmptyGuid())
            {
                sql.Append(@" AND role.OrganizationId in(
                select org.OrganizationId from System_Organization org where org.ParentIds  like '" + (input.Id + ",").Replace(",", @"\,") + "%" + "' escape '\\' OR OrganizationId = '" + input.Id + "') ");
            }
            if (input != null)
            {
                sql.Append(input.Sql);
            }
            sql.Append(" ORDER BY role.OrganizationId,role.OrderNo");
            if (input != null && !input.Id.IsNullOrEmptyGuid())
            {
                return SqlMapperUtil.SqlWithParams<SystemRoleOutput>(sql.ToString(), new {orgId = input.Id});
            }
            return SqlMapperUtil.SqlWithParams<SystemRoleOutput>(sql.ToString());
        }
        
        /// <summary>
        ///     获取该用户已经具有的角色信息
        /// </summary>
        /// <param name="privilegeMaster"></param>
        /// <param name="userId">需要查询的用户id</param>
        /// <returns></returns>
        public Task<IEnumerable<SystemRoleOutput>> GetHaveUserRole(EnumPrivilegeMaster privilegeMaster,
            Guid userId)
        {
            var sql = new StringBuilder(@"SELECT * FROM System_Role ro
                                                    LEFT JOIN System_PermissionUser per ON per.PrivilegeMasterValue=ro.RoleId
                                                    WHERE per.PrivilegeMasterUserId=@userId AND PrivilegeMaster=@privilegeMaster");
            return SqlMapperUtil.SqlWithParams<SystemRoleOutput>(sql.ToString(),
                new {privilegeMaster = (byte) privilegeMaster, userId});
        }
    }
}