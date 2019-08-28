using EIP.Common.Entities.Dtos;
using EIP.Common.Restful;
using EIP.Common.Restful.Attribute;
using EIP.System.Business.Permission;
using EIP.System.Models.Dtos.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EIP.Controllers
{
    /// <summary>
    ///     数据权限控制器
    /// </summary>
    [Authorize]
    public class DataController : BaseController
    {
        #region 构造函数

        private readonly ISystemDataLogic _dataLogic;
        private readonly ISystemMenuLogic _menuLogic;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataLogic"></param>
        /// <param name="menuLogic"></param>
        public DataController(ISystemDataLogic dataLogic, ISystemMenuLogic menuLogic)
        {
            _dataLogic = dataLogic;
            _menuLogic = menuLogic;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     根据菜单Id获取数据权限规则
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("配置信息-方法-列表-根据菜单Id获取数据权限规则")]
        public async Task<JsonResult> GetDataByMenuId(SystemDataGetDataByMenuIdInput input = null)
        {
            return JsonForGridLoadOnce(await _dataLogic.GetDataByMenuId(input));
        }

        /// <summary>
        ///     保存数据权限规则
        /// </summary>
        /// <param name="doubleWayDto">数据权限规则</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("配置信息-方法-新增/编辑-保存数据权限规则")]
        public async Task<JsonResult> SaveData(SystemDataOutput doubleWayDto)
        {
            return Json(await _dataLogic.SaveData(doubleWayDto));
        }

        /// <summary>
        ///     根据字段Id删除数据权限规则
        /// </summary>
        /// <param name="input">数据权限规则Id</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("配置信息-方法-列表-删除")]
        public async Task<JsonResult> DeleteByDataId(IdInput input)
        {
            return Json(await _dataLogic.DeleteByDataId(input));
        }
        #endregion
    }
}