using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EIP.Common.Core.Extensions;
using EIP.Common.Dapper;
using EIP.Common.DataAccess;
using EIP.Common.Entities;
using EIP.Common.Entities.Paging;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Permission
{
    public class SystemFieldRepository : DapperAsyncRepository<SystemField>, ISystemFieldRepository
    {
        /// <summary>
        ///     根据菜单Id获取对应的字段信息
        /// </summary>
        /// <param name="paging">菜单Id</param>
        /// <returns></returns>
        public Task<PagedResults<SystemFieldOutput>> GetFieldByMenuId(SystemFieldPagingInput paging)
        {
            var sql = new StringBuilder(
                "SELECT field.*,menu.Name MenuName,@rowNumber, @recordCount  FROM System_Field field LEFT JOIN System_Menu menu ON field.MenuId=menu.MenuId @where");
            if (!paging.MenuId.IsNullOrEmptyGuid())
            {
                sql.Append(string.Format(" AND menu.MenuId='{0}'", paging.MenuId));
            }
            if (!paging.IsShowHidden)
            {
                sql.Append(string.Format(" AND field.Hidden='true' AND field.IsFreeze='false'"));
            }
            return PagingQueryAsync<SystemFieldOutput>(sql.ToString(), paging);
        }

        /// <summary>
        ///     根据菜单Id和用户Id获取字段权限数据
        /// </summary>
        /// <param name="mvcRote"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<SystemField> GetFieldByMenuIdAndUserId(MvcRote mvcRote, Guid userId)
        {
            const string procName = @"System_Proc_GetFieldPermissions";
            return SqlMapperUtil.StoredProcWithParamsSync<SystemField>(procName,
                new { UserId = userId, mvcRote.Area, mvcRote.Controller, mvcRote.Action });
        }
    }
}