using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Core.Extensions;
using EIP.Common.Core.Resource;
using EIP.Common.Core.Utils;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Tree;
using EIP.System.DataAccess.Config;
using EIP.System.Models.Dtos.Config;
using EIP.System.Models.Entities;
using EIP.System.Models.Resx;
using MongoDB.Driver;

namespace EIP.System.Business.Config
{
    /// <summary>
    ///     字典业务逻辑实现
    /// </summary>
    public class SystemDictionaryLogic : DapperAsyncLogic<SystemDictionary>, ISystemDictionaryLogic
    {
        #region 构造函数

        public SystemDictionaryLogic()
        {
            _dictionaryRepository = new SystemDictionaryRepository();
        }
        private readonly ISystemDictionaryMongoDbRepository _dictionaryMongoDbRepository;
        private readonly ISystemDictionaryRepository _dictionaryRepository;

        public SystemDictionaryLogic(ISystemDictionaryRepository dictionaryRepository, ISystemDictionaryMongoDbRepository dictionaryMongoDbRepository)
            : base(dictionaryRepository)
        {
            _dictionaryRepository = dictionaryRepository;
            _dictionaryMongoDbRepository = dictionaryMongoDbRepository;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     查询所有字典信息:Ztree格式
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<JsTreeEntity>> GetDictionaryTree()
        {
            var allDics = (await GetAllEnumerableAsync()).ToList();
            var list = new List<FilterDefinition<SystemDictionary>>
            {
                Builders<SystemDictionary>.Filter.Lt("CreateTime", DateTime.Now)
            };
            var filter = Builders<SystemDictionary>.Filter.And(list);
            //删除所有
            await _dictionaryMongoDbRepository.DeleteManyAsync(filter);
            //新增所有
            _dictionaryMongoDbRepository.BulkInsertAsync(allDics);
            return await _dictionaryRepository.GetDictionaryTree();
        }

        /// <summary>
        ///     查询所有字典信息
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<JsTreeEntity>> GetMongoDbDictionaryTreeByParentIds(IdInput input)
        {
            IList<JsTreeEntity> treeEntities = new List<JsTreeEntity>();
            var builder = Builders<SystemDictionary>.Filter;
            var filter = builder.Where(w=>w.ParentIds.Contains(input.Id.ToString()));
            var datas = await _dictionaryMongoDbRepository.GetAllEnumerableAsync(filter);
            foreach (var data in datas)
            {
                treeEntities.Add(new JsTreeEntity
                {
                    id = data.DictionaryId,
                    parent = data.DictionaryId == input.Id ? "#" : data.ParentId.ToString(),
                    text = data.Name,
                    state = new JsTreeStateEntity()
                });
            }
            return treeEntities;
        }

        /// <summary>
        ///    根据父级id获取下级(只有一级)
        /// </summary>
        /// <param name="input">父级id</param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemDictionary>> GetDictionarieByParentId(IdInput input)
        {
            return (await _dictionaryRepository.FindAllAsync(f => f.ParentId == input.Id)).OrderBy(o => o.OrderNo);
        }

        /// <summary>
        /// 根据父级查询下级(所有下级)
        /// </summary>
        /// <param name="input">父级id</param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemDictionaryGetByParentIdOutput>> GetDictionariesParentId(SystemDictionaryGetByParentIdInput input)
        {
            var allDics = (await GetAllEnumerableAsync()).ToList();
            var dics = (await _dictionaryRepository.GetDictionariesParentId(input)).ToList();
            foreach (var dic in dics)
            {
                foreach (var parent in dic.ParentIds.Split(','))
                {
                    //查找上级
                    var dicinfo = allDics.FirstOrDefault(w => w.DictionaryId.ToString() == parent);
                    if (dicinfo != null) dic.ParentNames += dicinfo.Name + ">";
                }
                if (!dic.ParentNames.IsNullOrEmpty())
                    dic.ParentNames = dic.ParentNames.TrimEnd('>');
            }
            return dics.OrderBy(o => o.ParentNames);
        }

        /// <summary>
        ///     保存字典信息
        /// </summary>
        /// <param name="dictionary">字典信息</param>
        /// <returns></returns>
        public async Task<OperateStatus> SaveDictionary(SystemDictionary dictionary)
        {
            OperateStatus operateStatus;
            if (dictionary.DictionaryId.IsEmptyGuid())
            {
                dictionary.CreateTime = DateTime.Now;
                dictionary.CanbeDelete = true;
                dictionary.DictionaryId = CombUtil.NewComb();
                operateStatus = await InsertAsync(dictionary);
            }
            else
            {
                dictionary.UpdateTime = DateTime.Now;
                dictionary.UpdateUserId = dictionary.CreateUserId;
                dictionary.UpdateUserName = dictionary.CreateUserName;
                var dic = await GetByIdAsync(dictionary.DictionaryId);
                dictionary.CanbeDelete = dic.CanbeDelete;
                dictionary.CreateTime = dic.CreateTime;
                dictionary.CreateUserId = dic.CreateUserId;
                dictionary.CreateUserName = dic.CreateUserName;
                operateStatus = await UpdateAsync(dictionary);
            }
            GeneratingParentIds();
            return operateStatus;
        }

        /// <summary>
        ///     删除字典及下级数据
        /// </summary>
        /// <param name="input">id</param>
        /// <returns></returns>
        public async Task<OperateStatus> DeleteDictionary(IdInput<string> input)
        {
            var operateStatus = new OperateStatus();
            if (input.Id.IsNullOrEmpty())
            {
                return operateStatus;
            }
            foreach (var id in input.Id.Split(','))
            {
                //判断该字典是否允许删除:可能是系统定义的字典则不允许删除
                var dictionary = await GetByIdAsync(id);
                if (!dictionary.CanbeDelete)
                {
                    operateStatus.ResultSign = ResultSign.Error;
                    operateStatus.Message = Chs.CanotDelete;
                    return operateStatus;
                }
                //是否具有子项
                IEnumerable<SystemDictionary> dictionaries = await GetDictionarieByParentId(new IdInput(Guid.Parse(id)));
                if (dictionaries.Any())
                {
                    operateStatus.ResultSign = ResultSign.Error;
                    operateStatus.Message = ResourceSystem.具有下级项;
                    return operateStatus;
                }
            }
            foreach (var id in input.Id.Split(','))
            {
                try
                {
                    operateStatus = await DeleteAsync(new SystemDictionary { DictionaryId = Guid.Parse(id) });
                    if (operateStatus.ResultSign == ResultSign.Error)
                    {
                        return operateStatus;
                    }
                }
                catch (Exception e)
                {
                    operateStatus.Message = e.Message;
                    return operateStatus;
                }
            }
            operateStatus.ResultSign = ResultSign.Successful;
            operateStatus.Message = Chs.Successful;
            return operateStatus;
        }

        /// <summary>
        ///     根据ParentIds获取所有下级
        /// </summary>
        /// <param name="input">代码值</param>
        /// <returns></returns>
        public async Task<IEnumerable<JsTreeEntity>> GetDictionaryTreeByParentIds(IdInput input)
        {
            return await _dictionaryRepository.GetDictionaryTreeByParentIds(input);
        }

        #endregion

        /// <summary>
        ///     批量生成代码
        /// </summary>
        /// <returns></returns>
        public async Task<OperateStatus> GeneratingParentIds()
        {
            OperateStatus operateStatus = new OperateStatus();
            try
            {
                //获取所有字典树
                var dics = (await GetAllEnumerableAsync()).ToList();

                var topDics = dics.Where(w => w.ParentId == Guid.Empty);
                foreach (var dic in topDics)
                {
                    dic.ParentIds = dic.DictionaryId.ToString();
                    await UpdateAsync(dic);
                    await GeneratingParentIds(dic, dics.ToList(), "");
                }
            }
            catch (Exception ex)
            {
                operateStatus.Message = ex.Message;
                return operateStatus;
            }
            operateStatus.Message = Chs.Successful;
            operateStatus.ResultSign = ResultSign.Successful;
            return operateStatus;
        }

        /// <summary>
        /// 递归获取代码
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="dictionaries"></param>
        /// <param name="dicId"></param>
        private async Task GeneratingParentIds(SystemDictionary dictionary, IList<SystemDictionary> dictionaries, string dicId)
        {
            string parentIds = dictionary.DictionaryId.ToString();
            //获取下级
            var nextDic = dictionaries.Where(w => w.ParentId == dictionary.DictionaryId).ToList();
            if (nextDic.Any())
            {
                parentIds = dicId.IsNullOrEmpty() ? parentIds : dicId + "," + parentIds;
            }
            foreach (var dic in nextDic)
            {
                dic.ParentIds = parentIds + "," + dic.DictionaryId;
                await UpdateAsync(dic);
                await GeneratingParentIds(dic, dictionaries, parentIds);
            }
        }

        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SystemDictionaryEditOutput> GetById(IdInput input)
        {
            SystemDictionaryEditOutput output = (await GetByIdAsync(input.Id)).MapTo<SystemDictionaryEditOutput>();
            //获取父级信息
            var parentInfo = await GetByIdAsync(output.ParentId);
            if (parentInfo != null)
            {
                output.ParentName = parentInfo.Name;
            }
            return output;
        }
    }
}