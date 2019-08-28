using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Tree;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Identity
{
    public interface ISystemOrganizationRepository : IAsyncRepository<SystemOrganization>
    {
        /// <summary>
        ///     根据父级查询下级
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetSystemOrganizationByPid(IdInput input);

        /// <summary>
        ///     获取所有组织机构信息
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetSystemOrganization();

        /// <summary>
        ///     根据父级查询下级
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemOrganizationOutput>> GetOrganizationsByParentId(SystemOrganizationDataPermissionTreeInput input);

        /// <summary>
        ///     根据父级查询下级
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemOrganizationOutput>> GetOrganizationsByParentId(SystemOrganizationsByParentIdInput input);

        /// <summary>
        /// 数据权限组织机构树
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetOrganizationResultByDataPermission(IdInput<string> input);

        /// <summary>
        /// 获取具有数据权限的组织机构数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<JsTreeEntity>> GetOrganizationDataPermissionTree(
            SystemOrganizationDataPermissionTreeInput input);
    }
}