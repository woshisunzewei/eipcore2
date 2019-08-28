using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Core.Extensions;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Tree;
using EIP.Common.Core.Resource;
using EIP.Common.Core.Utils;
using EIP.System.DataAccess.Permission;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using EIP.System.Models.Enums;
using EIP.System.Models.Resx;

namespace EIP.System.Business.Permission
{
    public class SystemMenuLogic : DapperAsyncLogic<SystemMenu>, ISystemMenuLogic
    {
        #region 构造函数

        private readonly ISystemMenuRepository _menuRepository;
        private readonly ISystemMenuButtonRepository _menuButtonRepository;
        private readonly ISystemPermissionLogic _permissionLogic;

        public SystemMenuLogic(ISystemMenuRepository menuRepository,
            ISystemPermissionLogic permissionLogic, ISystemMenuButtonRepository menuButtonRepository)
            : base(menuRepository)
        {
            _menuButtonRepository = menuButtonRepository;
            _permissionLogic = permissionLogic;
            _menuRepository = menuRepository;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     根据状态为True的菜单信息
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<JsTreeEntity>> GetAllMenuTree()
        {
            return await _menuRepository.GetAllMenuTree();
        }

        /// <summary>
        ///     根据状态为True的菜单信息
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SystemMenu>> GetMeunuByPId(IdInput input)
        {
            return await _menuRepository.GetMeunuByPId(input);
        }

        /// <summary>
        ///     保存菜单
        /// </summary>
        /// <param name="menu">菜单信息</param>
        /// <returns></returns>
        public async Task<OperateStatus> SaveMenu(SystemMenu menu)
        {
            OperateStatus result;
            menu.CanbeDelete = true;
            if (menu.MenuId.IsEmptyGuid())
            {
                menu.MenuId = CombUtil.NewComb();
                result = await InsertAsync(menu);
            }
            else
            {
                result = await UpdateAsync(menu);
            }
            await GeneratingParentIds();
            return result;
        }

        /// <summary>
        ///     批量生成代码
        /// </summary>
        /// <returns></returns>
        public async Task<OperateStatus> GeneratingParentIds()
        {
            OperateStatus operateStatus = new OperateStatus();
            try
            {
                var alls = (await GetAllEnumerableAsync()).ToList();
                var tops = alls.Where(w => w.ParentId == Guid.Empty);
                foreach (var org in tops)
                {
                    org.ParentIds = org.MenuId.ToString();
                    await UpdateAsync(org);
                    await GeneratingParentIds(org, alls.ToList(), "");
                }
            }
            catch (Exception ex)
            {
                operateStatus.Message = ex.Message;
                return operateStatus;
            }
            operateStatus.Message = Chs.Successful;
            operateStatus.ResultSign = ResultSign.Successful;
            return operateStatus;
        }

        /// <summary>
        /// 递归获取代码
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="menus"></param>
        /// <param name="orgId"></param>
        private async Task GeneratingParentIds(SystemMenu menu, IList<SystemMenu> menus, string orgId)
        {
            string parentIds = menu.MenuId.ToString();
            //获取下级
            var next = menus.Where(w => w.ParentId == menu.MenuId).ToList();
            if (next.Any())
            {
                parentIds = orgId.IsNullOrEmpty() ? parentIds : orgId + "," + parentIds;
            }
            foreach (var or in next)
            {
                or.ParentIds = parentIds + "," + or.MenuId;
                await UpdateAsync(or);
                await GeneratingParentIds(or
                    , menus, parentIds);
            }
        }

        /// <summary>
        ///     删除菜单及下级数据
        /// </summary>
        /// <param name="input">父级id</param>
        /// <returns></returns>
        public async Task<OperateStatus> DeleteMenu(IdInput input)
        {
            var operateStatus = new OperateStatus();

            //判断该项能否进行删除
            var menu = await GetByIdAsync(input.Id);

            if (menu != null && !menu.CanbeDelete)
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message = Chs.CanotDelete;
                return operateStatus;
            }
            //查看是否具有下级
            if ((await GetMeunuByPId(input)).Any())
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message = ResourceSystem.具有下级项;
                return operateStatus;
            }
            //查看该菜单是否已被特性占用
            var permissions = await _permissionLogic.GetSystemPermissionsByPrivilegeAccessAndValue(EnumPrivilegeAccess.菜单, input.Id);
            if (permissions.Any())
            {
                //获取被占用类型及值
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message = ResourceSystem.已被赋予权限;
                return operateStatus;
            }
            return await DeleteAsync(new SystemMenu { MenuId = input.Id });
        }


        /// <summary>
        ///     查询所有具有菜单权限的菜单
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<JsTreeEntity>> GetHaveMenuPermissionMenu()
        {
            return await _menuRepository.GetHaveMenuPermissionMenu();
        }

        /// <summary>
        ///     查询所有具有数据权限的菜单
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<JsTreeEntity>> GetHaveDataPermissionMenu()
        {
            return await _menuRepository.GetHaveDataPermissionMenu();
        }

        /// <summary>
        ///     查询所有具有字段权限的菜单
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<JsTreeEntity>> GetHaveFieldPermissionMenu()
        {
            return await _menuRepository.GetHaveFieldPermissionMenu();
        }

        /// <summary>
        ///     查询所有具有功能项的菜单
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<JsTreeEntity>> GetHaveMenuButtonPermissionMenu()
        {
            return await _menuRepository.GetHaveMenuButtonPermissionMenu();
        }

        /// <summary>
        /// 获取显示在菜单列表上数据
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SystemMenuGetMenuByParentIdOutput>> GetMenuByParentId(SystemMenuGetMenuByParentIdInput input)
        {
            var menus = (await GetAllEnumerableAsync()).ToList();
            IList<SystemMenuGetMenuByParentIdOutput> systemMenus = (await _menuRepository.GetMenuByParentId(input)).ToList();
            foreach (var menu in systemMenus)
            {
                if (!menu.ParentIds.IsNullOrEmpty())
                {
                    foreach (var parent in menu.ParentIds.Split(','))
                    {
                        //查找上级
                        var dicinfo = menus.FirstOrDefault(w => w.MenuId.ToString() == parent);
                        if (dicinfo != null) menu.ParentNames += dicinfo.Name + ">";
                    }
                    if (!menu.ParentNames.IsNullOrEmpty())
                        menu.ParentNames = menu.ParentNames.TrimEnd('>');
                }
            }
            return systemMenus.OrderBy(o => o.ParentNames).ToList();
        }


        #region 级联删除Demo

        /// <summary>
        ///     删除菜单及下级数据
        /// </summary>
        /// <param name="input">父级id</param>
        /// <returns></returns>
        public async Task<OperateStatus> DeleteMenuAndChilds(IdInput<string> input)
        {
            var operateStatus = new OperateStatus();
            //判断该项能否进行删除
            var menu = await GetByIdAsync(input.Id);
            if (!menu.CanbeDelete)
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message = Chs.CanotDelete;
                return operateStatus;
            }
            foreach (var id in input.Id.Split(','))
            {
                Guid menuId = Guid.Parse(id);
                MenuDeletGuid.Add(menuId);
                await GetMenuDeleteGuid(menuId);
                foreach (var delete in MenuDeletGuid)
                {
                    await _permissionLogic.DeleteSystemPermissionsByPrivilegeAccessAndValue(EnumPrivilegeAccess.菜单, delete);
                    //删除对应按钮及按钮权限
                    var functions = await _menuButtonRepository.GetMenuButtonByMenuId(new SystemMenuGetMenuButtonByMenuIdInput{ Id = delete });
                    foreach (var item in functions)
                    {
                        await _permissionLogic.DeleteSystemPermissionsByPrivilegeAccessAndValue(EnumPrivilegeAccess.菜单按钮, item.MenuButtonId);
                        await _menuButtonRepository.DeleteAsync(new SystemMenuButton { MenuButtonId = item.MenuButtonId });
                    }
                    await DeleteAsync(new SystemMenu
                    {
                        MenuId = delete
                    });
                }
            }
            operateStatus.ResultSign = ResultSign.Successful;
            operateStatus.Message = Chs.Successful;
            return operateStatus;
        }

        /// <summary>
        ///     删除主键集合
        /// </summary>
        public IList<Guid> MenuDeletGuid = new List<Guid>();

        /// <summary>
        ///     获取删除主键信息
        /// </summary>
        /// <param name="guid"></param>
        private async Task GetMenuDeleteGuid(Guid guid)
        {
            //获取下级
            var menus = await _menuRepository.GetMeunuByPId(new IdInput(guid));
            if (menus.Count() > 0)
            {
                foreach (var dic in menus)
                {
                    MenuDeletGuid.Add(dic.MenuId);
                    await GetMenuDeleteGuid(dic.MenuId);
                }
            }
        }

        #endregion

        #endregion


    }
}