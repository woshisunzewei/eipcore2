using EIP.Common.Core.Utils;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using EIP.Common.Entities.CustomAttributes;
using EIP.Common.Entities.Paging;

namespace EIP.Common.DataAccess
{
    /// <summary>
    /// MongoDb数据库访问
    /// mongod --config "D:\Program Files\MongoDB\Server\3.2\mongo.conf" --install --serviceName "MongoDB"  
    /// net start MongoDB
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MongoDbAsyncRepository<T> : IAsyncMongoDbRepository<T> where T : class, new()
    {
        /// <summary>
        /// 获取mongodb实例
        /// </summary>
        /// <returns></returns>
        public IMongoCollection<T> MongodbInfoClient()
        {
            var client = new MongoClient(ConfigurationUtil.GetSection("MongoDb:ConnectionString"));
            Type t = typeof(T);
            string dbName = "EIP";
            DbAttribute m = t.GetTypeInfo().GetCustomAttribute<DbAttribute>();
            if (m != null)
            {
                dbName = m.Name;
            }
            var dataBase = client.GetDatabase(dbName);
            return dataBase.GetCollection<T>(typeof(T).Name);
        }

        #region Add 添加一条数据
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="t">添加的实体</param>
        /// <returns></returns>
        public bool Insert(T t)
        {
            try
            {
                var client = MongodbInfoClient();
                client.InsertOne(t);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region AddAsync 异步添加一条数据
        /// <summary>
        /// 异步添加一条数据
        /// </summary>
        /// <param name="t">添加的实体</param>

        /// <returns></returns>
        public async Task<bool> InsertAsync(T t)
        {
            try
            {
                var client = MongodbInfoClient();
                await client.InsertOneAsync(t);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region InsertMany 批量插入
        /// <summary>
        /// 批量插入
        /// </summary>

        /// <param name="t">实体集合</param>
        /// <returns></returns>
        public bool BulkInsert(IEnumerable<T> t)
        {
            try
            {
                var client = MongodbInfoClient();
                client.InsertMany(t);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region InsertManyAsync 异步批量插入
        /// <summary>
        /// 异步批量插入
        /// </summary>
        /// <param name="t">实体集合</param>
        /// <returns></returns>
        public async Task<bool> BulkInsertAsync(IEnumerable<T> t)
        {
            try
            {
                var client = MongodbInfoClient();
                await client.InsertManyAsync(t);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Update 修改一条数据
        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="t">添加的实体</param>

        /// <returns></returns>
        public UpdateResult Update(T t, string id)
        {
            try
            {
                var client = MongodbInfoClient();
                //修改条件
                FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
                //要修改的字段
                var list = new List<UpdateDefinition<T>>();
                foreach (var item in t.GetType().GetProperties())
                {
                    if (item.Name.ToLower() == "id") continue;
                    list.Add(Builders<T>.Update.Set(item.Name, item.GetValue(t)));
                }
                var updatefilter = Builders<T>.Update.Combine(list);
                return client.UpdateOne(filter, updatefilter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region UpdateAsync 异步修改一条数据
        /// <summary>
        /// 异步修改一条数据
        /// </summary>
        /// <param name="t">添加的实体</param>

        /// <returns></returns>
        public async Task<UpdateResult> UpdateAsync(T t, string id)
        {
            try
            {
                var client = MongodbInfoClient();
                //修改条件
                FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
                //要修改的字段
                var list = new List<UpdateDefinition<T>>();
                foreach (var item in t.GetType().GetProperties())
                {
                    if (item.Name.ToLower() == "id") continue;
                    list.Add(Builders<T>.Update.Set(item.Name, item.GetValue(t)));
                }
                var updatefilter = Builders<T>.Update.Combine(list);
                return await client.UpdateOneAsync(filter, updatefilter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region UpdateManay 批量修改数据
        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="dic">要修改的字段</param>

        /// <param name="filter">修改条件</param>
        /// <returns></returns>
        public UpdateResult UpdateManay(Dictionary<string, string> dic, FilterDefinition<T> filter)
        {
            try
            {
                var client = MongodbInfoClient();
                T t = new T();
                //要修改的字段
                var list = new List<UpdateDefinition<T>>();
                foreach (var item in t.GetType().GetProperties())
                {
                    if (!dic.ContainsKey(item.Name)) continue;
                    var value = dic[item.Name];
                    list.Add(Builders<T>.Update.Set(item.Name, value));
                }
                var updatefilter = Builders<T>.Update.Combine(list);
                return client.UpdateMany(filter, updatefilter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region UpdateManayAsync 异步批量修改数据
        /// <summary>
        /// 异步批量修改数据
        /// </summary>
        /// <param name="dic">要修改的字段</param>

        /// <param name="filter">修改条件</param>
        /// <returns></returns>
        public async Task<UpdateResult> UpdateManayAsync(Dictionary<string, string> dic, FilterDefinition<T> filter)
        {
            try
            {
                var client = MongodbInfoClient();
                T t = new T();
                //要修改的字段
                var list = new List<UpdateDefinition<T>>();
                foreach (var item in t.GetType().GetProperties())
                {
                    if (!dic.ContainsKey(item.Name)) continue;
                    var value = dic[item.Name];
                    list.Add(Builders<T>.Update.Set(item.Name, value));
                }
                var updatefilter = Builders<T>.Update.Combine(list);
                return await client.UpdateManyAsync(filter, updatefilter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete 删除一条数据
        /// <summary>
        /// 删除一条数据
        /// </summary>

        /// <param name="id">objectId</param>
        /// <returns></returns>
        public DeleteResult Delete(string id)
        {
            try
            {
                var client = MongodbInfoClient();
                FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
                return client.DeleteOne(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region DeleteAsync 异步删除一条数据
        /// <summary>
        /// 异步删除一条数据
        /// </summary>

        /// <param name="id">objectId</param>
        /// <returns></returns>
        public async Task<DeleteResult> DeleteAsync(string id)
        {
            try
            {
                var client = MongodbInfoClient();
                //修改条件
                FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
                return await client.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region DeleteMany 删除多条数据
        /// <summary>
        /// 删除一条数据
        /// </summary>

        /// <param name="filter">删除的条件</param>
        /// <returns></returns>
        public DeleteResult DeleteMany(FilterDefinition<T> filter)
        {
            try
            {
                var client = MongodbInfoClient();
                return client.DeleteMany(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region DeleteManyAsync 异步删除多条数据
        /// <summary>
        /// 异步删除多条数据
        /// </summary>

        /// <param name="filter">删除的条件</param>
        /// <returns></returns>
        public async Task<DeleteResult> DeleteManyAsync(FilterDefinition<T> filter)
        {
            try
            {
                var client = MongodbInfoClient();
                return await client.DeleteManyAsync(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Count 根据条件获取总数
        /// <summary>
        /// 根据条件获取总数
        /// </summary>

        /// <param name="filter">条件</param>
        /// <returns></returns>
        public long Count(FilterDefinition<T> filter)
        {
            try
            {
                var client = MongodbInfoClient();
                return client.Count(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CountAsync 异步根据条件获取总数
        /// <summary>
        /// 异步根据条件获取总数
        /// </summary>

        /// <param name="filter">条件</param>
        /// <returns></returns>
        public async Task<long> CountAsync(FilterDefinition<T> filter)
        {
            try
            {
                var client = MongodbInfoClient();
                return await client.CountAsync(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FindOne 根据id查询一条数据
        /// <summary>
        /// 根据id查询一条数据
        /// </summary>

        /// <param name="id">objectid</param>
        /// <param name="field">要查询的字段，不写时查询全部</param>
        /// <returns></returns>
        public T FindOne(string id, string[] field = null)
        {
            try
            {
                var client = MongodbInfoClient();
                FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    return client.Find(filter).FirstOrDefault<T>();
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<T>>();
                for (int i = 0; i < field.Length; i++)
                {
                    fieldList.Add(Builders<T>.Projection.Include(field[i]));
                }
                var projection = Builders<T>.Projection.Combine(fieldList);
                fieldList.Clear();
                return client.Find(filter).Project<T>(projection).FirstOrDefault<T>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FindOneAsync 异步根据id查询一条数据
        /// <summary>
        /// 异步根据id查询一条数据
        /// </summary>

        /// <param name="id">objectid</param>
        /// <returns></returns>
        public async Task<T> FindOneAsync(string id, string[] field = null)
        {
            try
            {
                var client = MongodbInfoClient();
                FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    return await client.Find(filter).FirstOrDefaultAsync();
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<T>>();
                for (int i = 0; i < field.Length; i++)
                {
                    fieldList.Add(Builders<T>.Projection.Include(field[i]));
                }
                var projection = Builders<T>.Projection.Combine(fieldList);
                fieldList?.Clear();
                return await client.Find(filter).Project<T>(projection).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FindList 查询集合
        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="field">要查询的字段,不写时查询全部</param>
        /// <param name="sort">要排序的字段</param>
        /// <returns></returns>
        public IEnumerable<T> GetAllEnumerable(FilterDefinition<T> filter = null, string[] field = null, SortDefinition<T> sort = null)
        {
            try
            {
                var client = MongodbInfoClient();
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    if (sort == null) return client.Find(filter).ToList();
                    //进行排序
                    return client.Find(filter).Sort(sort).ToList();
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<T>>();
                for (int i = 0; i < field.Length; i++)
                {
                    fieldList.Add(Builders<T>.Projection.Include(field[i]));
                }
                var projection = Builders<T>.Projection.Combine(fieldList);
                fieldList?.Clear();
                if (sort == null) return client.Find(filter).Project<T>(projection).ToList();
                //排序查询
                return client.Find(filter).Sort(sort).Project<T>(projection).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FindListAsync 异步查询集合
        /// <summary>
        /// 异步查询集合
        /// </summary>

        /// <param name="filter">查询条件</param>
        /// <param name="field">要查询的字段,不写时查询全部</param>
        /// <param name="sort">要排序的字段</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllEnumerableAsync(FilterDefinition<T> filter = null, string[] field = null, SortDefinition<T> sort = null)
        {
            try
            {
                var client = MongodbInfoClient();
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    if (sort == null) return await client.Find(filter).ToListAsync();
                    return await client.Find(filter).Sort(sort).ToListAsync();
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<T>>();
                for (int i = 0; i < field.Length; i++)
                {
                    fieldList.Add(Builders<T>.Projection.Include(field[i]));
                }
                var projection = Builders<T>.Projection.Combine(fieldList);
                fieldList?.Clear();
                if (sort == null) return await client.Find(filter).Project<T>(projection).ToListAsync();
                //排序查询
                return await client.Find(filter).Sort(sort).Project<T>(projection).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FindListByPage 分页查询集合
        /// <summary>
        /// 分页查询集合
        /// </summary>

        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>

        /// <param name="field">要查询的字段,不写时查询全部</param>
        /// <param name="sort">要排序的字段</param>
        /// <returns></returns>
        public PagedResults<T> FindListByPage(FilterDefinition<T> filter, int pageIndex, int pageSize, string[] field = null, SortDefinition<T> sort = null)
        {
            try
            {
                PagedResults<T> pagedResults = new PagedResults<T>();
                var client = MongodbInfoClient();
                var pagerInfo = new PagerInfo
                {
                    RecordCount = client.Count(filter),
                    Page = pageIndex
                };
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    if (sort == null)
                    {
                        pagedResults.Data = client.Find(filter).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
                    }
                    else
                    {  //进行排序
                        pagedResults.Data = client.Find(filter).Sort(sort).Skip((pageIndex - 1) * pageSize)
                            .Limit(pageSize).ToList();
                    }

                    pagerInfo.PageCount = (pagerInfo.RecordCount + pageIndex - 1) / pageIndex; //页总数 
                    pagedResults.PagerInfo = pagerInfo;
                    return pagedResults;
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<T>>();
                for (int i = 0; i < field.Length; i++)
                {
                    fieldList.Add(Builders<T>.Projection.Include(field[i]));
                }
                var projection = Builders<T>.Projection.Combine(fieldList);
                fieldList?.Clear();

                //不排序
                pagedResults.Data = sort == null ? client.Find(filter).Project<T>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList()
                    : client.Find(filter).Sort(sort).Project<T>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
                pagerInfo.PageCount = (pagerInfo.RecordCount + pageIndex - 1) / pageIndex; //页总数 
                pagedResults.PagerInfo = pagerInfo;
                return pagedResults;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FindListByPageAsync 异步分页查询集合
        /// <summary>
        /// 异步分页查询集合
        /// </summary>

        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="field">要查询的字段,不写时查询全部</param>
        /// <param name="sort">要排序的字段</param>
        /// <returns></returns>
        public async Task<PagedResults<T>> FindListByPageAsync(FilterDefinition<T> filter, int pageIndex, int pageSize, string[] field = null, SortDefinition<T> sort = null)
        {
            try
            {
                PagedResults<T> pagedResults = new PagedResults<T>();
                var client = MongodbInfoClient();
                var pagerInfo = new PagerInfo
                {
                    RecordCount = client.Count(filter),
                    Page = pageIndex
                };
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    if (sort == null)
                    {
                        pagedResults.Data = await client.Find(filter).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
                    }
                    else
                    {  //进行排序
                        pagedResults.Data = await client.Find(filter).Sort(sort).Skip((pageIndex - 1) * pageSize)
                            .Limit(pageSize).ToListAsync();
                    }

                    pagerInfo.PageCount = (pagerInfo.RecordCount + pageIndex - 1) / pageIndex; //页总数 
                    pagedResults.PagerInfo = pagerInfo;
                    return pagedResults;
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<T>>();
                for (int i = 0; i < field.Length; i++)
                {
                    fieldList.Add(Builders<T>.Projection.Include(field[i]));
                }
                var projection = Builders<T>.Projection.Combine(fieldList);
                fieldList?.Clear();

                //不排序
                if (sort == null)
                {
                    pagedResults.Data = await client.Find(filter).Project<T>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
                }
                else
                {
                    pagedResults.Data = await client.Find(filter).Sort(sort).Project<T>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
                }
                pagerInfo.PageCount = (pagerInfo.RecordCount + pageIndex - 1) / pageIndex; //页总数 
                pagedResults.PagerInfo = pagerInfo;
                return pagedResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}