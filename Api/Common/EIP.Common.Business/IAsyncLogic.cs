using EIP.Common.Entities;
using EIP.Common.Entities.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace EIP.Common.Business
{
    public interface IAsyncLogic<T> where T : class
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">新增实体</param>
        /// <returns></returns>
        Task<OperateStatus> InsertAsync(T entity);

        /// <summary>
        ///     插入可返回自增id值
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        Task<OperateStatus> InsertScalarAsync(T entity);

        /// <summary>
        /// Dapper批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<OperateStatus> InsertMultipleDapperAsync(IEnumerable<T> list);

        /// <summary>
        /// SqlBulkCopy批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<OperateStatus> InsertMultipleAsync(IEnumerable<T> list);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="current">更新实体</param>
        /// <returns></returns>
        Task<OperateStatus> UpdateAsync(T current);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<OperateStatus> DeleteAsync(T entity);
        
        /// <summary>
        /// 删除所有
        /// </summary>
        /// <returns></returns>
        Task<OperateStatus> DeleteAllAsync();

        /// <summary>
        /// 删除所有
        /// </summary>
        /// <returns></returns>
        Task<OperateStatus> DeleteByIdsAsync(string ids);

        /// <summary>
        /// 获取集合数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllEnumerableAsync();

        /// <summary>
        /// 根据主键获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(object id);

        /// <summary>
        /// 存储过程分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        Task<PagedResults<T>> PagingQueryProcAsync(QueryParam queryParam);
    }
}