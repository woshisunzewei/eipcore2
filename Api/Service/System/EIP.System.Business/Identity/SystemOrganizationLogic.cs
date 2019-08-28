using EIP.Common.Business;
using EIP.Common.Core.Extensions;
using EIP.Common.Core.Resource;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Tree;
using EIP.System.Business.Permission;
using EIP.System.DataAccess.Identity;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using EIP.System.Models.Enums;
using EIP.System.Models.Resx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIP.System.Business.Identity
{
    public class SystemOrganizationLogic : DapperAsyncLogic<SystemOrganization>, ISystemOrganizationLogic
    {
        #region 构造函数

        private readonly ISystemOrganizationRepository _organizationRepository;
        private readonly ISystemPermissionUserLogic _permissionUserLogic;
        private readonly ISystemPermissionLogic _permissionLogic;
        private readonly ISystemGroupLogic _groupLogic;
        private readonly ISystemRoleLogic _roleLogic;
        private readonly ISystemPostLogic _postLogic;
        public SystemOrganizationLogic(ISystemOrganizationRepository organizationRepository,
            ISystemPermissionUserLogic permissionUserLogic,
            ISystemPermissionLogic permissionLogic,
            ISystemGroupLogic groupLogic,
            ISystemRoleLogic roleLogic,
            ISystemPostLogic postLogic)
            : base(organizationRepository)
        {
            _permissionUserLogic = permissionUserLogic;
            _permissionLogic = permissionLogic;
            _groupLogic = groupLogic;
            _roleLogic = roleLogic;
            _postLogic = postLogic;
            _organizationRepository = organizationRepository;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     异步读取树数据
        /// </summary>
        /// <param name="input">父级id</param>
        /// <returns></returns>
        public async Task<IEnumerable<JsTreeEntity>> GetOrganizationTreeAsync(IdInput input)
        {
            var lists = (await _organizationRepository.GetSystemOrganizationByPid(input)).ToList();
            foreach (var list in lists)
            {
                var info = (await _organizationRepository.GetSystemOrganizationByPid(input)).ToList();
                if (info.Count > 0)
                {
                    list.parent = true;
                }
            }
            return lists;
        }

        /// <summary>
        ///     读取树数据
        /// </summary>
        /// <param name="input">父级id</param>
        /// <returns></returns>
        public async Task<IEnumerable<JsTreeEntity>> GetSystemOrganizationByPid(IdInput input)
        {
            return (await _organizationRepository.GetSystemOrganizationByPid(input)).ToList();
        }

        /// <summary>
        ///     同步读取所有树数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<JsTreeEntity>> GetOrganizationTree()
        {
            return await _organizationRepository.GetSystemOrganization();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SystemOrganizationChartOutput>> GetOrganizationChart()
        {
            var allOrgs = (await GetAllEnumerableAsync()).ToList();
            var topOrgs = allOrgs.Where(w => w.ParentId == Guid.Empty).ToList();
            //总模块
            IList<SystemOrganizationChartOutput> outputs = new List<SystemOrganizationChartOutput>(topOrgs.Count);
            foreach (var root in topOrgs)
            {
                outputs.Add(new SystemOrganizationChartOutput
                {
                    id = root.OrganizationId,
                    name = root.Name,
                    title = root.MainSupervisor.IsNullOrEmpty() ? "" : root.MainSupervisor
                });
            }
            //便利子模块
            foreach (var permission in outputs)
            {
                //判断有多少个模块
                IList<SystemOrganization> perRoots =
                    allOrgs.Where(f => f.ParentId.ToString() == permission.id.ToString()).ToList();
                IList<SystemOrganizationChartOutput> trees = new List<SystemOrganizationChartOutput>();
                SystemOrganizationChartOutput tree = null;
                foreach (var treeEntity in perRoots)
                {
                    tree = new SystemOrganizationChartOutput
                    {
                        name = treeEntity.Name,
                        title = treeEntity.MainSupervisor.IsNullOrEmpty() ? "" : treeEntity.MainSupervisor,
                        children = GetWdChildNodes(ref allOrgs, treeEntity)
                    };
                    trees.Add(tree);
                }
                permission.children = trees;
                tree = null;
            }
            return outputs;
        }

        /// <summary>
        ///     根据当前节点，加载子节点
        /// </summary>
        /// <param name="treeEntitys">TreeEntity的集合</param>
        /// <param name="currTreeEntity">当前节点</param>
        private IList<SystemOrganizationChartOutput> GetWdChildNodes(ref List<SystemOrganization> treeEntitys,
            SystemOrganization currTreeEntity)
        {
            IList<SystemOrganization> childNodes =
                treeEntitys.Where(f => f.ParentId.ToString() == currTreeEntity.OrganizationId.ToString()).ToList();
            if (childNodes.Count <= 0)
            {
                return null;
            }
            IList<SystemOrganizationChartOutput> childTrees = new List<SystemOrganizationChartOutput>(childNodes.Count);
            SystemOrganizationChartOutput tree = null;
            foreach (var treeEntity in childNodes)
            {
                tree = new SystemOrganizationChartOutput
                {
                    name = treeEntity.Name,
                    title = treeEntity.MainSupervisor.IsNullOrEmpty() ? "" : treeEntity.MainSupervisor,
                    children = GetWdChildNodes(ref treeEntitys, treeEntity)
                };
                childTrees.Add(tree);
            }
            return childTrees;
        }

        /// <summary>
        ///     所有数据权限组织机构
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<JsTreeEntity>> GetOrganizationDataPermissionTree(SystemOrganizationDataPermissionTreeInput input)
        {
            if (input.DataSql.IsNullOrEmpty())
                return new List<JsTreeEntity>();
            var datas = (await _organizationRepository.GetOrganizationDataPermissionTree(input)).ToList();
            if (!input.DataSql.IsNullOrEmpty() && !input.DataSql.Contains("1=1"))
            {
                foreach (var data in datas)
                {
                    data.state = new JsTreeStateEntity();
                    if (data.id.ToString() == input.PrincipalUser.OrganizationId.ToString())
                    {
                        data.parent = "#";
                    }
                }
            }
            else
            {
                foreach (var d in datas)
                {
                    if (d.parent.ToString() == Guid.Empty.ToString())
                    {
                        d.parent = "#";
                    }
                    d.state = new JsTreeStateEntity();
                }
            }
            return datas;
        }

        /// <summary>
        ///     根据父级查询下级
        /// </summary>
        /// <param name="input">父级id</param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemOrganizationOutput>> GetOrganizationsByParentId(SystemOrganizationDataPermissionTreeInput input)
        {
            if (input.DataSql.IsNullOrEmpty())
                return new List<SystemOrganizationOutput>();
            var allOrgs = (await GetAllEnumerableAsync()).ToList();
            IList<SystemOrganizationOutput> organizations = (await _organizationRepository.GetOrganizationsByParentId(input)).ToList();
            foreach (var organization in organizations)
            {
                if (!organization.ParentIds.IsNullOrEmpty())
                {

                    foreach (var parent in organization.ParentIds.Split(','))
                    {
                        //查找上级
                        var dicinfo = allOrgs.FirstOrDefault(w => w.OrganizationId.ToString() == parent);
                        if (dicinfo != null) organization.ParentNames += dicinfo.Name + ">";
                    }
                    if (!organization.ParentNames.IsNullOrEmpty())
                        organization.ParentNames = organization.ParentNames.TrimEnd('>');
                }
            }
            return organizations;
        }

        /// <summary>
        ///     保存组织机构
        /// </summary>
        /// <param name="organization">组织机构</param>
        /// <returns></returns>
        public async Task<OperateStatus> SaveOrganization(SystemOrganization organization)
        {
            OperateStatus operateStatus;
            if (organization.OrganizationId.IsEmptyGuid())
            {
                organization.CreateTime = DateTime.Now;
                organization.OrganizationId = Guid.NewGuid();
                operateStatus = await InsertAsync(organization);
            }
            else
            {
                organization.UpdateTime = DateTime.Now;
                organization.UpdateUserId = organization.CreateUserId;
                organization.UpdateUserName = organization.CreateUserName;
                SystemOrganization systemOrganization = await GetByIdAsync(organization.OrganizationId);
                organization.CreateTime = systemOrganization.CreateTime;
                organization.CreateUserId = systemOrganization.CreateUserId;
                organization.CreateUserName = systemOrganization.CreateUserName;
                operateStatus = await UpdateAsync(organization);
            }
            GeneratingParentIds();
            return operateStatus;
        }

        /// <summary>
        ///     删除组织机构下级数据
        ///     删除条件:
        ///     1:没有下级菜单
        ///     2:没有权限数据
        ///     3:没有人员
        /// </summary>
        /// <param name="input">组织机构id</param>
        /// <returns></returns>
        public async Task<OperateStatus> DeleteOrganization(IdInput input)
        {
            var operateStatus = new OperateStatus();
            //判断下级菜单
            IList<JsTreeEntity> orgs = (await _organizationRepository.GetSystemOrganizationByPid(input)).ToList();
            if (orgs.Any())
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message = ResourceSystem.具有下级项;
                return operateStatus;
            }

            //判断是否具有人员
            var permissionUsers = await
                _permissionUserLogic.GetPermissionUsersByPrivilegeMasterAdnPrivilegeMasterValue(
                    EnumPrivilegeMaster.组织机构,
                    input.Id);
            if (permissionUsers.Any())
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message = ResourceSystem.具有人员;
                return operateStatus;
            }

            //判断是否有角色
            var orgRole = await
               _roleLogic.GetRolesByOrganizationId(new SystemRolesGetByOrganizationId { Id = input.Id });
            if (orgRole.Any())
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message = ResourceSystem.具有角色;
                return operateStatus;
            }

            //判断是否有组
            var orgGroup = await
                _groupLogic.GetGroupByOrganizationId(new SystemGroupGetGroupByOrganizationIdInput
                {
                    Id = input.Id
                });
            if (orgGroup.Any())
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message = ResourceSystem.具有组;
                return operateStatus;
            }

            //判断是否有岗位
            var orgPost = await
               _postLogic.GetPostByOrganizationId(new SystemPostGetByOrganizationId { Id = input.Id });
            if (orgPost.Any())
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message = ResourceSystem.具有岗位;
                return operateStatus;
            }

            //判断是否具有按钮权限
            var functionPermissions = await
                _permissionLogic.GetPermissionByPrivilegeMasterValue(
                  new SystemPermissionByPrivilegeMasterValueInput()
                  {
                      PrivilegeAccess = EnumPrivilegeAccess.菜单按钮,
                      PrivilegeMasterValue = input.Id,
                      PrivilegeMaster = EnumPrivilegeMaster.组织机构
                  });
            if (functionPermissions.Any())
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message = ResourceSystem.具有功能项权限;
                return operateStatus;
            }
            //判断是否具有菜单权限
            var menuPermissions = await
                _permissionLogic.GetPermissionByPrivilegeMasterValue(
                 new SystemPermissionByPrivilegeMasterValueInput()
                 {
                     PrivilegeAccess = EnumPrivilegeAccess.菜单,
                     PrivilegeMasterValue = input.Id,
                     PrivilegeMaster = EnumPrivilegeMaster.组织机构
                 });
            if (menuPermissions.Any())
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message = ResourceSystem.具有菜单权限;
                return operateStatus;
            }
            //进行删除操作
            return await DeleteAsync(new SystemOrganization()
            {
                OrganizationId = input.Id
            });
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
                var dics = (await GetAllEnumerableAsync()).ToList();
                var topOrgs = dics.Where(w => w.ParentId == Guid.Empty);
                foreach (var org in topOrgs)
                {
                    org.ParentIds = org.OrganizationId.ToString();
                    await UpdateAsync(org);
                    await GeneratingParentIds(org, dics.ToList(), "");
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
        /// <param name="organization"></param>
        /// <param name="organizations"></param>
        /// <param name="orgId"></param>
        private async Task GeneratingParentIds(SystemOrganization organization, IList<SystemOrganization> organizations, string orgId)
        {
            string parentIds = organization.OrganizationId.ToString();
            //获取下级
            var nextOrgs = organizations.Where(w => w.ParentId == organization.OrganizationId).ToList();
            if (nextOrgs.Any())
            {
                parentIds = orgId.IsNullOrEmpty() ? parentIds : orgId + "," + parentIds;
            }
            foreach (var or in nextOrgs)
            {
                or.ParentIds = parentIds + "," + or.OrganizationId;
                await UpdateAsync(or);
                await GeneratingParentIds(or
                    , organizations, parentIds);
            }
        }

        /// <summary>
        /// 数据权限组织机构树
        /// </summary>
        ///  <param name="input"></param>
        /// <returns></returns>
        public async Task<IEnumerable<JsTreeEntity>> GetOrganizationResultByDataPermission(IdInput<string> input)
        {
            return await _organizationRepository.GetOrganizationResultByDataPermission(input);
        }
        #endregion
    }
}