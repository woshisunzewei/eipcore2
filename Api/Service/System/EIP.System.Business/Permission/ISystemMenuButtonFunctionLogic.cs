using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Permission
{
    /// <summary>
    ///    菜单按钮功能逻辑接口
    /// </summary>
    public interface ISystemMenuButtonFunctionLogic : IAsyncLogic<SystemMenuButtonFunction>
    {
        /// <summary>
        /// 获取菜单按钮功能项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemMenuButtonFunctionOutput>> GetMenuButtonFunctions(IdInput input);

        /// <summary>
        /// 获取所有功能项,若input有值则排除该功能项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemMenuButtonFunctionOutput>> GetAllFunctions(SystemMenuButtonGetFunctionsInput input);

        /// <summary>
        /// 保存菜单按钮功能项
        /// </summary>
        /// <param name="menuButtonFunctions"></param>
        /// <returns></returns>
        Task<OperateStatus> SaveMenuButtonFunction(IList<SystemMenuButtonFunction> menuButtonFunctions);

        /// <summary>
        /// 删除菜单按钮功能项
        /// </summary>
        /// <param name="menuButtonFunction"></param>
        /// <returns></returns>
        Task<OperateStatus> DeleteMenuButtonFunction(SystemMenuButtonFunction menuButtonFunction);
    }
}