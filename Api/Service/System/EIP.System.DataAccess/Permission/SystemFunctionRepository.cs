using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Dapper;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Permission
{
    public class SystemFunctionRepository : DapperAsyncRepository<SystemFunction>, ISystemFunctionRepository
    {

        /// <summary>
        /// 根据系统代码获取功能项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<IEnumerable<SystemFunction>> GetSystemFunctionsByAppCode(IdInput<string> input = null)
        {
            const string sql = "SELECT * FROM System_Function WHERE AppCode=@appCode ORDER BY Area,Controller,IsPage DESC";
            return input == null ? 
                SqlMapperUtil.SqlWithParams<SystemFunction>(sql) : 
                SqlMapperUtil.SqlWithParams<SystemFunction>(sql, new { appCode = input.Id });
        }
    }
}