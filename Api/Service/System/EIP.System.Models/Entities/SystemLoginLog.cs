using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EIP.Common.Entities.CustomAttributes;
using MongoDB.Bson.Serialization.Attributes;

namespace EIP.System.Models.Entities
{
    /// <summary>
    /// System_LoginLog表实体类
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("System_LoginLog")]
    [Db("EIP_Log")]
    public class SystemLoginLog
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        public Guid LoginLogId { get; set; }

        /// <summary>
        ///     客户端Id
        /// </summary>
        public string RemoteIp { get; set; }

        /// <summary>
        ///     客户端Ip对应地址
        /// </summary>
        public string RemoteIpAddress { get; set; }

        /// <summary>
        ///     登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        ///     退出时间
        /// </summary>
        public DateTime? LoginOutTime { get; set; }

        /// <summary>
        ///     停留时间(分钟)
        /// </summary>
        public double? StandingTime { get; set; }

        /// <summary>
        ///     创建人员
        /// </summary>
        public Guid? CreateUserId { get; set; }

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
