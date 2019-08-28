using EIP.Common.Business;
using EIP.Common.Core.Extensions;
using EIP.Common.Core.Utils;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.System.DataAccess.Permission;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using EIP.System.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIP.System.Business.Permission
{
    /// <summary>
    ///     功能项业务逻辑
    /// </summary>
    public class SystemMenuButtonLogic : DapperAsyncLogic<SystemMenuButton>, ISystemMenuButtonLogic
    {
        #region 构造函数
        private readonly ISystemMenuRepository _menuRepository;
        private readonly ISystemMenuButtonRepository _functionRepository;
        private readonly ISystemPermissionLogic _permissionLogic;

        public SystemMenuButtonLogic(ISystemMenuButtonRepository functionRepository,
            ISystemPermissionLogic permissionLogic, ISystemMenuRepository menuRepository)
            : base(functionRepository)
        {
            _menuRepository = menuRepository;
            _permissionLogic = permissionLogic;
            _functionRepository = functionRepository;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     根据菜单获取功能项信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemMenuButtonOutput>> GetMenuButtonByMenuId(SystemMenuGetMenuButtonByMenuIdInput input)
        {
            var functions = (await _functionRepository.GetMenuButtonByMenuId(input)).ToList();
            var menus = (await _menuRepository.FindAllAsync()).ToList();
            foreach (var item in functions)
            {
                var menu = menus.FirstOrDefault(w => w.MenuId == item.MenuId);
                if (menu != null && !menu.ParentIds.IsNullOrEmpty())
                {
                    foreach (var parent in menu.ParentIds.Split(','))
                    {
                        //查找上级
                        var dicinfo = menus.FirstOrDefault(w => w.MenuId.ToString() == parent);
                        if (dicinfo != null) item.MenuNames += dicinfo.Name + ">";
                    }
                    if (!item.MenuNames.IsNullOrEmpty())
                        item.MenuNames = item.MenuNames.TrimEnd('>');
                }
            }
            return functions.OrderBy(o => o.MenuNames).ThenBy(b => b.OrderNo).ToList();
        }

        /// <summary>
        ///     保存功能项信息
        /// </summary>
        /// <param name="input">功能项信息</param>
        /// <returns></returns>
        public async Task<OperateStatus> SaveMenuButton(SystemMenuButtonSaveInput input)
        {
            SystemMenuButton button = input.MapTo<SystemMenuButton>();
            if (button.MenuButtonId.IsEmptyGuid())
            {
                button.MenuButtonId = CombUtil.NewComb();
                return await InsertAsync(button);
            }
            return await UpdateAsync(button);
        }

        /// <summary>
        ///     删除功能项
        /// </summary>
        /// <param name="input">功能项信息</param>
        /// <returns></returns>
        public async Task<OperateStatus> DeleteMenuButton(IdInput input)
        {
            var operateStatus = new OperateStatus();
            //查看该功能项是否已被特性占用
            var permissions = await _permissionLogic.DeleteSystemPermissionsByPrivilegeAccessAndValue(EnumPrivilegeAccess.菜单按钮, input.Id);
            return await DeleteAsync(new SystemMenuButton
            {
                MenuButtonId = input.Id
            });
        }

        /// <summary>
        ///     获取登录人员对应菜单下的功能项
        /// </summary>
        /// <param name="mvcRote">路由信息</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemMenuButton>> GetMenuButtonByMenuIdAndUserId(MvcRote mvcRote,
            Guid userId)
        {
            return (await _functionRepository.GetMenuButtonByMenuIdAndUserId(mvcRote, userId)).ToList();
        }

        #endregion
    }
}