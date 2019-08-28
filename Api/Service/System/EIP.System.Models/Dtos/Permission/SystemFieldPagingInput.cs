using System;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Paging;

namespace EIP.System.Models.Dtos.Permission
{
    /// <summary>
    /// 系统字段分页参数
    /// </summary>
    public class SystemFieldPagingInput : QueryParam, IInputDto
    {
        /// <summary>
        /// 菜单
        /// </summary>
        public Guid? MenuId { get; set; }

        /// <summary>
        /// 是否显示隐藏
        /// </summary>
        public bool IsShowHidden { get; set; }
    }
}