using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Tree;
using EIP.System.Models.Dtos.Config;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Config
{
    /// <summary>
    ///     系统字典业务逻辑接口
    /// </summary>
    public interface ISystemDictionaryLogic : IAsyncLogic<SystemDictionary>
    {
        /// <summary>
        ///     查询所有字典信息:Ztree格式
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetDictionaryTree();

        /// <summary>
        /// 根据父级id获取下级(只有一级)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemDictionary>> GetDictionarieByParentId(IdInput input);

        /// <summary>
        /// 根据父级查询下级(所有下级)
        /// </summary>
        /// <param name="input">父级id</param>
        /// <returns></returns>
        Task<IEnumerable<SystemDictionaryGetByParentIdOutput>> GetDictionariesParentId(SystemDictionaryGetByParentIdInput input);

        /// <summary>
        /// 根据ParentIds获取所有下级
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetDictionaryTreeByParentIds(IdInput input);

        /// <summary>
        ///     保存字典信息
        /// </summary>
        /// <param name="dictionary">字典信息</param>
        /// <returns></returns>
        Task<OperateStatus> SaveDictionary(SystemDictionary dictionary);

        /// <summary>
        ///     删除字典及下级数据
        /// </summary>
        /// <param name="input">父级id</param>
        /// <returns></returns>
        Task<OperateStatus> DeleteDictionary(IdInput<string> input);

        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SystemDictionaryEditOutput> GetById(IdInput input);

        /// <summary>
        /// 从MongoDb获取字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetMongoDbDictionaryTreeByParentIds(IdInput input);
    }
}