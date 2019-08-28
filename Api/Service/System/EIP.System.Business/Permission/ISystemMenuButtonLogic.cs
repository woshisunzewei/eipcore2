using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.System.Models.Entities;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Dtos.Permission;

namespace EIP.System.Business.Permission
{
    /// <summary>
    ///     功能项业务逻辑
    /// </summary>
    public interface ISystemMenuButtonLogic : IAsyncLogic<SystemMenuButton>
    {
        /// <summary>
        ///     根据菜单获取功能项信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemMenuButtonOutput>> GetMenuButtonByMenuId(SystemMenuGetMenuButtonByMenuIdInput input);

        /// <summary>
        ///     保存功能项信息
        /// </summary>
        /// <param name="function">功能项信息</param>
        /// <returns></returns>
        Task<OperateStatus> SaveMenuButton(SystemMenuButtonSaveInput function);

        /// <summary>
        ///     删除功能项
        /// </summary>
        /// <param name="input">功能项信息</param>
        /// <returns></returns>
        Task<OperateStatus> DeleteMenuButton(IdInput input);

        /// <summary>
        ///     获取登录人员对应菜单下的功能项
        /// </summary>
        /// <param name="mvcRote">路由信息</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        Task<IEnumerable<SystemMenuButton>> GetMenuButtonByMenuIdAndUserId(MvcRote mvcRote,
             Guid userId);
    }
}