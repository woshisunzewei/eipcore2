using EIP.Common.Core.Extensions;
using EIP.Common.Dapper;
using EIP.Common.DataAccess;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Entities;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EIP.System.DataAccess.Identity
{
    public class SystemPostRepository : DapperAsyncRepository<SystemPost>, ISystemPostRepository
    {
        /// <summary>
        ///     查询归属某组织下的岗位信息
        /// </summary>
        /// <param name="input">组织机构PostId</param>
        /// <returns>岗位信息</returns>
        public Task<IEnumerable<SystemPostOutput>> GetPostByOrganizationId(SystemPostGetByOrganizationId input)
        {
            var sql = new StringBuilder();
            sql.Append(
                @"SELECT post.*,org.OrganizationId,org.Name OrganizationName
                         FROM System_Post post LEFT JOIN System_Organization org ON org.OrganizationId=post.OrganizationId WHERE 1=1");
            if (input != null && !input.Id.IsNullOrEmptyGuid())
            {
                sql.Append(@" AND post.OrganizationId in(
                select org.OrganizationId from System_Organization org where org.ParentIds  like '" + (input.Id + ",").Replace(",", @"\,") + "%" + "' escape '\\' OR OrganizationId = '" + input.Id + "') ");
            }
            if (input != null)
                sql.Append(input.Sql);
            sql.Append(" ORDER BY post.OrganizationId");
            return SqlMapperUtil.SqlWithParams<SystemPostOutput>(sql.ToString());
        }
    }
}