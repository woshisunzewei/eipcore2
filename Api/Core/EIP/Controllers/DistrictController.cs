using EIP.Common.Core.Extensions;
using EIP.Common.Entities.Dtos;
using EIP.Common.Restful;
using EIP.Common.Restful.Attribute;
using EIP.System.Business.Config;
using EIP.System.Models.Dtos.Config;
using EIP.System.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EIP.Controllers
{
    /// <summary>
    ///     省市县管理控制器
    /// </summary>
    [Authorize]
    public class DistrictController : BaseController
    {
        #region 构造函数

        private readonly ISystemDistrictLogic _districtLogic;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="districtLogic"></param>
        public DistrictController(ISystemDistrictLogic districtLogic)
        {
            _districtLogic = districtLogic;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     根据父级查询所有子集
        /// </summary>
        /// <param name="input">父级</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("省市县维护-方法-列表-根据父级查询所有子集")]
        public async Task<JsonResult> GetDistrictTreeByParentId(IdInput<string> input)
        {
            return Json(await _districtLogic.GetDistrictTreeByParentId(input));
        }

        /// <summary>
        ///     根据父级查询所有子集
        /// </summary>
        /// <param name="input">父级</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("省市县维护-方法-列表-根据父级查询所有子集")]
        public async Task<JsonResult> GetDistrictByParentId(IdInput<string> input)
        {
            return JsonForGridLoadOnce(await _districtLogic.GetDistrictByParentId(input));
        }
        /// <summary>
        ///     根据县Id获取省市县Id
        /// </summary>
        /// <param name="input">县Id</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("省市县维护-方法-列表-根据Id获取")]
        public async Task<JsonResult> GetDistrictById(IdInput<string> input)
        {
            SystemDistrictGetByIdOutput output = new SystemDistrictGetByIdOutput();
            var dis = await _districtLogic.GetByIdAsync(input.Id);
            if (dis != null)
            {
                var parentDis = await _districtLogic.GetByIdAsync(dis.ParentId);
                output = dis.MapTo<SystemDistrictGetByIdOutput>();
                output.ParentName = parentDis.Name;
            }
            return Json(output);
        }
        /// <summary>
        ///     根据县Id获取省市县Id
        /// </summary>
        /// <param name="input">县Id</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("省市县维护-方法-列表-根据县Id获取省市县Id")]
        public async Task<JsonResult> GetDistrictByCountId(IdInput<string> input)
        {
            return Json(input.Id.IsNullOrEmpty()
                ? new SystemDistrict()
                : await _districtLogic.GetDistrictByCountId(input));
        }

        /// <summary>
        ///     保存省市县信息
        /// </summary>
        /// <param name="district">省市县信息</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("省市县维护-方法-新增/编辑-保存省市县信息")]
        public async Task<JsonResult> SaveDistrict(SystemDistrict district)
        {
            return Json(await _districtLogic.SaveDistrict(district));
        }

        /// <summary>
        ///     检测代码是否已经具有重复项
        /// </summary>
        /// <param name="input">需要验证的参数</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("省市县维护-方法-新增/编辑-检测代码是否已经具有重复项")]
        public async Task<JsonResult> CheckDistrictId(CheckSameValueInput input)
        {
            return JsonForCheckSameValue(await _districtLogic.CheckDistrictId(input));
        }

        /// <summary>
        ///     删除省市县及下级数据
        /// </summary>
        /// <param name="input">父级id</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("省市县维护-方法-列表-删除省市县及下级数据")]
        public async Task<JsonResult> DeleteDistrict(IdInput<string> input)
        {
            return Json(await _districtLogic.DeleteDistrict(input));
        }

        #endregion
    }
}