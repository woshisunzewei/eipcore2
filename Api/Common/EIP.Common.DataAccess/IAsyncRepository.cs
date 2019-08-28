using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EIP.Common.Entities.Paging;

namespace EIP.Common.DataAccess
{
    public interface IAsyncRepository<T> where T : class
    {
        #region 新增

        /// <summary>
        ///     Insert object to DB
        /// </summary>
        bool Insert(T instance, IDbTransaction transaction = null);

        /// <summary>
        ///     Insert object to DB
        /// </summary>
        Task<bool> InsertAsync(T instance, IDbTransaction transaction = null);

        /// <summary>
        ///     Bulk Insert objects to DB
        /// </summary>
        int BulkInsert(IEnumerable<T> instances, IDbTransaction transaction = null);

        /// <summary>
        ///     Bulk Insert objects to DB
        /// </summary>
        Task<int> BulkInsertAsync(IEnumerable<T> instances, IDbTransaction transaction = null);

        #endregion

        #region 删除

        /// <summary>
        ///     Delete object from DB
        /// </summary>
        bool Delete(T instance, IDbTransaction transaction = null);

        /// <summary>
        ///     Delete object from DB
        /// </summary>
        Task<bool> DeleteAsync(IDbTransaction transaction = null);

        /// <summary>
        ///     Delete object from DB
        /// </summary>
        Task<bool> DeleteAsync(T instance, IDbTransaction transaction = null);

        /// <summary>
        ///     Delete objects from DB
        /// </summary>
        bool Delete(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null);


        /// <summary>
        ///     Delete objects from DB
        /// </summary>
        Task<bool> DeleteAllAsync(Expression<Func<T, bool>> predicate = null
            , IDbTransaction transaction = null);

        /// <summary>
        ///     Delete objects from DB
        /// </summary>
        Task<bool> DeleteByIdsAsync(string ids);

        #endregion

        #region 更新

        /// <summary>
        ///     Update object in DB
        /// </summary>
        bool Update(T instance, IDbTransaction transaction = null);

        /// <summary>
        ///     Bulk Update objects in DB
        /// </summary>
        Task<bool> BulkUpdateAsync(IEnumerable<T> instances, IDbTransaction transaction = null);

        /// <summary>
        ///     Bulk Update objects in DB
        /// </summary>
        bool BulkUpdate(IEnumerable<T> instances, IDbTransaction transaction = null);

        /// <summary>
        ///     Update object in DB
        /// </summary>
        Task<bool> UpdateAsync(T instance, IDbTransaction transaction = null);

        #endregion

        #region 查询

        /// <summary>
        ///     Get first object
        /// </summary>
        T Find(IDbTransaction transaction = null);

        /// <summary>
        ///     Get first object
        /// </summary>
        T Find(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null);

        /// <summary>
        ///     Get first object with join objects
        /// </summary>
        T Find<TChild1>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get first object with join objects
        /// </summary>
        T Find<TChild1, TChild2>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get first object with join objects
        /// </summary>
        T Find<TChild1, TChild2, TChild3>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get first object with join objects
        /// </summary>
        T Find<TChild1, TChild2, TChild3, TChild4>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get first object with join objects
        /// </summary>
        T Find<TChild1, TChild2, TChild3, TChild4, TChild5>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get first object with join objects
        /// </summary>
        T Find<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get object by Id
        /// </summary>
        T FindById(object id, IDbTransaction transaction = null);

        /// <summary>
        ///     Get object by Id with join objects
        /// </summary>
        T FindById<TChild1>(object id,
            Expression<Func<T, object>> tChild1,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get object by Id with join objects
        /// </summary>
        T FindById<TChild1, TChild2>(object id,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get object by Id with join objects
        /// </summary>
        T FindById<TChild1, TChild2, TChild3>(object id,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get object by Id with join objects
        /// </summary>
        T FindById<TChild1, TChild2, TChild3, TChild4>(object id,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get object by Id with join objects
        /// </summary>
        T FindById<TChild1, TChild2, TChild3, TChild4, TChild5>(object id,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get object by Id with join objects
        /// </summary>
        T FindById<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(object id,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get object by Id
        /// </summary>
        Task<T> FindByIdAsync(object id, IDbTransaction transaction = null);

        /// <summary>
        ///     Get object by Id with join objects
        /// </summary>
        Task<T> FindByIdAsync<TChild1>(object id,
            Expression<Func<T, object>> tChild1,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get object by Id with join objects
        /// </summary>
        Task<T> FindByIdAsync<TChild1, TChild2>(object id,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get object by Id with join objects
        /// </summary>
        Task<T> FindByIdAsync<TChild1, TChild2, TChild3>(object id,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get object by Id with join objects
        /// </summary>
        Task<T> FindByIdAsync<TChild1, TChild2, TChild3, TChild4>(object id,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get object by Id with join objects
        /// </summary>
        Task<T> FindByIdAsync<TChild1, TChild2, TChild3, TChild4, TChild5>(object id,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get object by Id with join objects
        /// </summary>
        Task<T> FindByIdAsync<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(object id,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get first object
        /// </summary>
        Task<T> FindAsync(IDbTransaction transaction = null);

        /// <summary>
        ///     Get first object
        /// </summary>
        Task<T> FindAsync(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null);

        /// <summary>
        ///     Get first object with join objects
        /// </summary>
        Task<T> FindAsync<TChild1>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get first object with join objects
        /// </summary>
        Task<T> FindAsync<TChild1, TChild2>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get first object with join objects
        /// </summary>
        Task<T> FindAsync<TChild1, TChild2, TChild3>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get first object with join objects
        /// </summary>
        Task<T> FindAsync<TChild1, TChild2, TChild3, TChild4>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get first object with join objects
        /// </summary>
        Task<T> FindAsync<TChild1, TChild2, TChild3, TChild4, TChild5>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get first object with join objects
        /// </summary>
        Task<T> FindAsync<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get all objects
        /// </summary>
        IEnumerable<T> FindAll(IDbTransaction transaction = null);

        /// <summary>
        ///     Get all objects
        /// </summary>
        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null);


        /// <summary>
        ///     Get all objects with join objects
        /// </summary>
        IEnumerable<T> FindAll<TChild1>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, IDbTransaction transaction = null);


        /// <summary>
        ///     Get all objects with join objects
        /// </summary>
        IEnumerable<T> FindAll<TChild1, TChild2>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get all objects with join objects
        /// </summary>
        IEnumerable<T> FindAll<TChild1, TChild2, TChild3>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get all objects with join objects
        /// </summary>
        IEnumerable<T> FindAll<TChild1, TChild2, TChild3, TChild4>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get all objects with join objects
        /// </summary>
        IEnumerable<T> FindAll<TChild1, TChild2, TChild3, TChild4, TChild5>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get all objects with join objects
        /// </summary>
        IEnumerable<T> FindAll<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get all objects
        /// </summary>
        Task<IEnumerable<T>> FindAllAsync(IDbTransaction transaction = null);

        /// <summary>
        ///     Get all objects
        /// </summary>
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null);


        /// <summary>
        ///     Get all objects with join objects
        /// </summary>
        Task<IEnumerable<T>> FindAllAsync<TChild1>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get all objects with join objects
        /// </summary>
        Task<IEnumerable<T>> FindAllAsync<TChild1, TChild2>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null);


        /// <summary>
        ///     Get all objects with join objects
        /// </summary>
        Task<IEnumerable<T>> FindAllAsync<TChild1, TChild2, TChild3>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get all objects with join objects
        /// </summary>
        Task<IEnumerable<T>> FindAllAsync<TChild1, TChild2, TChild3, TChild4>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get all objects with join objects
        /// </summary>
        Task<IEnumerable<T>> FindAllAsync<TChild1, TChild2, TChild3, TChild4, TChild5>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get all objects with join objects
        /// </summary>
        Task<IEnumerable<T>> FindAllAsync<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null);

        /// <summary>
        ///     Get all objects with BETWEEN query
        /// </summary>
        IEnumerable<T> FindAllBetween(object from, object to, Expression<Func<T, object>> btwField, IDbTransaction transaction = null);

        /// <summary>
        ///     Get all objects with BETWEEN query
        /// </summary>
        IEnumerable<T> FindAllBetween(object from, object to, Expression<Func<T, object>> btwField, Expression<Func<T, bool>> predicate = null, IDbTransaction transaction = null);

        /// <summary>
        ///     Get all objects with BETWEEN query
        /// </summary>
        IEnumerable<T> FindAllBetween(DateTime from, DateTime to, Expression<Func<T, object>> btwField, IDbTransaction transaction = null);

        /// <summary>
        ///     Get all objects with BETWEEN query
        /// </summary>
        IEnumerable<T> FindAllBetween(DateTime from, DateTime to, Expression<Func<T, object>> btwField, Expression<Func<T, bool>> predicate, IDbTransaction transaction = null);

        /// <summary>
        ///     Get all objects with BETWEEN query
        /// </summary>
        Task<IEnumerable<T>> FindAllBetweenAsync(object from, object to, Expression<Func<T, object>> btwField, IDbTransaction transaction = null);

        /// <summary>
        ///     Get all objects with BETWEEN query
        /// </summary>
        Task<IEnumerable<T>> FindAllBetweenAsync(object from, object to, Expression<Func<T, object>> btwField, Expression<Func<T, bool>> predicate, IDbTransaction transaction = null);

        /// <summary>
        ///     Get all objects with BETWEEN query
        /// </summary>
        Task<IEnumerable<T>> FindAllBetweenAsync(DateTime from, DateTime to, Expression<Func<T, object>> btwField, IDbTransaction transaction = null);

        /// <summary>
        ///     Get all objects with BETWEEN query
        /// </summary>
        Task<IEnumerable<T>> FindAllBetweenAsync(DateTime from, DateTime to, Expression<Func<T, object>> btwField, Expression<Func<T, bool>> predicate, IDbTransaction transaction = null);

        #endregion

        #region 分页
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="querySql"></param>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        Task<PagedResults<T>> PagingQueryAsync<T>(string querySql, QueryParam queryParam);

        /// <summary>
        /// 存储过程分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        Task<PagedResults<T>> PagingQueryProcAsync(QueryParam queryParam);
        #endregion
    }
}