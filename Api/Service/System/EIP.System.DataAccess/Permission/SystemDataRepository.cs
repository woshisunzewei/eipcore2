using EIP.Common.Core.Extensions;
using EIP.Common.Dapper;
using EIP.Common.DataAccess;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EIP.System.DataAccess.Permission
{
    public class SystemDataRepository : DapperAsyncRepository<SystemData>, ISystemDataRepository
    {
        /// <summary>
        ///     根据菜单id获取数据权限定义
        /// </summary>
        /// <param name="input">菜单id</param>
        /// <returns></returns>
        public Task<IEnumerable<SystemDataOutput>> GetDataByMenuId(SystemDataGetDataByMenuIdInput input = null)
        {
            var sql = new StringBuilder(
                "SELECT data.*,menu.Name MenuName FROM System_Data data LEFT JOIN System_Menu menu ON data.MenuId=menu.MenuId WHERE 1=1");
            if (input != null)
            {
                sql.Append(input.Sql);
                if (!input.Id.IsNullOrEmptyGuid())
                {
                    sql.Append(" AND data.MenuId=@menuId");
                    return SqlMapperUtil.SqlWithParams<SystemDataOutput>(sql.ToString(), new { menuId = input.Id });
                }
            }
            sql.Append(" ORDER BY data.OrderNo");
            return SqlMapperUtil.SqlWithParams<SystemDataOutput>(sql.ToString());
        }
    }
}