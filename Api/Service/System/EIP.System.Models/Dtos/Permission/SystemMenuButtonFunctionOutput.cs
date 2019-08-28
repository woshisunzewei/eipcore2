using System;
using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Permission
{
    /// <summary>
    /// 菜单按钮功能项
    /// </summary>
    public class SystemMenuButtonFunctionOutput : IOutputDto
    {
        /// <summary>
        /// 按钮Id
        /// </summary>
        public Guid MenuButtonId { get; set; }

        /// <summary>
        /// 功能项Id
        /// </summary>
        public Guid FunctionId { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        public string AppCode { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 视图
        /// </summary>
        public bool IsPage { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}