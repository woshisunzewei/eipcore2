using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Tree;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Config
{
    public interface ISystemDistrictLogic : IAsyncLogic<SystemDistrict>
    {
        /// <summary>
        ///     根据父级查询所有子集树形结构
        /// </summary>
        /// <param name="input">父级Id</param>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetDistrictTreeByParentId(IdInput<string> input);

        /// <summary>
        ///     根据父级查询所有子集
        /// </summary>
        /// <param name="input">父级Id</param>
        /// <returns></returns>
        Task<IEnumerable<SystemDistrict>> GetDistrictByParentId(IdInput<string> input);

        /// <summary>
        ///     根据县Id获取省市县Id
        /// </summary>
        /// <param name="input">县Id</param>
        /// <returns></returns>
        Task<SystemDistrict> GetDistrictByCountId(IdInput<string> input);

        /// <summary>
        ///     检测代码是否已经具有重复项
        /// </summary>
        /// <param name="input">需要验证的参数</param>
        /// <returns></returns>
        Task<OperateStatus> CheckDistrictId(CheckSameValueInput input);

        /// <summary>
        ///     保存省市县信息
        /// </summary>
        /// <param name="systemDistrict">省市县信息</param>
        /// <returns></returns>
        Task<OperateStatus> SaveDistrict(SystemDistrict systemDistrict);

        /// <summary>
        ///     删除省市县及下级数据
        /// </summary>
        /// <param name="input">父级id</param>
        /// <returns></returns>
        Task<OperateStatus> DeleteDistrict(IdInput<string> input);
    }
}