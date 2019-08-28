using System;
using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Config
{
    public class SystemDataBaseBackUpDoubleWay : IdInput, IDoubleWayDto
    {
        /// <summary>
        /// 备份地址
        /// </summary>
        public string BackUpOrRestorePath { get; set; }

        /// <summary>
        /// 数据库Id
        /// </summary>
        public Guid DataBaseId { get; set; }
    }
}