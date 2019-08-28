using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Tree;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Permission
{
    public interface ISystemMenuRepository : IAsyncRepository<SystemMenu>
    {
       
        /// <summary>
        ///     查询所有菜单
        /// </summary>
        /// <param name="haveUrl">是否具有菜单</param>
        /// <param name="isMenuShow"></param>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetAllMenuTree(bool haveUrl = false,
            bool isMenuShow = false);

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
        ///     根据父级查询下级
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SystemMenu>> GetMeunuByPId(IdInput input);

        /// <summary>
        /// 根据父级获取下面菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemMenuGetMenuByParentIdOutput>> GetMenuByParentId(SystemMenuGetMenuByParentIdInput input);
    }
}