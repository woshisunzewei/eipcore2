using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Entities.Paging;
using MongoDB.Driver;

namespace EIP.Common.DataAccess
{
    public interface IAsyncMongoDbRepository<T> where T : class
    {
        bool Insert(T t);

        Task<bool> InsertAsync(T t);

        bool BulkInsert(IEnumerable<T> t);

        Task<bool> BulkInsertAsync(IEnumerable<T> t);

        UpdateResult Update(T t, string id);

        Task<UpdateResult> UpdateAsync(T t, string id);

        UpdateResult UpdateManay(Dictionary<string, string> dic, FilterDefinition<T> filter);

        Task<UpdateResult> UpdateManayAsync(Dictionary<string, string> dic, FilterDefinition<T> filter);

        DeleteResult Delete(string id);

        Task<DeleteResult> DeleteAsync(string id);

        DeleteResult DeleteMany(FilterDefinition<T> filter);

        Task<DeleteResult> DeleteManyAsync(FilterDefinition<T> filter);

        long Count(FilterDefinition<T> filter);

        Task<long> CountAsync(FilterDefinition<T> filter);

        T FindOne(string id, string[] field = null);

        Task<T> FindOneAsync(string id, string[] field = null);

        IEnumerable<T> GetAllEnumerable(FilterDefinition<T> filter = null, string[] field = null, SortDefinition<T> sort = null);

        Task<IEnumerable<T>> GetAllEnumerableAsync(FilterDefinition<T> filter = null, string[] field = null, SortDefinition<T> sort = null);

        PagedResults<T> FindListByPage(FilterDefinition<T> filter, int pageIndex, int pageSize,
            string[] field = null, SortDefinition<T> sort = null);

        Task<PagedResults<T>> FindListByPageAsync(FilterDefinition<T> filter, int pageIndex, int pageSize,
            string[] field = null, SortDefinition<T> sort = null);
    }
}