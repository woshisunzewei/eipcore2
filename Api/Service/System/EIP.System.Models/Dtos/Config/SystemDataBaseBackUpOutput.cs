using System;
using EIP.Common.Core.Utils;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Enums;

namespace EIP.System.Models.Dtos.Config
{
    /// <summary>
    /// 数据库备份输出列表
    /// </summary>
    public class SystemDataBaseBackUpOutput : IOutputDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid BackUpId { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public byte From { get; set; }
        public string DataBaseBackFrom => EnumUtil.GetEnumNameByIndex<EnumFrom>(From);

        /// <summary>
        /// 备份名称
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 备份时间
        /// </summary>		
        public DateTime BackUpTime { get; set; }

        /// <summary>
        /// 还原时间
        /// </summary>		
        public DateTime? RestoreTime { get; set; }

        /// <summary>
        /// 大小
        /// </summary>		
        public string Size { get; set; }

        /// <summary>
        /// 备份文件路径
        /// </summary>		
        public string Path { get; set; }
    }
}