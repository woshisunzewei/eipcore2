using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EIP.Common.Entities.Paging;

namespace EIP.Common.DataAccess
{
    /// <summary>
    /// Ef
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EfAsyncRepository<T> : IAsyncRepository<T> where T : class, new()
    {
        public T Find(IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public T Find(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public T Find<TChild1>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public T Find<TChild1, TChild2>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public T Find<TChild1, TChild2, TChild3>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2, Expression<Func<T, object>> tChild3,
            IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public T Find<TChild1, TChild2, TChild3, TChild4>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public T Find<TChild1, TChild2, TChild3, TChild4, TChild5>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public T Find<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2, Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5, Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public T FindById(object id, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public T FindById<TChild1>(object id, Expression<Func<T, object>> tChild1, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public T FindById<TChild1, TChild2>(object id, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public T FindById<TChild1, TChild2, TChild3>(object id, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2, Expression<Func<T, object>> tChild3,
            IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public T FindById<TChild1, TChild2, TChild3, TChild4>(object id, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2, Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public T FindById<TChild1, TChild2, TChild3, TChild4, TChild5>(object id, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public T FindById<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(object id, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5, Expression<Func<T, object>> tChild6, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByIdAsync(object id, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByIdAsync<TChild1>(object id, Expression<Func<T, object>> tChild1, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByIdAsync<TChild1, TChild2>(object id, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByIdAsync<TChild1, TChild2, TChild3>(object id, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2, Expression<Func<T, object>> tChild3,
            IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByIdAsync<TChild1, TChild2, TChild3, TChild4>(object id, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByIdAsync<TChild1, TChild2, TChild3, TChild4, TChild5>(object id, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByIdAsync<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(object id, Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2, Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5, Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync(IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync<TChild1>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync<TChild1, TChild2>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync<TChild1, TChild2, TChild3>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync<TChild1, TChild2, TChild3, TChild4>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync<TChild1, TChild2, TChild3, TChild4, TChild5>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2, Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2, Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5, Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAll(IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAll<TChild1>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAll<TChild1, TChild2>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAll<TChild1, TChild2, TChild3>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAll<TChild1, TChild2, TChild3, TChild4>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAll<TChild1, TChild2, TChild3, TChild4, TChild5>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2, Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAll<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2, Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5, Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAllAsync(IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAllAsync<TChild1>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAllAsync<TChild1, TChild2>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAllAsync<TChild1, TChild2, TChild3>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAllAsync<TChild1, TChild2, TChild3, TChild4>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAllAsync<TChild1, TChild2, TChild3, TChild4, TChild5>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2, Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAllAsync<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2, Expression<Func<T, object>> tChild3, Expression<Func<T, object>> tChild4, Expression<Func<T, object>> tChild5, Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public bool Insert(T instance, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(T instance, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public int BulkInsert(IEnumerable<T> instances, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<int> BulkInsertAsync(IEnumerable<T> instances, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public bool Delete(T instance, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(T instance, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public bool Update(T instance, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> BulkUpdateAsync(IEnumerable<T> instances, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public bool BulkUpdate(IEnumerable<T> instances, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(T instance, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAllBetween(object @from, object to, Expression<Func<T, object>> btwField, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAllBetween(object @from, object to, Expression<Func<T, object>> btwField, Expression<Func<T, bool>> predicate = null,
            IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAllBetween(DateTime @from, DateTime to, Expression<Func<T, object>> btwField, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAllBetween(DateTime @from, DateTime to, Expression<Func<T, object>> btwField, Expression<Func<T, bool>> predicate,
            IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAllBetweenAsync(object @from, object to, Expression<Func<T, object>> btwField, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAllBetweenAsync(object @from, object to, Expression<Func<T, object>> btwField, Expression<Func<T, bool>> predicate,
            IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAllBetweenAsync(DateTime @from, DateTime to, Expression<Func<T, object>> btwField, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAllBetweenAsync(DateTime @from, DateTime to, Expression<Func<T, object>> btwField, Expression<Func<T, bool>> predicate,
            IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResults<T1>> PagingQueryAsync<T1>(string querySql, QueryParam queryParam)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResults<T>> PagingQueryProcAsync(QueryParam queryParam)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdsAsync(string ids)
        {
            throw new NotImplementedException();
        }
    }
}