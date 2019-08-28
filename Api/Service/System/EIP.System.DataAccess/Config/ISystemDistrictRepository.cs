using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Tree;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Config
{
    public interface ISystemDistrictRepository : IAsyncRepository<SystemDistrict>
    {
        /// <summary>
        ///     根据父级查询所有子集树形结构
        /// </summary>
        /// <param name="input">父级</param>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetDistrictTreeByParentId(IdInput<string> input);

        /// <summary>
        ///     根据父级查询所有子集
        /// </summary>
        /// <param name="input">父级Id</param>
        /// <returns></returns>
        Task<IEnumerable<SystemDistrict>> GetDistrictByParentId(IdInput<string> input);

        /// <summary>
        ///     检查字典代码:唯一性检查
        /// </summary>
        /// <param name="input">代码</param>
        /// <returns></returns>
        Task<bool> CheckDistrictId(CheckSameValueInput input);

        /// <summary>
        ///     根据县Id获取省市县Id
        /// </summary>
        /// <param name="input">县Id</param>
        /// <returns></returns>
        Task<SystemDistrict> GetDistrictByCountId(IdInput<string> input);
    }
}