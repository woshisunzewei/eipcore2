using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Common.Entities;
using EIP.System.Models.Entities;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Dtos.Permission;

namespace EIP.System.DataAccess.Permission
{
    public interface ISystemMenuButtonRepository : IAsyncRepository<SystemMenuButton>
    {
        /// <summary>
        ///     根据菜单获取功能项信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemMenuButtonOutput>> GetMenuButtonByMenuId(SystemMenuGetMenuButtonByMenuIdInput input = null);

        /// <summary>
        ///     根据路由信息获取功能项信息
        /// </summary>
        /// <param name="mvcRote"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemMenuButton>> GetMenuButtonByMvcRote(MvcRote mvcRote);

        /// <summary>
        ///     根据菜单Id和用户Id获取按钮权限数据
        /// </summary>
        /// <param name="mvcRote"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemMenuButton>> GetMenuButtonByMenuIdAndUserId(MvcRote mvcRote,
            Guid userId);
    }
}