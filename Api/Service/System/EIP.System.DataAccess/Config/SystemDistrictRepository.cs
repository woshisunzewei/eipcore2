using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Core.Extensions;
using EIP.Common.Dapper;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Tree;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Config
{
    public class SystemDistrictRepository : DapperAsyncRepository<SystemDistrict>, ISystemDistrictRepository
    {
        /// <summary>
        ///     根据父级查询所有子集树形结构
        /// </summary>
        /// <param name="input">父级</param>
        /// <returns></returns>
        public Task<IEnumerable<JsTreeEntity>> GetDistrictTreeByParentId(IdInput<string> input)
        {
            const string sql = "SELECT DistrictId id,name text,ParentId parent FROM System_District WHERE ParentId=@parentId";
            return  SqlMapperUtil.SqlWithParams<JsTreeEntity>(sql, new { parentId = input.Id });
        }

        /// <summary>
        ///     根据父级查询所有子集
        /// </summary>
        /// <param name="input">父级Id</param>
        /// <returns></returns>
        public Task<IEnumerable<SystemDistrict>> GetDistrictByParentId(IdInput<string> input)
        {
            const string sql = "SELECT * FROM System_District WHERE ParentId=@parentId";
            return  SqlMapperUtil.SqlWithParams<SystemDistrict>(sql, new { parentId = input.Id });
        }

        /// <summary>
        ///     检查字典代码:唯一性检查
        /// </summary>
        /// <param name="input">唯一性检查</param>
        /// <returns></returns>
        public Task<bool> CheckDistrictId(CheckSameValueInput input)
        {
            var sql = "SELECT DistrictId FROM System_District WHERE DistrictId=@param";
            if (!input.Id.IsNullOrEmptyGuid())
            {
                sql += " AND DistrictId!=@districtId";
            }
            return SqlMapperUtil.SqlWithParamsBool<SystemDistrict>(sql, new
            {
                param = input.Param,
                districtId = input.Id
            });
        }

        /// <summary>
        ///     根据县Id获取省市县Id
        /// </summary>
        /// <param name="input">县Id</param>
        /// <returns></returns>
        public Task<SystemDistrict> GetDistrictByCountId(IdInput<string> input)
        {
            const string selectSql =
                "SELECT Id,ParentId,(SELECT ParentId FROM System_District WHERE DistrictId=dis.ParentId) ProvinceId FROM System_District dis WHERE DistrictId=@countId";
            return  SqlMapperUtil.SqlWithParamsSingle<SystemDistrict>(selectSql, new { countId = input.Id });
        }
    }
}