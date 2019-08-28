using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using EIP.Common.Core.Utils;
using EIP.Common.Entities.CustomAttributes;
using EIP.Common.Entities.Paging;

namespace EIP.Common.Dapper
{
    public static class SqlMapperUtil
    {
        private static IDbConnection GetConnectoin<T>()
        {
            string connectionString = ConfigurationUtil.GetSection("EIP:ConnectionString");
            Type t = typeof(T);
            DbAttribute m = t.GetTypeInfo().GetCustomAttribute<DbAttribute>();
            if (m != null)
            {
                connectionString = ConfigurationUtil.GetSection(m.Name + ":ConnectionString");
            }
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }



        public static T Find<T>(IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.Find(transaction);
            }
        }


        public static T Find<T>(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.Find(predicate, transaction);
            }
        }


        public static T Find<T, TChild1>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.Find<TChild1>(predicate, tChild1, transaction);
            }
        }

        public static T Find<T, TChild1, TChild2>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.Find<TChild1, TChild2>(predicate, tChild1, tChild2, transaction);
            }
        }

        public static T Find<T, TChild1, TChild2, TChild3>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.Find<TChild1, TChild2, TChild3>(predicate, tChild1, tChild2, tChild3, transaction);
            }
        }



        public static T Find<T, TChild1, TChild2, TChild3, TChild4>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.Find<TChild1, TChild2, TChild3, TChild4>(predicate, tChild1, tChild2, tChild3, tChild4, transaction);
            }
        }


        public static T Find<T, TChild1, TChild2, TChild3, TChild4, TChild5>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.Find<TChild1, TChild2, TChild3, TChild4, TChild5>(predicate, tChild1, tChild2, tChild3, tChild4, tChild5, transaction);
            }
        }

        public static T Find<T, TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.Find<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(predicate, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6, transaction);
            }
        }

        public static T FindById<T>(object id, IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindById(id, transaction);
            }
        }


        public static T FindById<T, TChild1>(object id,
            Expression<Func<T, object>> tChild1,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindById<TChild1>(id, tChild1, transaction);
            }
        }


        public static T FindById<T, TChild1, TChild2>(object id,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindById<TChild1, TChild2>(id, tChild1, tChild2, transaction);
            }
        }


        public static T FindById<T, TChild1, TChild2, TChild3>(object id,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindById<TChild1, TChild2, TChild3>(id, tChild1, tChild2, tChild3, transaction);
            }
        }


        public static T FindById<T, TChild1, TChild2, TChild3, TChild4>(object id,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindById<TChild1, TChild2, TChild3, TChild4>(id, tChild1, tChild2, tChild3, tChild4, transaction);
            }
        }


        public static T FindById<T, TChild1, TChild2, TChild3, TChild4, TChild5>(object id,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindById<TChild1, TChild2, TChild3, TChild4, TChild5>(id, tChild1, tChild2, tChild3, tChild4, tChild5, transaction);
            }
        }


        public static T FindById<T, TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(object id,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindById<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(id, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6, transaction);
            }
        }


        public static async Task<T> FindByIdAsync<T>(object id, IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return await repository.FindByIdAsync(id, transaction);
            }
        }


        public static Task<T> FindByIdAsync<T, TChild1>(object id,
            Expression<Func<T, object>> tChild1,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindByIdAsync<TChild1>(id, tChild1, transaction);
            }
        }


        public static Task<T> FindByIdAsync<T, TChild1, TChild2>(object id,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindByIdAsync<TChild1, TChild2>(id, tChild1, tChild2, transaction);
            }
        }


        public static Task<T> FindByIdAsync<T, TChild1, TChild2, TChild3>(object id,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindByIdAsync<TChild1, TChild2, TChild3>(id, tChild1, tChild2, tChild3, transaction);
            }
        }


        public static Task<T> FindByIdAsync<T, TChild1, TChild2, TChild3, TChild4>(object id,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindByIdAsync<TChild1, TChild2, TChild3, TChild4>(id, tChild1, tChild2, tChild3, tChild4, transaction);
            }
        }


        public static Task<T> FindByIdAsync<T, TChild1, TChild2, TChild3, TChild4, TChild5>(object id,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindByIdAsync<TChild1, TChild2, TChild3, TChild4, TChild5>(id, tChild1, tChild2, tChild3, tChild4, tChild5, transaction);
            }
        }


        public static Task<T> FindByIdAsync<T, TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(object id,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindByIdAsync<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(id, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6, transaction);
            }
        }


        public static Task<T> FindAsync<T>(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAsync(predicate, transaction);
            }
        }

        public static Task<T> FindAsync<T>(IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAsync(transaction);
            }
        }


        public static Task<T> FindAsync<T, TChild1>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAsync<TChild1>(predicate, tChild1, transaction);
            }
        }


        public static Task<T> FindAsync<T, TChild1, TChild2>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAsync<TChild1, TChild2>(predicate, tChild1, tChild2, transaction);
            }
        }


        public static Task<T> FindAsync<T, TChild1, TChild2, TChild3>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAsync<TChild1, TChild2, TChild3>(predicate, tChild1, tChild2, tChild3, transaction);
            }
        }


        public static Task<T> FindAsync<T, TChild1, TChild2, TChild3, TChild4>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAsync<TChild1, TChild2, TChild3, TChild4>(predicate, tChild1, tChild2, tChild3, tChild4, transaction);
            }
        }


        public static Task<T> FindAsync<T, TChild1, TChild2, TChild3, TChild4, TChild5>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAsync<TChild1, TChild2, TChild3, TChild4, TChild5>(predicate, tChild1, tChild2, tChild3, tChild4, tChild5, transaction);
            }
        }


        public static Task<T> FindAsync<T, TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAsync<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(predicate, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6, transaction);
            }
        }


        public static IEnumerable<T> FindAll<T>(IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAll(transaction);
            }
        }


        public static IEnumerable<T> FindAll<T>(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAll(predicate, transaction);
            }
        }


        public static IEnumerable<T> FindAll<T, TChild1>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAll<TChild1>(predicate, tChild1, transaction);
            }
        }


        public static IEnumerable<T> FindAll<T, TChild1, TChild2>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAll<TChild1, TChild2>(predicate, tChild1, tChild2, transaction);
            }
        }


        public static IEnumerable<T> FindAll<T, TChild1, TChild2, TChild3>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAll<TChild1, TChild2, TChild3>(predicate, tChild1, tChild2, tChild3, transaction);
            }
        }


        public static IEnumerable<T> FindAll<T, TChild1, TChild2, TChild3, TChild4>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAll<TChild1, TChild2, TChild3, TChild4>(predicate, tChild1, tChild2, tChild3, tChild4, transaction);
            }
        }


        public static IEnumerable<T> FindAll<T, TChild1, TChild2, TChild3, TChild4, TChild5>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAll<TChild1, TChild2, TChild3, TChild4, TChild5>(predicate, tChild1, tChild2, tChild3, tChild4, tChild5, transaction);
            }
        }


        public static IEnumerable<T> FindAll<T, TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAll<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(predicate, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6, transaction);
            }
        }

        public static async Task<IEnumerable<T>> FindAllAsync<T>(IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return await repository.FindAllAsync(transaction);
            }
        }


        public static async Task<IEnumerable<T>> FindAllAsync<T>(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return await repository.FindAllAsync(predicate, transaction);
            }
        }


        public static Task<IEnumerable<T>> FindAllAsync<T, TChild1>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> tChild1, IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAllAsync<TChild1>(predicate, tChild1, transaction);
            }
        }


        public static Task<IEnumerable<T>> FindAllAsync<T, TChild1, TChild2>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAllAsync<TChild1, TChild2>(predicate, tChild1, tChild2, transaction);
            }
        }


        public static Task<IEnumerable<T>> FindAllAsync<T, TChild1, TChild2, TChild3>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAllAsync<TChild1, TChild2, TChild3>(predicate, tChild1, tChild2, tChild3, transaction);
            }
        }


        public static Task<IEnumerable<T>> FindAllAsync<T, TChild1, TChild2, TChild3, TChild4>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAllAsync<TChild1, TChild2, TChild3, TChild4>(predicate, tChild1, tChild2, tChild3, tChild4, transaction);
            }
        }


        public static Task<IEnumerable<T>> FindAllAsync<T, TChild1, TChild2, TChild3, TChild4, TChild5>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAllAsync<TChild1, TChild2, TChild3, TChild4, TChild5>(predicate, tChild1, tChild2, tChild3, tChild4, tChild5, transaction);
            }
        }


        public static Task<IEnumerable<T>> FindAllAsync<T, TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> tChild1,
            Expression<Func<T, object>> tChild2,
            Expression<Func<T, object>> tChild3,
            Expression<Func<T, object>> tChild4,
            Expression<Func<T, object>> tChild5,
            Expression<Func<T, object>> tChild6,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAllAsync<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(predicate, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6, transaction);
            }
        }


        public static bool Insert<T>(T instance, IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.Insert(instance, transaction);
            }
        }


        public static async Task<bool> InsertAsync<T>(T instance, IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return await repository.InsertAsync(instance, transaction);
            }
        }


        public static int BulkInsert<T>(IEnumerable<T> instances, IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.BulkInsert(instances, transaction);
            }
        }


        public static async Task<int> BulkInsertAsync<T>(IEnumerable<T> instances, IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return await repository.BulkInsertAsync(instances, transaction);
            }
        }


        public static bool BulkUpdate<T>(IEnumerable<T> instances, IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.BulkUpdate(instances, transaction);
            }
        }


        public static Task<bool> BulkUpdateAsync<T>(IEnumerable<T> instances, IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.BulkUpdateAsync(instances, transaction);
            }
        }

        public static bool Delete<T>(T instance, IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.Delete(instance, transaction);
            }
        }


        public static async Task<bool> DeleteAsync<T>(T instance, IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return await repository.DeleteAsync(instance, transaction);
            }
        }

        public static async Task<bool> DeleteByIdsAsync<T>(string ids) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return await repository.DeleteByIdsAsync(ids);
            }
        }


        public static bool Delete<T>(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.Delete(predicate, transaction);
            }
        }


        public static async Task<bool> DeleteAllAsync<T>(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return await repository.DeleteAsync(predicate, transaction);
            }
        }


        public static bool Update<T>(T instance, IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.Update(instance, transaction);
            }
        }


        public static async Task<bool> UpdateAsync<T>(T instance, IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return await repository.UpdateAsync(instance, transaction);
            }
        }



        public static IEnumerable<T> FindAllBetween<T>(object from, object to,
            Expression<Func<T, object>> btwField,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAllBetween(from, to, btwField, transaction);
            }
        }


        public static IEnumerable<T> FindAllBetween<T>(object from, object to,
            Expression<Func<T, object>> btwField,
            Expression<Func<T, bool>> predicate = null,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAllBetween(from, to, btwField, transaction);
            }
        }


        public static IEnumerable<T> FindAllBetween<T>(DateTime from,
            DateTime to,
            Expression<Func<T, object>> btwField,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAllBetween(from, to, btwField, transaction);
            }
        }


        public static IEnumerable<T> FindAllBetween<T>(DateTime from,
            DateTime to,
            Expression<Func<T, object>> btwField,
            Expression<Func<T, bool>> predicate,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAllBetween(from, to, btwField, transaction);
            }
        }


        public static Task<IEnumerable<T>> FindAllBetweenAsync<T>(object from,
            object to,
            Expression<Func<T, object>> btwField,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAllBetweenAsync(from, to, btwField, transaction);
            }
        }


        public static Task<IEnumerable<T>> FindAllBetweenAsync<T>(object from,
            object to,
            Expression<Func<T, object>> btwField,
            Expression<Func<T, bool>> predicate,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAllBetweenAsync(from, to, btwField, transaction);
            }
        }


        public static Task<IEnumerable<T>> FindAllBetweenAsync<T>(DateTime from,
            DateTime to,
            Expression<Func<T, object>> btwField,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAllBetweenAsync(from, to, btwField, transaction);
            }
        }


        public static Task<IEnumerable<T>> FindAllBetweenAsync<T>(DateTime from,
            DateTime to,
            Expression<Func<T, object>> btwField,
            Expression<Func<T, bool>> predicate,
            IDbTransaction transaction = null) where T : class
        {
            using (IDbConnection connection = GetConnectoin<T>())
            {
                var repository = new DapperRepository<T>(connection);
                return repository.FindAllBetweenAsync(from, to, btwField, transaction);
            }
        }

        #region 分页
        /// <summary>
        ///     复杂查询分页
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="querySql">查询语句</param>
        /// <param name="queryParam">查询参数</param>
        /// <returns>分页结果</returns>
        /// <remarks>
        ///     注意事项：
        ///     1.sql语句中需要加上@where、@orderBy、@rowNumber、@recordCount标记
        ///     如: "select *, @rowNumber, @recordCount from ADM_Rule @where"
        ///     2.实体中需增加扩展属性，作记录总数输出：RecordCount
        ///     3.标记解释:
        ///     @where：      查询条件
        ///     @orderBy：    排序
        ///     @x：          分页记录起点
        ///     @y：          分页记录终点
        ///     @recordCount：记录总数
        ///     @rowNumber：  行号
        ///     4.示例参考:
        /// </remarks>
        public static Task<PagedResults<T>> PagingQueryAsync<T>(string querySql, QueryParam queryParam)
        {
            var sql = queryParam.IsReport ?
                string.Format(@"select * from ({0}) seq ", querySql) :
                string.Format(@"select * from ({0}) seq where seq.rownum between @x and @y", querySql);
            var currentPage = queryParam.Page; //当前页号
            var pageSize = queryParam.Limit; //每页记录数
            var lower = ((currentPage - 1) * pageSize) + 1; //记录起点
            var upper = currentPage * pageSize; //记录终点
            var parms = new DynamicParameters();
            parms.Add("x", lower);
            parms.Add("y", upper);
            //排序字段
            var orderString = string.Format("{0} {1}", queryParam.Sidx, queryParam.Sord);
            sql = sql.Replace("@recordCount", " count(*) over() as RecordCount ")
                .Replace("@rowNumber", " row_number() over (order by @orderBy) as rownum ")
                .Replace("@orderBy", orderString)
                .Replace("@where", " WHERE 1=1 " + (string.IsNullOrWhiteSpace(queryParam.Filters) ? string.Empty : SearchFilterUtil.ConvertFilters(queryParam.Filters)));
            var data = SqlWithParams<T>(sql, parms).Result.ToList();
            var pagerInfo = new PagerInfo();
            var first = data.FirstOrDefault();
            //记录总数
            if (first != null)
                pagerInfo.RecordCount = (int)first.GetType().GetProperty("RecordCount").GetValue(first, null);
            pagerInfo.Page = queryParam.Page;
            pagerInfo.PageCount = (pagerInfo.RecordCount + queryParam.Limit - 1) / queryParam.Limit; //页总数 
            return Task.Factory.StartNew(() => new PagedResults<T> { Data = data, PagerInfo = pagerInfo });
        }

        /// <summary>
        /// 单表存储过程分页
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="queryParam">分页参数</param>
        /// <returns>返回值</returns>
        public static Task<PagedResults<T>> PagingQueryProcAsync<T>(QueryParam queryParam) where T : class
        {
            try
            {
                using (IDbConnection connection = GetConnectoin<T>())
                {
                    var repository = new DapperRepository<T>(connection);
                    var parms = new DynamicParameters();
                    parms.Add("TableName", repository.SqlGenerator.TableName);
                    parms.Add("PrimaryKey", repository.SqlGenerator.KeySqlProperties[0].ColumnName);
                    parms.Add("Fields", "*");
                    parms.Add("Filters", queryParam.Filters);
                    parms.Add("PageIndex", queryParam.Page);
                    parms.Add("PageSize", queryParam.Limit);
                    parms.Add("Sort", queryParam.Sidx + " " + queryParam.Sord);
                    parms.Add("RecordCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    var pagerInfo = new PagerInfo();
                    var data = StoredProcWithParams<T>("System_Proc_Paging", parms).Result.ToList();
                    pagerInfo.RecordCount = parms.Get<int>("RecordCount");
                    pagerInfo.Page = queryParam.Page;
                    pagerInfo.PageCount = (pagerInfo.RecordCount + queryParam.Limit - 1) / queryParam.Limit; //页总数 
                    return Task.Factory.StartNew(() => new PagedResults<T>
                    {
                        Data = data,
                        PagerInfo = pagerInfo
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion

        #region 查询
        /// <summary>
        ///     执行Sql语句带参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> SqlWithParams<T>(string sql, dynamic parms = null)
        {
            using (IDbConnection db = GetConnectoin<T>())
            {
                return await db.QueryAsync<T>(sql, (object)parms);
            }
        }

        /// <summary>
        ///     执行语句返回bool
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static async Task<bool> SqlWithParamsBool<T>(string sql, dynamic parms)
        {
            using (IDbConnection db = GetConnectoin<T>())
            {
                return (await db.QueryAsync<T>(sql, (object)parms)).Any();
            }
        }

        /// <summary>
        /// 执行语句返回第一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static async Task<T> SqlWithParamsSingle<T>(string sql, dynamic parms = null)
        {
            using (IDbConnection db = GetConnectoin<T>())
            {
                return await db.QueryFirstOrDefaultAsync<T>(sql, (object)parms);
            }
        }

        #endregion

        #region MyRegion

        /// <summary>
        ///     执行增加删除修改语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="parms">参数信息</param>
        /// <param name="isSetConnectionStr">是否需要重置连接字符串</param>
        /// <returns>影响数</returns>
        public static async Task<int> InsertUpdateOrDeleteSql<T>(string sql, dynamic parms = null, bool isSetConnectionStr = true)
        {
            using (var db = GetConnectoin<T>())
            {
                return await db.ExecuteAsync(sql, (object)parms);
            }
        }

        /// <summary>
        ///     执行增加删除修改语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="parms">参数信息</param>
        /// <param name="isSetConnectionStr">是否需要重置连接字符串</param>
        /// <returns>影响数</returns>
        public static async Task<bool> InsertUpdateOrDeleteSqlBool<T>(string sql, dynamic parms = null, bool isSetConnectionStr = true)
        {
            using (var db = GetConnectoin<T>())
            {
                return await db.ExecuteAsync(sql, (object)parms) > 0;
            }
        }

        #endregion

        #region 存储过程

        /// <summary>
        /// 存储过程查询所有值:同步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procName"></param>
        /// <param name="parms"></param>
        /// <param name="isSetConnectionStr"></param>
        /// <returns></returns>
        public static IEnumerable<T> StoredProcWithParamsSync<T>(string procName, dynamic parms,
            bool isSetConnectionStr = true)
        {
            using (var db = GetConnectoin<T>())
            {
                return db.Query<T>(procName, (object)parms);
            }
        }

        /// <summary>
        ///     存储过程查询所有值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procName">The procname.</param>
        /// <param name="parms">The parms.</param>
        /// <param name="isSetConnectionStr"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> StoredProcWithParams<T>(string procName, dynamic parms,
            bool isSetConnectionStr = true)
        {
            using (IDbConnection db = GetConnectoin<T>())
            {
                return await db.QueryAsync<T>(procName, (object)parms, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// 增删改查存储过程
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static Task<int> InsertUpdateOrDeleteStoredProc<T>(string procName, dynamic parms = null)
        {
            using (var db = GetConnectoin<T>())
            {
                return db.ExecuteAsync(procName, (object)parms);
            }
        }

        /// <summary>
        /// 返回存储过程第一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procName"></param>
        /// <param name="parms"></param>
        /// <param name="isSetConnectionStr"></param>
        /// <returns></returns>
        public static Task<T> StoredProcWithParamsSingle<T>(string procName, dynamic parms = null,
            bool isSetConnectionStr = true)
        {
            using (var db = GetConnectoin<T>())
            {
                return db.QueryFirstAsync<T>(procName, (object)parms);
            }
        }
        #endregion
    }

    public class DontMap
    {
    }
}