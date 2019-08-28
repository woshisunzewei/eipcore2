using System;
using EIP.Common.Core.Utils;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace EIP.Common.Core.Log
{
    /// <summary>
    ///     说  明:日志记录基类
    ///     备  注:
    ///     编写人:孙泽伟-2015/04/01
    /// </summary>
    /// <typeparam name="TLog">记录日志实体</typeparam>
    public abstract class BaseHandler<TLog>
    {
        #region 构造函数

        /// <summary>
        ///     构造函数初始化
        /// </summary>
        /// <param name="loggerConfig"></param>
        protected BaseHandler(string loggerConfig)
        {
            LoggerConfig = loggerConfig;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     写入日志,虚函数.可进行重写
        /// </summary>
        public virtual void WriteLog()
        {
            if (string.IsNullOrEmpty(LoggerConfig))
            {
                return;
            }
            Task.Factory.StartNew(() => InsertMongoDb());
        }

        /// <summary>
        /// 插入MongoDb
        /// </summary>
        private void InsertMongoDb()
        {
            var client = new MongoClient(ConfigurationUtil.GetSection("MongoDb:ConnectionString"));
            var dataBase = client.GetDatabase("EIP_Log");
            var table = dataBase.GetCollection<TLog>(LoggerConfig);
            table.InsertOneAsync(Log);
        }

        /// <summary>
        /// 获取请求数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected string RequestData(HttpRequest request)
        {
            if (request.ContentLength == 0) return String.Empty;
            return request.Method == HttpMethods.Post ? JsonConvert.SerializeObject(request.Form) : request.QueryString.Value;
        }
        #endregion

        #region 属性

        /// <summary>
        ///     需要启动的日志模式名称
        /// </summary>
        private string LoggerConfig { get; set; }

        public TLog Log { get; set; }

        #endregion
    }
}