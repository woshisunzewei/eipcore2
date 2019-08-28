using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Tree;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Identity
{
    public interface ISystemOrganizationLogic : IAsyncLogic<SystemOrganization>
    {
        /// <summary>
        ///     异步读取树数据
        /// </summary>
        /// <param name="input">父级id</param>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetOrganizationTreeAsync(IdInput input);

        /// <summary>
        /// 获取组织机构信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetSystemOrganizationByPid(IdInput input);

        /// <summary>
        /// 获取具有数据权限的组织机构数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetOrganizationDataPermissionTree(
            SystemOrganizationDataPermissionTreeInput input);

        /// <summary>
        ///     同步读取所有树数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetOrganizationTree();

        /// <summary>
        ///     同步读取所有树数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SystemOrganizationChartOutput>> GetOrganizationChart();

        /// <summary>
        ///     根据父级查询下级
        /// </summary>
        /// <param name="input">父级id</param>
        /// <returns></returns>
        Task<IEnumerable<SystemOrganizationOutput>> GetOrganizationsByParentId(SystemOrganizationDataPermissionTreeInput input);

        /// <summary>
        ///     保存组织机构
        /// </summary>
        /// <param name="organization">组织机构</param>
        /// <returns></returns>
        Task<OperateStatus> SaveOrganization(SystemOrganization organization);

        /// <summary>
        ///     删除组织机构下级数据
        /// </summary>
        /// <param name="input">父级id</param>
        /// <returns></returns>
        Task<OperateStatus> DeleteOrganization(IdInput input);

        /// <summary>
        /// 数据权限组织机构树
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetOrganizationResultByDataPermission(IdInput<string> input);

    }
}