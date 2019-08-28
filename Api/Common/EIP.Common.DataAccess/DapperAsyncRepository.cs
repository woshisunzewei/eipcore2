using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EIP.Common.Dapper;
using EIP.Common.Entities.Paging;

namespace EIP.Common.DataAccess
{
    /// <summary>
    /// Dapper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DapperAsyncRepository<T> : IAsyncRepository<T> where T : class, new()
    {
        public Task<PagedResults<T>> PagingQueryAsync<T>(string querySql, QueryParam queryParam)
        {
            return SqlMapperUtil.PagingQueryAsync<T>(querySql, queryParam);
        }

        public Task<PagedResults<T>> PagingQueryProcAsync(QueryParam queryParam)
        {
            return SqlMapperUtil.PagingQueryProcAsync<T>(queryParam);
        }

        public Task<bool> DeleteAsync(IDbTransaction transaction = null)
        {
            return SqlMapperUtil.DeleteAsync(transaction);
        }

        public T Find(IDbTransaction transaction = null)
        {
            return Find(null, transaction);
        }

        public T Find(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null)
        {
            return SqlMapperUtil.FindAll(predicate, transaction).FirstOrDefault();
        }

        public T Find< TChild1>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, IDbTransaction transaction = null) 
        {
            return Find<TChild1, DontMap, DontMap, DontMap, DontMap, DontMap>(predicate, tChild1, null, null, null, null, null, transaction);
        }

        public T Find< TChild1, TChild2>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null) 
        {
            return Find<TChild1, TChild2, DontMap, DontMap, DontMap, DontMap>(predicate, tChild1, tChild2, null, null, null, null, transaction);
        }

        public T Find<TChild1, TChild2, TChild3>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2, Expression<Func<T, object>> tChild3,
            IDbTransaction transaction = null) 
        {
            return Find<TChild1, TChild2, TChild3, DontMap, DontMap, DontMap>(predicate, tChild1, tChild2, tChild3, null, null, null, transaction);
        }

        public T Find<TChild1, TChild2, TChild3, TChild4>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression< Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, IDbTransaction transaction = null) 
        {
            return Find<TChild1, TChild2, TChild3, TChild4, DontMap, DontMap>(predicate, tChild1, tChild2, tChild3, tChild4, null, null, transaction);
        }

        public T Find< TChild1, TChild2, TChild3, TChild4, TChild5>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5, IDbTransaction transaction = null)
        {
            return Find<TChild1, TChild2, TChild3, TChild4, TChild5, DontMap>(predicate, tChild1, tChild2, tChild3, tChild4, tChild5, null, transaction);
        }

        public T Find<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2, Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5, Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null) 
        {
            return SqlMapperUtil.Find<T, TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(predicate, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6, transaction);
        }

        public T FindById(object id, IDbTransaction transaction = null) 
        {
            return SqlMapperUtil.FindById<T>(id, transaction);
        }

        public T FindById<TChild1>(object id, Expression<Func<T, object>> tChild1, IDbTransaction transaction = null)
        {
            return FindById<TChild1, DontMap, DontMap, DontMap, DontMap, DontMap>(id, tChild1, null, null, null, null, null, transaction);
        }

        public T FindById<TChild1, TChild2>(object id, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2, IDbTransaction transaction = null)
        {
            return FindById<TChild1, TChild2, DontMap, DontMap, DontMap, DontMap>(id, tChild1, tChild2, null, null, null, null, transaction);
        }

        public T FindById<TChild1, TChild2, TChild3>(object id, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2, Expression<Func<T, object>> tChild3,
            IDbTransaction transaction = null)
        {
            return FindById<TChild1, TChild2, TChild3, DontMap, DontMap, DontMap>(id, tChild1, tChild2, tChild3, null, null, null, transaction);
        }

        public T FindById<TChild1, TChild2, TChild3, TChild4>(object id, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2, Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4, IDbTransaction transaction = null)
        {
            return FindById<TChild1, TChild2, TChild3, TChild4, DontMap, DontMap>(id, tChild1, tChild2, tChild3, tChild4, null, null, transaction);
        }

        public T FindById<TChild1, TChild2, TChild3, TChild4, TChild5>(object id, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5, IDbTransaction transaction = null)
        {
            return FindById<TChild1, TChild2, TChild3, TChild4, TChild5, DontMap>(id, tChild1, tChild2, tChild3, tChild4, tChild5, null, transaction);
        }

        public T FindById<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(object id, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5, Expression<Func<T, object>> tChild6, IDbTransaction transaction = null)
        {
            return  SqlMapperUtil.FindById<T,TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(id, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6, transaction);
        }

        public Task<T> FindByIdAsync(object id, IDbTransaction transaction = null)
        {
            return SqlMapperUtil.FindByIdAsync<T>(id, transaction);
        }

        public Task<T> FindByIdAsync<TChild1>(object id, Expression<Func<T, object>> tChild1, IDbTransaction transaction = null)
        {
            return FindByIdAsync<TChild1, DontMap, DontMap, DontMap, DontMap, DontMap>(id, tChild1, null, null, null, null, null, transaction);
        }

        public Task<T> FindByIdAsync<TChild1, TChild2>(object id, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null)
        {
            return FindByIdAsync<TChild1, TChild2, DontMap, DontMap, DontMap, DontMap>(id, tChild1, tChild2, null, null, null, null, transaction);
        }

        public Task<T> FindByIdAsync<TChild1, TChild2, TChild3>(object id, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2, 
            Expression<Func<T, object>> tChild3,
            IDbTransaction transaction = null)
        {
            return FindByIdAsync<TChild1, TChild2, TChild3, DontMap, DontMap, DontMap>(id, tChild1, tChild2, tChild3, null, null, null, transaction);
        }

        public Task<T> FindByIdAsync<TChild1, TChild2, TChild3, TChild4>(object id, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, IDbTransaction transaction = null)
        {
            return FindByIdAsync<TChild1, TChild2, TChild3, TChild4, DontMap, DontMap>(id, tChild1, tChild2, tChild3, tChild4, null, null, transaction);
        }

        public Task<T> FindByIdAsync<TChild1, TChild2, TChild3, TChild4, TChild5>(object id, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5, IDbTransaction transaction = null)
        {
            return FindByIdAsync<TChild1, TChild2, TChild3, TChild4, TChild5, DontMap>(id, tChild1, tChild2, tChild3, tChild4, tChild5, null, transaction);
        }

        public Task<T> FindByIdAsync<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(object id,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2, 
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null)
        {
            return SqlMapperUtil.FindByIdAsync<T,TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(id, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6, transaction);
        }

        public Task<T> FindAsync(IDbTransaction transaction = null)
        {
            return FindAsync(null,transaction);
        }

        public Task<T> FindAsync(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null)
        {
            return SqlMapperUtil.FindAsync(predicate, transaction);
        }

        public Task<T> FindAsync<TChild1>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, IDbTransaction transaction = null)
        {
            return FindAsync<TChild1, DontMap, DontMap, DontMap, DontMap, DontMap>(predicate, tChild1, null, null, null, null, null, transaction);
        }

        public Task<T> FindAsync<TChild1, TChild2>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null)
        {
            return FindAsync<TChild1, TChild2, DontMap, DontMap, DontMap, DontMap>(predicate, tChild1, tChild2, null, null, null, null, transaction);
        }

        public Task<T> FindAsync<TChild1, TChild2, TChild3>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, IDbTransaction transaction = null)
        {
            return FindAsync<TChild1, TChild2, TChild3, DontMap, DontMap, DontMap>(predicate, tChild1, tChild2, tChild3, null, null, null, transaction);
        }

        public Task<T> FindAsync<TChild1, TChild2, TChild3, TChild4>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, IDbTransaction transaction = null)
        {
            return FindAsync<TChild1, TChild2, TChild3, TChild4, DontMap, DontMap>(predicate, tChild1, tChild2, tChild3, tChild4, null, null, transaction);
        }

        public Task<T> FindAsync<TChild1, TChild2, TChild3, TChild4, TChild5>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2, Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5, IDbTransaction transaction = null)
        {
            return FindAsync<TChild1, TChild2, TChild3, TChild4, TChild5, DontMap>(predicate, tChild1, tChild2, tChild3, tChild4, tChild5, null, transaction);
        }

        public Task<T> FindAsync<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2, Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5, Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null)
        {
            return SqlMapperUtil.FindAsync<T,TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(predicate, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6, transaction);
        }

        public IEnumerable<T> FindAll(IDbTransaction transaction = null)
        {
            return FindAll(null, transaction);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null)
        {
            return SqlMapperUtil.FindAll(predicate, transaction);
        }

        public IEnumerable<T> FindAll<TChild1>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, IDbTransaction transaction = null)
        {
            return FindAll<TChild1, DontMap, DontMap, DontMap, DontMap, DontMap>(predicate, tChild1, null, null, null, null, null, transaction);
        }

        public IEnumerable<T> FindAll<TChild1, TChild2>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null)
        {
            return FindAll<TChild1, TChild2, DontMap, DontMap, DontMap, DontMap>(predicate, tChild1, tChild2, null, null, null, null, transaction);
        }

        public IEnumerable<T> FindAll<TChild1, TChild2, TChild3>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, IDbTransaction transaction = null)
        {
            return FindAll<TChild1, TChild2, TChild3, DontMap, DontMap, DontMap>(predicate, tChild1, tChild2, tChild3, null, null, null, transaction);
        }

        public IEnumerable<T> FindAll<TChild1, TChild2, TChild3, TChild4>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, IDbTransaction transaction = null)
        {
            return FindAll<TChild1, TChild2, TChild3, TChild4, DontMap, DontMap>(predicate, tChild1, tChild2, tChild3, tChild4, null, null, transaction);
        }

        public IEnumerable<T> FindAll<TChild1, TChild2, TChild3, TChild4, TChild5>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2, Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5, IDbTransaction transaction = null)
        {
            return FindAll<TChild1, TChild2, TChild3, TChild4, TChild5, DontMap>(predicate, tChild1, tChild2, tChild3, tChild4, tChild5, null, transaction);
        }

        public IEnumerable<T> FindAll<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null)
        {
            return SqlMapperUtil.FindAll<T,TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(predicate, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6, transaction);
        }

        public Task<IEnumerable<T>> FindAllAsync(IDbTransaction transaction = null)
        {
            return FindAllAsync(null, transaction);
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null)
        {
            return await SqlMapperUtil.FindAllAsync(predicate, transaction);
        }

        public Task<IEnumerable<T>> FindAllAsync<TChild1>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, IDbTransaction transaction = null)
        {
            return FindAllAsync<TChild1, DontMap, DontMap, DontMap, DontMap, DontMap>(predicate, tChild1, null, null, null, null, null, transaction);
        }

        public Task<IEnumerable<T>> FindAllAsync<TChild1, TChild2>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null)
        {
            return FindAllAsync<TChild1, TChild2, DontMap, DontMap, DontMap, DontMap>(predicate, tChild1, tChild2, null, null, null, null, transaction);
        }

        public Task<IEnumerable<T>> FindAllAsync<TChild1, TChild2, TChild3>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, IDbTransaction transaction = null)
        {
            return FindAllAsync<TChild1, TChild2, TChild3, DontMap, DontMap, DontMap>(predicate, tChild1, tChild2, tChild3, null, null, null, transaction);
        }

        public Task<IEnumerable<T>> FindAllAsync<TChild1, TChild2, TChild3, TChild4>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, IDbTransaction transaction = null)
        {
            return FindAllAsync<TChild1, TChild2, TChild3, TChild4, DontMap, DontMap>(predicate, tChild1, tChild2, tChild3, tChild4, null, null, transaction);
        }

        public Task<IEnumerable<T>> FindAllAsync<TChild1, TChild2, TChild3, TChild4, TChild5>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2, Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5, IDbTransaction transaction = null)
        {
            return FindAllAsync<TChild1, TChild2, TChild3, TChild4, TChild5, DontMap>(predicate, tChild1, tChild2, tChild3, tChild4, tChild5, null, transaction);
        }

        public Task<IEnumerable<T>> FindAllAsync<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2, Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5,
            Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null)
        {
           return SqlMapperUtil.FindAllAsync<T,TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(predicate, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6, transaction);
        }

        public bool Insert(T instance, IDbTransaction transaction = null)
        {
            return SqlMapperUtil.Insert(instance, transaction);
        }

        public Task<bool> InsertAsync(T instance, IDbTransaction transaction = null)
        {
            return SqlMapperUtil.InsertAsync(instance, transaction);
        }

        public int BulkInsert(IEnumerable<T> instances, IDbTransaction transaction = null)
        {
            return SqlMapperUtil.BulkInsert(instances, transaction);
        }

        public async Task<int> BulkInsertAsync(IEnumerable<T> instances, IDbTransaction transaction = null)
        {
            return await SqlMapperUtil.BulkInsertAsync(instances, transaction);
        }

        public bool Delete(T instance, IDbTransaction transaction = null)
        {
            return SqlMapperUtil.Delete(instance, transaction);
        }

        public Task<bool> DeleteAsync(T instance, IDbTransaction transaction = null)
        {
            return SqlMapperUtil.DeleteAsync(instance, transaction);
        }

        public Task<bool> DeleteByIdsAsync(string ids)
        {
            return SqlMapperUtil.DeleteByIdsAsync<T>(ids);
        }

        public bool Delete(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null)
        {
            return SqlMapperUtil.Delete(predicate, transaction);
        }

        public Task<bool> DeleteAllAsync(Expression<Func<T, bool>> predicate=null, IDbTransaction transaction = null)
        {
            return SqlMapperUtil.DeleteAllAsync(predicate, transaction);
        }

        public bool Update(T instance, IDbTransaction transaction = null)
        {
            return SqlMapperUtil.Update(instance, transaction);
        }

        public Task<bool> BulkUpdateAsync(IEnumerable<T> instances, IDbTransaction transaction = null)
        {
            return SqlMapperUtil.BulkUpdateAsync(instances, transaction);
        }

        public bool BulkUpdate(IEnumerable<T> instances, IDbTransaction transaction = null)
        {
            return SqlMapperUtil.BulkUpdate(instances, transaction);
        }

        public Task<bool> UpdateAsync(T instance, IDbTransaction transaction = null)
        {
            return SqlMapperUtil.UpdateAsync(instance, transaction);
        }

        public IEnumerable<T> FindAllBetween(object from, object to, Expression<Func<T, object>> btwField, IDbTransaction transaction = null)
        {
            return SqlMapperUtil.FindAllBetween(from, to, btwField, transaction);
        }

        public IEnumerable<T> FindAllBetween(object from, object to, Expression<Func<T, object>> btwField, Expression<Func<T, bool>> predicate = null,
            IDbTransaction transaction = null)
        {
            return SqlMapperUtil.FindAllBetween(from, to, btwField, predicate, transaction);
        }

        public IEnumerable<T> FindAllBetween(DateTime from, DateTime to, Expression<Func<T, object>> btwField, IDbTransaction transaction = null)
        {
            return SqlMapperUtil.FindAllBetween(from, to, btwField,transaction);
        }

        public IEnumerable<T> FindAllBetween(DateTime from, DateTime to, Expression<Func<T, object>> btwField, Expression<Func<T, bool>> predicate,
            IDbTransaction transaction = null)
        {
            return SqlMapperUtil.FindAllBetween(from, to, btwField, predicate, transaction);
        }

        public Task<IEnumerable<T>> FindAllBetweenAsync(object from, object to, Expression<Func<T, object>> btwField, IDbTransaction transaction = null)
        {
            return SqlMapperUtil.FindAllBetweenAsync(from, to, btwField,  transaction);
        }

        public Task<IEnumerable<T>> FindAllBetweenAsync(object from, object to, Expression<Func<T, object>> btwField, Expression<Func<T, bool>> predicate,
            IDbTransaction transaction = null)
        {
            return SqlMapperUtil.FindAllBetweenAsync(from, to, btwField, predicate, transaction);
        }

        public Task<IEnumerable<T>> FindAllBetweenAsync(DateTime from, DateTime to, Expression<Func<T, object>> btwField, IDbTransaction transaction = null)
        {
            return SqlMapperUtil.FindAllBetweenAsync(from, to, btwField, transaction);
        }

        public Task<IEnumerable<T>> FindAllBetweenAsync(DateTime @from, DateTime to, Expression<Func<T, object>> btwField, Expression<Func<T, bool>> predicate,
            IDbTransaction transaction = null)
        {
            return SqlMapperUtil.FindAllBetweenAsync(from, to, btwField, predicate, transaction);
        }

       
    }
}