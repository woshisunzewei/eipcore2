using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Tree;
using EIP.System.Models.Dtos.Config;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Config
{
    /// <summary>
    /// 字典数据访问接口
    /// </summary>
    public interface ISystemDictionaryRepository : IAsyncRepository<SystemDictionary>
    {
        /// <summary>
        ///     根据所有字段信息
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetDictionaryTree();

        /// <summary>
        ///     根据父级查询下级
        /// </summary>
        /// <param name="input">父级id</param>
        /// <returns></returns>
        Task<IEnumerable<SystemDictionaryGetByParentIdOutput>> GetDictionariesParentId(SystemDictionaryGetByParentIdInput input);
        
        /// <summary>
        ///   根据ParentId获取所有下级
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetDictionaryTreeByParentIds(IdInput input);
        
    }
}