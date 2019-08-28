using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.Common.Core.Resource;
using EIP.System.DataAccess.Permission;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Permission
{
    /// <summary>
    ///     菜单按钮功能逻辑接口
    /// </summary>
    public class SystemMenuButtonFunctionLogic : DapperAsyncLogic<SystemMenuButtonFunction>, ISystemMenuButtonFunctionLogic
    {
        #region 构造函数

        private readonly ISystemMenuButtonFunctionRepository _menuButtonFunctionRepository;

        public SystemMenuButtonFunctionLogic(ISystemMenuButtonFunctionRepository menuButtonFunctionRepository)
            : base(menuButtonFunctionRepository)
        {
            _menuButtonFunctionRepository = menuButtonFunctionRepository;
        }

        #endregion

        /// <summary>
        /// 获取菜单按钮功能项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemMenuButtonFunctionOutput>> GetMenuButtonFunctions(IdInput input)
        {
            return (await _menuButtonFunctionRepository.GetMenuButtonFunctions(input)).ToList();
        }

        /// <summary>
        /// 保存菜单按钮功能项
        /// </summary>
        /// <param name="menuButtonFunctions"></param>
        /// <returns></returns>
        public async Task<OperateStatus> SaveMenuButtonFunction(IList<SystemMenuButtonFunction> menuButtonFunctions)
        {
            return await InsertMultipleAsync(menuButtonFunctions);
        }

        /// <summary>
        /// 删除菜单按钮功能项
        /// </summary>
        /// <param name="menuButtonFunction"></param>
        /// <returns></returns>
        public async Task<OperateStatus> DeleteMenuButtonFunction(SystemMenuButtonFunction menuButtonFunction)
        {
            OperateStatus operateStatus = new OperateStatus();
            if (await _menuButtonFunctionRepository.DeleteMenuButtonFunction(menuButtonFunction))
            {
                operateStatus.ResultSign = ResultSign.Successful;
                operateStatus.Message = Chs.Successful;
            }
            return operateStatus;
        }

        /// <summary>
        /// 获取所有功能项,若input有值则排除该功能项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemMenuButtonFunctionOutput>> GetAllFunctions(SystemMenuButtonGetFunctionsInput input)
        {
            return (await _menuButtonFunctionRepository.GetAllFunctions(input)).ToList();
        }
    }
}