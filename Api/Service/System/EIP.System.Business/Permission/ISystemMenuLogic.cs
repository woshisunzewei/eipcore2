using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Tree;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Permission
{
    public interface ISystemMenuLogic : IAsyncLogic<SystemMenu>
    {
        #region 菜单
        
        /// <summary>
        ///     根据状态为True的菜单信息
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetAllMenuTree();

        /// <summary>
        ///     查询所有具有菜单权限的菜单
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetHaveMenuPermissionMenu();

        /// <summary>
        ///     查询所有具有数据权限的菜单
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetHaveDataPermissionMenu();

        /// <summary>
        ///     查询所有具有字段权限的菜单
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetHaveFieldPermissionMenu();

        /// <summary>
        ///     查询所有具有功能项的菜单
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetHaveMenuButtonPermissionMenu();

        /// <summary>
        ///     根据状态为True的菜单信息
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SystemMenu>> GetMeunuByPId(IdInput input);

        /// <summary>
        ///     根据父级查询下级
        /// </summary>
        /// <param name="menu">父级id</param>
        /// <returns></returns>
        Task<OperateStatus> SaveMenu(SystemMenu menu);

        /// <summary>
        ///     删除菜单及下级数据
        /// </summary>
        /// <param name="input">父级id</param>
        /// <returns></returns>
        Task<OperateStatus> DeleteMenu(IdInput input);
        /// <summary>
        ///     删除菜单及下级数据
        /// </summary>
        /// <param name="input">父级id</param>
        /// <returns></returns>
        Task<OperateStatus> DeleteMenuAndChilds(IdInput<string> input);
      
        /// <summary>
        /// 获取显示在菜单列表上数据
        /// </summary>
        /// <returns></returns>
        Task<IList<SystemMenuGetMenuByParentIdOutput>> GetMenuByParentId(SystemMenuGetMenuByParentIdInput input);
        #endregion
    }
}