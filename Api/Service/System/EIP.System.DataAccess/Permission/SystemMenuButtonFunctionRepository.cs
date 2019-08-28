using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EIP.Common.Core.Extensions;
using EIP.Common.Dapper;
using EIP.Common.DataAccess;
using EIP.System.Models.Entities;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Dtos.Permission;

namespace EIP.System.DataAccess.Permission
{
    /// <summary>
    /// 菜单按钮功能
    /// </summary>
    public class SystemMenuButtonFunctionRepository : DapperAsyncRepository<SystemMenuButtonFunction>, ISystemMenuButtonFunctionRepository
    {
        /// <summary>
        /// 获取菜单按钮功能项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<IEnumerable<SystemMenuButtonFunctionOutput>> GetMenuButtonFunctions(IdInput input)
        {
            const string sql = @"SELECT func.* FROM dbo.System_MenuButtonFunction buttonfunc
                                            LEFT JOIN dbo.System_Function func ON func.FunctionId = buttonfunc.FunctionId
                                           
                                            WHERE MenuButtonId=@menuId";
            return SqlMapperUtil.SqlWithParams<SystemMenuButtonFunctionOutput>(sql, new { menuId = input.Id });
        }

        /// <summary>
        /// 获取所有功能项,若input有值则排除该功能项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<IEnumerable<SystemMenuButtonFunctionOutput>> GetAllFunctions(SystemMenuButtonGetFunctionsInput input)
        {
            StringBuilder sql =
                new StringBuilder(@"SELECT func.* AppCode FROM dbo.System_Function func ");
            if (!input.Id.IsNullOrEmptyGuid())
            {
                sql.Append(
                    " WHERE 1=1 AND func.FunctionId NOT IN (SELECT FunctionId FROM System_MenuButtonFunction WHERE MenuButtonId=@menuButtonId)");
            }
            if (input.IsPage)
            {
                sql.Append(string.Format(" AND func.IsPage='{0}'", input.IsPage));
            }
            sql.Append(" ORDER BY Area,Controller,IsPage DESC");

            return SqlMapperUtil.SqlWithParams<SystemMenuButtonFunctionOutput>(sql.ToString(), new { menuButtonId = input.Id });
        }

        /// <summary>
        /// 删除菜单按钮功能项
        /// </summary>
        /// <param name="menuButtonFunction"></param>
        /// <returns></returns>
        public Task<bool> DeleteMenuButtonFunction(SystemMenuButtonFunction menuButtonFunction)
        {
            StringBuilder sql = new StringBuilder("DELETE FROM System_MenuButtonFunction WHERE 1=1");
            if (!menuButtonFunction.FunctionId.IsEmptyGuid())
            {
                sql.Append(" AND FunctionId=@functionId");
            }
            if (!menuButtonFunction.MenuButtonId.IsEmptyGuid())
            {
                sql.Append(" AND MenuButtonId=@menuButtonId");
            }
            return SqlMapperUtil.InsertUpdateOrDeleteSqlBool<SystemUserInfo>(sql.ToString(), new
            {
                menuButtonId = menuButtonFunction.MenuButtonId,
                functionId = menuButtonFunction.FunctionId
            });
        }
    }
}