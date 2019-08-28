using EIP.Common.Entities.Dtos;
using EIP.System.Models.Entities;

namespace EIP.System.Models.Dtos.Permission
{
    public class SystemMenuButtonOutput : SystemMenuButton, IOutputDto
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuNames { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
    }
}