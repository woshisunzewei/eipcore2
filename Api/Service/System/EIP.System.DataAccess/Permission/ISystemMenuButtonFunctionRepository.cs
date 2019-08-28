using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Permission
{
    /// <summary>
    /// 菜单按钮功能
    /// </summary>
    public interface ISystemMenuButtonFunctionRepository : IAsyncRepository<SystemMenuButtonFunction>
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
        /// 删除菜单按钮功能项
        /// </summary>
        /// <param name="menuButtonFunction"></param>
        /// <returns></returns>
        Task<bool> DeleteMenuButtonFunction(SystemMenuButtonFunction menuButtonFunction);
    }
}