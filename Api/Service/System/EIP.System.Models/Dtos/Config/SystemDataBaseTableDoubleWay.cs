using System;
using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Config
{
    /// <summary>
    ///     表
    /// </summary>
    public class SystemDataBaseTableDoubleWay : IdInput, IDoubleWayDto
    {
        /// <summary>
        /// 表名称
        /// </summary>

        public string TableName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}