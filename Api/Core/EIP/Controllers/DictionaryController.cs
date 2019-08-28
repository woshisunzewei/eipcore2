using EIP.Common.Entities.Dtos;
using EIP.Common.Restful;
using EIP.Common.Restful.Attribute;
using EIP.System.Business.Config;
using EIP.System.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using EIP.Common.Core.Auth;
using EIP.Common.Restful.Extension;
using EIP.System.Models.Dtos.Config;
using Microsoft.AspNetCore.Http;

namespace EIP.Controllers
{
    /// <summary>
    ///     字典控制器
    /// </summary>
    [Authorize]
    public class DictionaryController : BaseController
    {
        #region 构造函数
        private readonly PrincipalUser _currentUser;
        private readonly ISystemDictionaryLogic _dictionaryLogic;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionaryLogic"></param>
        /// <param name="httpContextAccessor"></param>
        public DictionaryController(ISystemDictionaryLogic dictionaryLogic,
            IHttpContextAccessor httpContextAccessor)
        {
            _currentUser = httpContextAccessor.CurrentUser();
            _dictionaryLogic = dictionaryLogic;
        }
        #endregion

        #region 方法
        /// <summary>
        ///     根据id获取
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("字典信息维护-方法-根据id获取")]
        public async Task<JsonResult> GetById(IdInput input)
        {
            return Json(await _dictionaryLogic.GetById(input));
        }

        /// <summary>
        ///     查询所有字典信息:Ztree格式
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("字典信息维护-方法-列表-查询所有字典信息")]
        public async Task<JsonResult> GetDictionaryTree()
        {
            return JsonForJsTree(await _dictionaryLogic.GetDictionaryTree());
        }

        /// <summary>
        ///     根据父级Id读取子字典列表信息
        /// </summary>
        /// <param name="input">父级id</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("字典信息维护-方法-列表-根据父级Id读取子字典列表信息")]
        public async Task<JsonResult> GetDictionariesByParentId(SystemDictionaryGetByParentIdInput input)
        {
            return JsonForGridLoadOnce(await _dictionaryLogic.GetDictionariesParentId(input));
        }

        /// <summary>
        /// 根据ParentIds获取所有下级
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("字典信息维护-方法-列表-根据ParentIds获取所有下级")]
        public async Task<JsonResult> GetDictionaryTreeByParentIds(IdInput input)
        {
            return JsonForJsTree((await _dictionaryLogic.GetDictionaryTreeByParentIds(input)).ToList());
        }

         /// <summary>
        /// 根据ParentIds获取所有下级
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("字典信息维护-方法-列表-根据ParentIds获取所有下级")]
        public async Task<JsonResult> GetDictionaryTreeByParentIdsHaveParent(IdInput input)
        {
            return Json((await _dictionaryLogic.GetMongoDbDictionaryTreeByParentIds(input)).ToList());
        }

        /// <summary>
        /// 根据ParentId获取所有下级
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("字典信息维护-方法-列表-根据ParentId获取所有下级")]
        public async Task<JsonResult> GetDictionarieByParentId(IdInput input)
        {
            return Json((await _dictionaryLogic.GetDictionarieByParentId(input)).ToList());
        }
        /// <summary>
        ///     保存字典数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("字典信息维护-方法-新增/编辑-保存字典数据")]
        public async Task<JsonResult> SaveDictionary(SystemDictionary dictionary)
        {
            dictionary.CreateUserId = _currentUser.UserId;
            dictionary.CreateUserName = _currentUser.Name;
            return Json(await _dictionaryLogic.SaveDictionary(dictionary));
        }

        /// <summary>
        ///     删除字典数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("字典信息维护-方法-列表-删除")]
        public async Task<JsonResult> DeleteDictionary(IdInput<string> input)
        {
            return Json(await _dictionaryLogic.DeleteDictionary(input));
        }
        #endregion
    }
}