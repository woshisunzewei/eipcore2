using EIP.Common.Entities;
using EIP.Common.Entities.Paging;
using EIP.Common.Entities.Tree;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace EIP.Common.Restful
{
    /// <summary>
    /// 继承BaseController都必须要继续授权验证
    /// </summary>
    [Route("api/[controller]/[action]")]
    [EnableCors("EIPCors")]//Cors跨域
    [Authorize]
    public class BaseController : Controller
    {
        #region Json
        
        /// <summary>
        ///     读取树结构:排除下级
        /// </summary>
        /// <param name="treeEntitys">TreeEntity的集合</param>
        /// <param name="id">需要排除下级的id</param>
        /// <returns></returns>
        protected JsonResult JsonForJstreeRemoveChildren(IList<JsTreeEntity> treeEntitys, Guid? id = null)
        {
            if (treeEntitys == null || treeEntitys.Count <= 0)
            {
                treeEntitys = new List<JsTreeEntity>();
                return Json(treeEntitys);
            }
            IList<JsTreeEntity> returnTreeEntities = new List<JsTreeEntity>(treeEntitys.Count);
            if (id == null)
            {
                foreach (var treeEntity in treeEntitys)
                {
                    returnTreeEntities.Add(new JsTreeEntity
                    {
                        id = treeEntity.id,
                        parent = Guid.Parse(treeEntity.parent.ToString()) == Guid.Empty ? "#" : treeEntity.parent,
                        icon = treeEntity.icon,
                        text = treeEntity.text,
                        state = new JsTreeStateEntity()
                    });
                }
                return Json(returnTreeEntities);
            }
            //判断有多少个模块
            IList<JsTreeEntity> roots = treeEntitys.Where(f => f.parent.ToString() == Guid.Empty.ToString()).ToList();
            if (roots.Count <= 0)
            {
                treeEntitys = new List<JsTreeEntity>();
                return Json(treeEntitys);
            }
            
            //便利子模块
            foreach (var permission in roots)
            {
                if (id.ToString() != permission.id.ToString())
                {
                    returnTreeEntities.Add(new JsTreeEntity
                    {
                        id = Guid.Parse(permission.id.ToString()),
                        parent = "#",
                        icon = permission.icon,
                        text = permission.text,
                        state = new JsTreeStateEntity()
                    });

                    //判断有多少个模块
                    IList<JsTreeEntity> perRoots =
                        treeEntitys.Where(f => f.parent.ToString() == permission.id.ToString()).ToList();
                    foreach (var treeEntity in perRoots)
                    {
                        if (id.ToString() != treeEntity.id.ToString())
                        {
                            returnTreeEntities.Add(new JsTreeEntity
                            {
                                id = treeEntity.id,
                                parent = treeEntity.parent,
                                icon = treeEntity.icon,
                                text = treeEntity.text,
                                state = new JsTreeStateEntity()
                            });
                            GetJstreeChildNodesChildren(ref treeEntitys, ref returnTreeEntities, treeEntity, id);
                        }
                    }
                }
            }
            return Json(returnTreeEntities);
        }

        /// <summary>
        ///     根据当前节点，加载子节点
        /// </summary>
        /// <param name="treeEntitys">TreeEntity的集合</param>
        /// <param name="returnTreeEntities"></param>
        /// <param name="currTreeEntity">当前节点</param>
        /// <param name="id">需要排除下级的id</param>
        private void GetJstreeChildNodesChildren(ref IList<JsTreeEntity> treeEntitys,
            ref IList<JsTreeEntity> returnTreeEntities,
            JsTreeEntity currTreeEntity
            , Guid? id = null)
        {
            IList<JsTreeEntity> childNodes =
                treeEntitys.Where(f => f.parent.ToString() == currTreeEntity.id.ToString()).ToList();
            foreach (var treeEntity in childNodes)
            {
                if (id.ToString() != treeEntity.id.ToString())
                {
                    returnTreeEntities.Add(new JsTreeEntity
                    {
                        id = treeEntity.id,
                        parent = treeEntity.parent,
                        icon = treeEntity.icon,
                        text = treeEntity.text,
                        state = new JsTreeStateEntity()
                    });
                    GetJstreeChildNodesChildren(ref treeEntitys, ref returnTreeEntities, treeEntity, id);
                }
            }
        }

        /// <summary>
        ///     返回分页后信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pagedResults"></param>
        /// <returns></returns>
        protected JsonResult JsonForGridPaging<T>(PagedResults<T> pagedResults)
        {
            return Json(new
            {
                code = 0,
                msg = "",
                page = pagedResults.PagerInfo?.Page ?? 0,
                count = pagedResults.PagerInfo?.RecordCount ?? 0,
                data = pagedResults.Data
            });
        }

        /// <summary>
        ///     返回一次性数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <returns></returns>
        protected JsonResult JsonForGridLoadOnce<T>(IEnumerable<T> datas)
        {
            var enumerable = datas as IList<T> ?? datas.ToList();
            return Json(new
            {
                code = 0,
                msg = "",
                count = enumerable.ToList().Count,
                data = enumerable
            });
        }

        /// <summary>
        ///     将Ztree返回数据转换为JsTree
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        protected JsonResult JsonForJsTree(IEnumerable<JsTreeEntity> datas)
        {
            foreach (var data in datas)
            {
                data.parent = Guid.Parse(data.parent.ToString()) == Guid.Empty ? "#" : data.parent;
                data.state = new JsTreeStateEntity();
            }
            return Json(datas);
        }

        /// <summary>
        ///     检查值是否相同
        /// </summary>
        /// <param name="operateStatus"></param>
        /// <returns></returns>
        protected JsonResult JsonForCheckSameValue(OperateStatus operateStatus)
        {
            return Json(new
            {
                info = operateStatus.Message,
                status = operateStatus.ResultSign == ResultSign.Successful ? "y" : "n"
            });
        }

        /// <summary>
        ///     检查值是否相同
        /// </summary>
        /// <param name="operateStatus"></param>
        /// <returns></returns>
        protected JsonResult JsonForCheckSameValueValidator(OperateStatus operateStatus)
        {
            return Json(new
            {
                valid = operateStatus.ResultSign == ResultSign.Successful
            });
        }

        #endregion

        #region 扩展
        /// <summary>
        /// 检查Model参数
        /// </summary>
        /// <returns></returns>
        protected OperateStatus CheckModelState()
        {
            OperateStatus operateStatus=new OperateStatus();
            if (!ModelState.IsValid)
            {
                var msg = string.Empty;
                foreach (var value in ModelState.Values)
                {
                    if (value.Errors.Count > 0)
                    {
                        foreach (var error in value.Errors)
                        {
                            msg = msg + error.ErrorMessage + ",";
                        }
                    }
                }
                operateStatus.Message = msg.TrimEnd(',');
            }
            else
            {
                operateStatus.ResultSign = ResultSign.Successful;
            }
            return operateStatus;
        }

        #endregion
    }
}