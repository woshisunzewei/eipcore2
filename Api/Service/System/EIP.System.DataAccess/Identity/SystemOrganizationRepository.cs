using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EIP.Common.Core.Extensions;
using EIP.Common.Dapper;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Tree;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Identity
{
    public class SystemOrganizationRepository : DapperAsyncRepository<SystemOrganization>, ISystemOrganizationRepository
    {
        /// <summary>
        ///     根据父级查询下级
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<IEnumerable<JsTreeEntity>> GetSystemOrganizationByPid(IdInput input)
        {
            var sql = new StringBuilder();
            sql.Append(
                "SELECT OrganizationId id,ParentId parent,Name text FROM System_Organization WHERE ParentId=@pId ORDER BY OrderNo");
            return SqlMapperUtil.SqlWithParams<JsTreeEntity>(sql.ToString(), new { pId = input.Id });
        }

        /// <summary>
        ///     获取所有组织机构信息
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<JsTreeEntity>> GetSystemOrganization()
        {
            var sql = new StringBuilder();
            sql.Append(
                "SELECT OrganizationId id,ParentId parent,Name text FROM System_Organization ORDER BY OrderNo");
            return SqlMapperUtil.SqlWithParams<JsTreeEntity>(sql.ToString());
        }

        /// <summary>
        ///     根据父级查询下级
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<IEnumerable<SystemOrganizationOutput>> GetOrganizationsByParentId(SystemOrganizationDataPermissionTreeInput input)
        {
            var sql = new StringBuilder();
            sql.Append("select *,(select name from System_Organization o where o.OrganizationId=org.ParentId) ParentName from System_Organization org where 1=1");
            
            if (input.Id != null)
            {
                sql.Append(input.HaveSelf
                    ? "AND org.ParentIds  like '%" + input.Id + "%'"
                    : "AND org.ParentIds  like '%" + (input.Id + ", ").Replace(", ", @",") + "%' ");
            }
            if (!input.DataSql.IsNullOrEmpty())
            {
                sql.Append( "AND "+input.DataSql);
            }
            sql.Append(input.Sql);
            return SqlMapperUtil.SqlWithParams<SystemOrganizationOutput>(sql.ToString(), new { pId = input.Id });
        }

        public Task<IEnumerable<SystemOrganizationOutput>> GetOrganizationsByParentId(SystemOrganizationsByParentIdInput input)
        {
            var sql = new StringBuilder();
            sql.Append("select *,(select name from System_Organization o where o.OrganizationId=org.ParentId) ParentName from System_Organization org where 1=1");
            if (input.Id != null)
            {
                sql.Append(input.HaveSelf
                    ? "AND org.ParentIds  like '%" + input.Id + "%'"
                    : "AND org.ParentIds  like ' % " + (input.Id + ", ").Replace(", ", @"\, ") + " % " +
                      "' escape '\\'");
            }
            return SqlMapperUtil.SqlWithParams<SystemOrganizationOutput>(sql.ToString(), new { pId = input.Id });
        }

        /// <summary>
        /// 数据权限组织机构树
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<IEnumerable<JsTreeEntity>> GetOrganizationResultByDataPermission(IdInput<string> input)
        {
            if (input.Id.IsNullOrEmpty())
            {
                return null;
            }
            string sql = "SELECT OrganizationId id,ParentId parent,Name text FROM System_Organization WHERE 1=1 AND " + input.Id + " ORDER BY OrderNo";
            return SqlMapperUtil.SqlWithParams<JsTreeEntity>(sql);
        }

        /// <summary>
        /// 获取数据权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<IEnumerable<JsTreeEntity>> GetOrganizationDataPermissionTree(SystemOrganizationDataPermissionTreeInput input)
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT OrganizationId id,ParentId parent,Name text FROM System_Organization WHERE 1=1 AND {input.DataSql} ORDER BY OrderNo");
            return SqlMapperUtil.SqlWithParams<JsTreeEntity>(sql.ToString());
        }
    }
}