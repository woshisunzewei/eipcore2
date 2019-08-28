using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EIP.Common.Dapper;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using EIP.System.Models.Enums;

namespace EIP.System.DataAccess.Permission
{
    public class SystemPermissionUserRepository : DapperAsyncRepository<SystemPermissionUser>,
        ISystemPermissionUserRepository
    {
        /// <summary>
        ///     根据用户Id删除权限用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<int> DeletePermissionUser(IdInput input)
        {
            const string sql = "DELETE FROM System_PermissionUser WHERE PrivilegeMasterUserId=@userId";
            return SqlMapperUtil.InsertUpdateOrDeleteSql<SystemPermissionUser>(sql, new { userId = input.Id });
        }

        /// <summary>
        ///     根据特性Id删除权限用户信息
        /// </summary>
        /// <param name="privilegeMaster">特性类型:组织机构、角色、岗位、组等</param>
        /// <param name="privilegeMasterValue">特性类型Id:组织机构、角色、岗位、组等</param>
        /// <returns></returns>
        public Task<int> DeletePermissionUser(EnumPrivilegeMaster privilegeMaster,
            Guid privilegeMasterValue)
        {
            const string sql = "DELETE FROM System_PermissionUser WHERE PrivilegeMaster=@privilegeMaster AND PrivilegeMasterValue=@privilegeMasterValue";
            return SqlMapperUtil.InsertUpdateOrDeleteSql<SystemPermissionUser>(sql,
                new
                {
                    privilegeMaster = (byte)privilegeMaster,
                    privilegeMasterValue
                });
        }

        /// <summary>
        ///     删除用户
        /// </summary>
        /// <param name="privilegeMasterUserId">用户Id</param>
        /// <param name="privilegeMasterValue">归属类型Id:组织机构、角色、岗位、组</param>
        /// <param name="privilegeMaster">归属人员类型:组织机构、角色、岗位、组</param>
        /// <returns></returns>
        public Task<int> DeletePrivilegeMasterUser(Guid privilegeMasterUserId,
            Guid privilegeMasterValue,
            EnumPrivilegeMaster privilegeMaster)
        {
            const string sql = "DELETE FROM System_PermissionUser WHERE PrivilegeMaster=@privilegeMaster AND PrivilegeMasterUserId=@privilegeMasterUserId AND PrivilegeMasterValue=@privilegeMasterValue";
            return SqlMapperUtil.InsertUpdateOrDeleteSql<SystemPermissionUser>(sql,
                new { privilegeMaster = (byte)privilegeMaster, privilegeMasterUserId, privilegeMasterValue });
        }


        /// <summary>
        ///     删除用户
        /// </summary>
        /// <param name="privilegeMasterUserId">用户Id</param>
        /// <param name="privilegeMaster">归属人员类型:组织机构、角色、岗位、组</param>
        /// <returns></returns>
        public Task<int> DeletePrivilegeMasterUser(Guid privilegeMasterUserId,
            EnumPrivilegeMaster privilegeMaster)
        {
            const string sql = "DELETE FROM System_PermissionUser WHERE PrivilegeMaster=@privilegeMaster AND PrivilegeMasterUserId=@privilegeMasterUserId";
            return SqlMapperUtil.InsertUpdateOrDeleteSql<SystemPermissionUser>(sql,
                new { privilegeMaster = (byte)privilegeMaster, privilegeMasterUserId });
        }

        /// <summary>
        ///     根据用户Id删除用户归属类型
        /// </summary>
        /// <param name="privilegeMaster">归属人员类型:组织机构、角色、岗位、组</param>
        /// <param name="privilegeMasterUserId">用户Id</param>
        /// <returns></returns>
        public Task<int> DeletePermissionMaster(EnumPrivilegeMaster privilegeMaster,
            Guid privilegeMasterUserId)
        {
            const string sql = "DELETE FROM System_PermissionUser WHERE PrivilegeMaster=@privilegeMaster AND PrivilegeMasterUserId=@privilegeMasterUserId";
            return SqlMapperUtil.InsertUpdateOrDeleteSql<SystemPermissionUser>(sql,
                new { privilegeMaster = (byte)privilegeMaster, privilegeMasterUserId });
        }

        /// <summary>
        ///     根据用户Id获取角色、组、岗位信息
        /// </summary>
        /// <param name="input">人员Id</param>
        /// <returns></returns>
        public Task<IEnumerable<SystemPrivilegeDetailListOutput>> GetSystemPrivilegeDetailOutputsByUserId(IdInput input)
        {
            var sql = string.Format(@"SELECT PrivilegeMaster,PrivilegeMasterValue,
                       CASE PrivilegeMaster 
                        WHEN {0} THEN --角色
                        (SELECT Name FROM dbo.System_Role WHERE RoleId=PrivilegeMasterValue) 
                         WHEN {1} THEN --组
                        (SELECT Name FROM dbo.System_Group WHERE GroupId=PrivilegeMasterValue) 
                         WHEN {2} THEN --岗位
                        (SELECT Name FROM dbo.System_Post WHERE PostId=PrivilegeMasterValue)
                         WHEN {3} THEN --组织机构
                        (SELECT Name FROM dbo.System_Organization WHERE OrganizationId=PrivilegeMasterValue)
                        END Name,

                        CASE PrivilegeMaster 
                        WHEN {0} THEN --角色
                        (
                        SELECT OrganizationId FROM dbo.System_Organization WHERE OrganizationId =
                        (SELECT OrganizationId FROM dbo.System_Role WHERE RoleId=PrivilegeMasterValue)
                        ) 
                         WHEN {1} THEN --组
                        (
                        SELECT OrganizationId FROM dbo.System_Organization WHERE OrganizationId =
                        (SELECT OrganizationId FROM dbo.System_Group WHERE GroupId=PrivilegeMasterValue) 
                        )
                         WHEN {2} THEN --岗位
                        (
                        SELECT OrganizationId FROM dbo.System_Organization WHERE OrganizationId =
                        (SELECT OrganizationId FROM dbo.System_Post WHERE PostId=PrivilegeMasterValue)
                        )
                        END OrganizationId
                        FROM dbo.System_PermissionUser WHERE PrivilegeMasterUserId=@userId AND PrivilegeMaster IN ({0},{1},{2},{3})",
                (byte)EnumPrivilegeMaster.角色, (byte)EnumPrivilegeMaster.组, (byte)EnumPrivilegeMaster.岗位, (byte)EnumPrivilegeMaster.组织机构);
            return SqlMapperUtil.SqlWithParams<SystemPrivilegeDetailListOutput>(sql, new
            {
                userId = input.Id
            });
        }

        /// <summary>
        ///     根据特权类型及特权id获取特权用户信息
        /// </summary>
        /// <param name="privilegeMaster">特权类型</param>
        /// <param name="privilegeMasterValue">特权id</param>
        /// <returns></returns>
        public Task<IEnumerable<SystemPermissionUser>> GetPermissionUsersByPrivilegeMasterAdnPrivilegeMasterValue(
            EnumPrivilegeMaster privilegeMaster,
            Guid? privilegeMasterValue = null)
        {
            var sql =
                new StringBuilder("SELECT * FROM System_PermissionUser WHERE PrivilegeMaster=@privilegeMaster");
            if (privilegeMasterValue != null)
            {
                sql.Append(" AND PrivilegeMasterValue=@privilegeMasterValue");
            }
            return SqlMapperUtil.SqlWithParams<SystemPermissionUser>(sql.ToString(),
                new { privilegeMaster, privilegeMasterValue });
        }

        /// <summary>
        ///     获取菜单、字段对应拥有者信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<IEnumerable<SystemPrivilegeDetailListOutput>> GetSystemPrivilegeDetailOutputsByAccessAndValue(
            SystemPrivilegeDetailInput input)
        {
            var sql = string.Format(@"SELECT PrivilegeMaster,
                        CASE PrivilegeMaster 
                        WHEN {0} THEN --角色
                        (SELECT Name FROM dbo.System_Role WHERE RoleId=per.PrivilegeMasterValue) 
						WHEN  {1} THEN --组织机构
                        (SELECT Name FROM dbo.System_Organization WHERE OrganizationId=per.PrivilegeMasterValue) 
                         WHEN {2} THEN --组
                        (SELECT Name FROM dbo.System_Group WHERE GroupId=per.PrivilegeMasterValue) 
                         WHEN {3} THEN --岗位
                        (SELECT Name FROM dbo.System_Post WHERE PostId=per.PrivilegeMasterValue)
						WHEN  {4} THEN --人员
                        (SELECT Name FROM dbo.System_UserInfo WHERE UserId=per.PrivilegeMasterValue) 
                        END Name,--名称

                        CASE PrivilegeMaster 
                        WHEN {0} THEN --角色
                        (
                        SELECT OrganizationId FROM dbo.System_Organization WHERE OrganizationId =
                        (SELECT OrganizationId FROM dbo.System_Role WHERE RoleId=per.PrivilegeMasterValue)
                        ) 
						WHEN  {1} THEN --组织机构
                        (SELECT Name FROM dbo.System_Organization WHERE OrganizationId=per.PrivilegeMasterValue) 
                         WHEN {2} THEN --组
                        (
                        SELECT OrganizationId FROM dbo.System_Organization WHERE OrganizationId =
                        (SELECT OrganizationId FROM dbo.System_Group WHERE GroupId=per.PrivilegeMasterValue) 
                        )
                         WHEN {3} THEN --岗位
                        (
                        SELECT OrganizationId FROM dbo.System_Organization WHERE OrganizationId =
                        (SELECT OrganizationId FROM dbo.System_Post WHERE PostId=per.PrivilegeMasterValue)
                        )
						WHEN  {4} THEN --人员
                        (
                        SELECT OrganizationId FROM dbo.System_Organization WHERE OrganizationId =
                        (SELECT PrivilegeMasterValue FROM System_PermissionUser u
						 WHERE  u.PrivilegeMaster={1} AND u.PrivilegeMasterUserId=per.PrivilegeMasterValue)
                        )
                        END OrganizationId --组织机构		
                        FROM  dbo.System_Permission per
						WHERE PrivilegeAccessValue=@privilegeAccessValue AND PrivilegeAccess=@privilegeAccess AND PrivilegeMaster IN ({0},{1},{2},{3},{4})",
                EnumPrivilegeMaster.角色.GetHashCode(),
                EnumPrivilegeMaster.组织机构.GetHashCode(),
                EnumPrivilegeMaster.组.GetHashCode(),
                EnumPrivilegeMaster.岗位.GetHashCode(),
                EnumPrivilegeMaster.人员.GetHashCode());
            return SqlMapperUtil.SqlWithParams<SystemPrivilegeDetailListOutput>(sql, new
            {
                privilegeAccessValue = input.Id,
                privilegeAccess = (byte)input.Access
            });
        }
    }
}