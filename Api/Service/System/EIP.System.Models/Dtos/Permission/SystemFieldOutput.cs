using EIP.Common.Entities.Dtos;
using EIP.System.Models.Entities;

namespace EIP.System.Models.Dtos.Permission
{
    /// <summary>
    /// 字段Dto
    /// </summary>
    public class SystemFieldOutput : SystemField, IOutputDto
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int RecordCount { get; set; }
    }
}