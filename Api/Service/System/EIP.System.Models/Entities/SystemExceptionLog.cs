using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EIP.Common.Entities.CustomAttributes;
using MongoDB.Bson.Serialization.Attributes;

namespace EIP.System.Models.Entities
{
    /// <summary>
    ///     System_ExceptionLog表实体类
    /// </summary>
    [BsonIgnoreExtraElements] 
    [Table( "System_ExceptionLog")]
    [Db("EIP_Log")]
    public class SystemExceptionLog 
    {
        /// <summary>
        ///     主键Id
        /// </summary>
        [Key]
        public Guid ExceptionLogId { get; set; }
        /// <summary>
        ///     消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     堆栈信息
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        ///     内部信息
        /// </summary>
        public string InnerException { get; set; }

        /// <summary>
        ///     客户端Id
        /// </summary>
        public string RemoteIp { get; set; }

        /// <summary>
        ///     客户端Ip对应地址
        /// </summary>
        public string RemoteIpAddress { get; set; }

        /// <summary>
        ///     请求Url
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        ///     请求数据
        /// </summary>
        public string RequestData { get; set; }

        /// <summary>
        ///     请求方式
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        ///     创建人员
        /// </summary>
        public Guid CreateUserId { get; set; }

        /// <summary>
        ///     创建人员登录代码
        /// </summary>
        public string CreateUserCode { get; set; }

        /// <summary>
        ///     创建人员名字
        /// </summary>
        public string CreateUserName { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}