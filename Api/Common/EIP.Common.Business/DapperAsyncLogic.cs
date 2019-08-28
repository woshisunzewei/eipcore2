using EIP.Common.Core.Extensions;
using EIP.Common.Core.Resource;
using EIP.Common.DataAccess;
using EIP.Common.Entities;
using EIP.Common.Entities.Paging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EIP.Common.Business
{
    public class DapperAsyncLogic<T> : IAsyncLogic<T> where T : class, new()
    {
        public DapperAsyncLogic() { }

        public IAsyncRepository<T> Repository;

        public DapperAsyncLogic(IAsyncRepository<T> repository)
        {
            Repository = repository ?? throw new ArgumentNullException("repository", "repository为空");
        }

        public async Task<OperateStatus> InsertAsync(T entity)
        {
            var operateStatus = new OperateStatus();
            try
            {
                var resultNum = await Repository.InsertAsync(entity);
                operateStatus.ResultSign = resultNum ? ResultSign.Successful : ResultSign.Error;
                operateStatus.Message = resultNum  ? Chs.Successful : Chs.Error;
            }
            catch (Exception exception)
            {
                operateStatus.Message = string.Format(Chs.Error, exception.Message);
            }
            return operateStatus;
        }

        public async Task<OperateStatus> InsertScalarAsync(T entity)
        {
            var operateStatus = new OperateStatus<int>();
            try
            {
                var resultNum = await Repository.InsertAsync(entity);
                operateStatus.ResultSign = resultNum  ? ResultSign.Successful : ResultSign.Error;
                operateStatus.Message = resultNum ? Chs.Successful : Chs.Error;
            }
            catch (Exception exception)
            {
                operateStatus.Message = string.Format(Chs.Error, exception.Message);
            }
            return operateStatus;
        }

        public async Task<OperateStatus> InsertMultipleDapperAsync(IEnumerable<T> list)
        {
            var operateStatus = new OperateStatus();
            try
            {
                var resultNum = await Repository.BulkInsertAsync(list);
                operateStatus.ResultSign = resultNum > 0 ? ResultSign.Successful : ResultSign.Error;
                operateStatus.Message = resultNum > 0 ? Chs.Successful : Chs.Error;
            }
            catch (Exception exception)
            {
                operateStatus.Message = string.Format(Chs.Error, exception.Message);
            }
            return operateStatus;
        }

        public async Task<OperateStatus> InsertMultipleAsync(IEnumerable<T> list)
        {
            var operateStatus = new OperateStatus();
            try
            {
                var resultNum = await Repository.BulkInsertAsync(list);
                operateStatus.ResultSign = resultNum > 0 ? ResultSign.Successful : ResultSign.Error;
                operateStatus.Message = resultNum > 0 ? Chs.Successful : Chs.Error;
            }
            catch (Exception exception)
            {
                operateStatus.Message = string.Format(Chs.Error, exception.Message);
            }
            return operateStatus;
        }

        public async Task<OperateStatus> UpdateAsync(T current)
        {
            var operateStatus = new OperateStatus();
            try
            {
                var resultNum = await Repository.UpdateAsync(current);
                operateStatus.ResultSign = resultNum ? ResultSign.Successful : ResultSign.Error;
                operateStatus.Message = resultNum  ? Chs.Successful : Chs.Error;
            }
            catch (Exception exception)
            {
                operateStatus.Message = string.Format(Chs.Error, exception.Message);
            }
            return operateStatus;
        }

        public async Task<OperateStatus> DeleteAsync(T entity)
        {
            var operateStatus = new OperateStatus();
            try
            {
                var resultNum = await Repository.DeleteAsync(entity);
                operateStatus.ResultSign = resultNum? ResultSign.Successful : ResultSign.Error;
                operateStatus.Message = resultNum ? Chs.Successful : Chs.Error;
            }
            catch (Exception exception)
            {
                operateStatus.Message = string.Format(Chs.Error, exception.Message);
            }
            return operateStatus;
        }

        public async Task<OperateStatus> DeleteAllAsync()
        {
            var operateStatus = new OperateStatus();
            try
            {   var resultNum = await Repository.DeleteAllAsync();
                operateStatus.ResultSign = resultNum ? ResultSign.Successful : ResultSign.Error;
                operateStatus.Message = resultNum ? Chs.Successful : Chs.Error;
            }
            catch (Exception exception)
            {
                operateStatus.Message = string.Format(Chs.Error, exception.Message);
            }
            return operateStatus;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<OperateStatus> DeleteByIdsAsync(string ids)
        {
            var operateStatus = new OperateStatus();
            try
            {
                ids = ids.InSql();
                var resultNum = await Repository.DeleteByIdsAsync(ids);
                operateStatus.ResultSign = resultNum ? ResultSign.Successful : ResultSign.Error;
                operateStatus.Message = resultNum ? Chs.Successful : Chs.Error;
            }
            catch (Exception exception)
            {
                operateStatus.Message = string.Format(Chs.Error, exception.Message);
            }
            return operateStatus;
        }

        public Task<IEnumerable<T>> GetAllEnumerableAsync()
        {
            return Repository.FindAllAsync();
        }

        public Task<T> GetByIdAsync(object id)
        {
            return Repository.FindByIdAsync(id);
        }

        public Task<PagedResults<T>> PagingQueryProcAsync(QueryParam queryParam)
        {
            return Repository.PagingQueryProcAsync(queryParam);
        }

    }
}