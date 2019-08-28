using EIP.Common.Entities.Dtos;
using EIP.Common.Restful;
using EIP.Common.Restful.Attribute;
using EIP.System.Business.Permission;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EIP.Controllers
{
    /// <summary>
    ///     字段管理控制器
    /// </summary>
    [Authorize]
    public class FieldController : BaseController
    {
        #region 构造函数

        private readonly ISystemFieldLogic _fieldLogic;
        private readonly ISystemMenuLogic _menuLogic;

        public FieldController(ISystemFieldLogic fieldLogic, ISystemMenuLogic menuLogic)
        {
            _fieldLogic = fieldLogic;
            _menuLogic = menuLogic;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     根据菜单Id获取字段数据
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("字段维护-方法-列表-根据菜单Id获取字段数据")]
        public async Task<JsonResult> GetFieldByMenuId(SystemFieldPagingInput paging)
        {
            return JsonForGridPaging(await _fieldLogic.GetFieldByMenuId(paging));
        }

        /// <summary>
        ///     保存字段信息
        /// </summary>
        /// <param name="field">字段信息</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("字段维护-方法-新增/编辑-保存字段信息")]
        public async Task<JsonResult> SaveField(SystemField field)
        {
            return Json(await _fieldLogic.SaveField(field));
        }

        /// <summary>
        ///     根据字段Id删除字段信息
        /// </summary>
        /// <param name="input">字段Id</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("字段维护-方法-列表-删除字段信息")]
        public async Task<JsonResult> DeleteField(IdInput input)
        {
            return Json(await _fieldLogic.DeleteField(input));
        }

        #endregion
    }
}