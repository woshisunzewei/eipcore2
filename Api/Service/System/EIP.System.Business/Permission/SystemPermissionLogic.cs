using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Core.Extensions;
using EIP.Common.Core.Resource;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Tree;
using EIP.System.DataAccess.Identity;
using EIP.System.DataAccess.Permission;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using EIP.System.Models.Enums;
using Newtonsoft.Json;

namespace EIP.System.Business.Permission
{
    /// <summary>
    ///     权限业务逻辑
    /// </summary>
    public class SystemPermissionLogic : DapperAsyncLogic<SystemPermission>, ISystemPermissionLogic
    {
        #region 构造函数

        /// <summary>
        ///     无参构造函数
        /// </summary>
        public SystemPermissionLogic(ISystemOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
            _menuButtonRepository = new SystemMenuButtonRepository();
            _userInfoRepository = new SystemUserInfoRepository();
        }

        #region 依赖注入

        private readonly ISystemMenuButtonRepository _menuButtonRepository;
        private readonly ISystemPermissionRepository _permissionRepository;
        private readonly ISystemPermissionUserRepository _permissionUsernRepository;
        private readonly ISystemUserInfoRepository _userInfoRepository;
        private readonly ISystemMenuRepository _menuRepository;
        private readonly ISystemDataRepository _dataRepository;
        private readonly ISystemOrganizationRepository _organizationRepository;
        public SystemPermissionLogic(ISystemMenuButtonRepository menuButtonRepository,
            ISystemPermissionRepository permissionRepository,
            ISystemPermissionUserRepository permissionUserRepository,
            ISystemUserInfoRepository userInfoRepository,
            ISystemMenuRepository menuRepository,
            ISystemDataRepository dataRepository, ISystemOrganizationRepository organizationRepository)
            : base(permissionRepository)
        {
            _menuButtonRepository = menuButtonRepository;
            _permissionRepository = permissionRepository;
            _permissionUsernRepository = permissionUserRepository;
            _userInfoRepository = userInfoRepository;
            _menuRepository = menuRepository;
            _dataRepository = dataRepository;
            _organizationRepository = organizationRepository;
        }

        #endregion

        #endregion

        #region 方法

        /// <summary>
        ///     根据状态为True的菜单信息
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SystemPermission>> GetPermissionByPrivilegeMasterValue(SystemPermissionByPrivilegeMasterValueInput input)
        {
            return (await _permissionRepository.GetPermissionByPrivilegeMasterValue(input)).ToList();
        }

        /// <summary>
        ///     保存权限信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OperateStatus> SavePermission(SystemPermissionSaveInput input)
        {
            var operateStatus = new OperateStatus();
            try
            {
                IList<SystemPermission> systemPermissions = input.Permissiones.Select(per => new SystemPermission
                {
                    PrivilegeAccess = (byte)input.PrivilegeAccess,
                    PrivilegeAccessValue = per,
                    PrivilegeMasterValue = input.PrivilegeMasterValue,
                    PrivilegeMaster = (byte)input.PrivilegeMaster,
                    PrivilegeMenuId = input.PrivilegeMenuId
                }).ToList();

                //删除该角色的权限信息
                await _permissionRepository.DeletePermissionByPrivilegeMasterValue(input.PrivilegeAccess, input.PrivilegeMasterValue, input.PrivilegeMenuId);
                if (input.PrivilegeMaster == EnumPrivilegeMaster.人员)
                {
                    //删除对应人员数据
                    await _permissionUsernRepository.DeletePermissionUser(input.PrivilegeMaster, input.PrivilegeMasterValue);
                    //判断是否具有权限
                    if (!systemPermissions.Any())
                    {
                        operateStatus.ResultSign = ResultSign.Successful;
                        operateStatus.Message = Chs.Successful;
                        return operateStatus;
                    }
                    //插入权限人员数据
                    var permissionUser = new SystemPermissionUser
                    {
                        PrivilegeMaster = (byte)input.PrivilegeMaster,
                        PrivilegeMasterUserId = input.PrivilegeMasterValue,
                        PrivilegeMasterValue = input.PrivilegeMasterValue
                    };
                    await _permissionUsernRepository.InsertAsync(permissionUser);
                }

                //是否具有权限数据
                if (!systemPermissions.Any())
                {
                    operateStatus.ResultSign = ResultSign.Successful;
                    operateStatus.Message = Chs.Successful;
                    return operateStatus;
                }
                //插入数据库
                await _permissionRepository.BulkInsertAsync(systemPermissions);
                operateStatus.ResultSign = ResultSign.Successful;
                operateStatus.Message = Chs.Successful;
                return operateStatus;
            }
            catch (Exception ex)
            {
                operateStatus.Message = ex.Message;
                return operateStatus;
            }
        }

        /// <summary>
        ///     根据用户Id获取用户具有的菜单权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<JsTreeEntity>> GetSystemPermissionMenuByUserId(Guid userId)
        {
            IList<JsTreeEntity> treeEntities = new List<JsTreeEntity>();
            //判断该用户是否为超级管理员:若是超级管理员则显示所有菜单
            var userInfo = await _userInfoRepository.FindByIdAsync(userId);
            if (userInfo != null)
            {
                //如果是超级管理员
                if (userInfo.IsAdmin)
                {
                    treeEntities = (await _menuRepository.GetAllMenuTree(true, true)).ToList();
                    return treeEntities;
                }
                treeEntities = (await _permissionRepository.GetSystemPermissionMenuByUserId(userId)).ToList();
            }
            return treeEntities;
        }

        /// <summary>
        ///     根据角色Id,岗位Id,组Id,人员Id获取具有的菜单信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns>树形菜单信息</returns>
        public async Task<IEnumerable<JsTreeEntity>> GetMenuHavePermissionByPrivilegeMasterValue(SystemPermissiontMenuHaveByPrivilegeMasterValueInput input)
        {
            return (await _permissionRepository.GetMenuHavePermissionByPrivilegeMasterValue(input)).ToList();
        }

        /// <summary>
        ///     查询对应拥有的功能项菜单信息
        /// </summary>
        /// <param name="privilegeMasterValue">信息</param>
        /// <param name="privilegeMaster"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemMenuButton>> GetFunctionByPrivilegeMaster(Guid privilegeMasterValue,
            EnumPrivilegeMaster privilegeMaster)
        {
            IList<SystemMenuButton> functions = new List<SystemMenuButton>();
            //获取拥有的菜单信息
            var menus = await GetMenuHavePermissionByPrivilegeMasterValue(new SystemPermissiontMenuHaveByPrivilegeMasterValueInput
            {
                PrivilegeMasterValue = privilegeMasterValue,
                PrivilegeMaster = privilegeMaster,
                PrivilegeAccess = EnumPrivilegeAccess.菜单按钮
            });

            //获取拥有的功能项信息
            IList<SystemPermission> haveFunctions =
              (await GetPermissionByPrivilegeMasterValue(
                new SystemPermissionByPrivilegeMasterValueInput()
                {
                    PrivilegeAccess = EnumPrivilegeAccess.菜单按钮,
                    PrivilegeMasterValue = privilegeMasterValue,
                    PrivilegeMaster = privilegeMaster
                })).ToList();

            //获取所有功能项
            IList<SystemMenuButtonOutput> functionDtos = (await _menuButtonRepository.GetMenuButtonByMenuId()).ToList();
            foreach (var menu in menus)
            {
                var function = functionDtos.Where(w => w.MenuId == (Guid)menu.id).OrderBy(o => o.OrderNo);
                foreach (var f in function)
                {
                    var selectFunction = haveFunctions.Where(w => w.PrivilegeAccessValue == f.MenuButtonId);
                    f.Remark = selectFunction.Any() ? "selected" : "";
                    functions.Add(f);
                }
            }
            return functions;
        }

        /// <summary>
        ///     查询对应拥有的功能项菜单信息
        /// </summary>
        /// <param name="privilegeMasterValue">信息</param>
        /// <param name="privilegeMaster"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemData>> GetDataByPrivilegeMaster(Guid privilegeMasterValue,
            EnumPrivilegeMaster privilegeMaster)
        {
            IList<SystemData> datas = new List<SystemData>();
            //获取拥有的菜单信息
            var menus = await GetMenuHavePermissionByPrivilegeMasterValue(new SystemPermissiontMenuHaveByPrivilegeMasterValueInput
            {
                PrivilegeMasterValue = privilegeMasterValue,
                PrivilegeMaster = privilegeMaster,
                PrivilegeAccess = EnumPrivilegeAccess.数据权限
            });

            //获取拥有的功能项信息
            IList<SystemPermission> haveDatas = (await
                GetPermissionByPrivilegeMasterValue(
                new SystemPermissionByPrivilegeMasterValueInput()
                {
                    PrivilegeAccess = EnumPrivilegeAccess.数据权限,
                    PrivilegeMasterValue = privilegeMasterValue,
                    PrivilegeMaster = privilegeMaster
                })).ToList();

            //获取所有功能项
            IList<SystemDataOutput> functionDtos = (await _dataRepository.GetDataByMenuId()).ToList();
            foreach (var menu in menus)
            {
                var function = functionDtos.Where(w => w.MenuId == (Guid)menu.id).OrderBy(o => o.OrderNo);
                foreach (var f in function)
                {
                    var selectFunction = haveDatas.Where(w => w.PrivilegeAccessValue == f.DataId);
                    f.Remark = selectFunction.Any() ? "selected" : "";
                    datas.Add(f);
                }
            }
            return datas;
        }


        /// <summary>
        ///     获取登录人员对应菜单下的功能项
        /// </summary>
        /// <param name="mvcRote">路由信息</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemMenuButton>> GetFunctionByMenuIdAndUserId(MvcRote mvcRote,
            Guid userId)
        {
            //判断当前人员是否为超级管理员若是超级管理员则具有最大权限
            IList<SystemMenuButton> functions = new List<SystemMenuButton>();
            //判断该用户是否为超级管理员:若是超级管理员则显示所有菜单
            var userInfo = await _userInfoRepository.FindByIdAsync(userId);
            if (userInfo != null)
            {
                //如果是超级管理员
                if (userInfo.IsAdmin)
                {
                    return (await _menuButtonRepository.GetMenuButtonByMvcRote(mvcRote)).ToList();
                }
                functions = (await _menuButtonRepository.GetMenuButtonByMenuIdAndUserId(mvcRote, userId)).ToList();
            }
            return functions;
        }

        /// <summary>
        ///     获取菜单、功能项等被使用的权限信息
        /// </summary>
        /// <param name="privilegeAccess">类型:菜单、功能项</param>
        /// <param name="privilegeAccessValue">对应值</param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemPermission>> GetSystemPermissionsByPrivilegeAccessAndValue(
            EnumPrivilegeAccess privilegeAccess,
            Guid? privilegeAccessValue = null)
        {
            return (await _permissionRepository.GetSystemPermissionsByPrivilegeAccessAndValue(privilegeAccess, privilegeAccessValue)).ToList();
        }

        /// <summary>
        /// 获取角色，组等具有的权限
        /// </summary>
        /// <param name="privilegeMaster">类型:角色，人员，组等</param>
        /// <param name="privilegeMasterValue">对应值</param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemPermission>> GetSystemPermissionsByPrivilegeMasterValueAndValue(EnumPrivilegeMaster privilegeMaster,
            Guid? privilegeMasterValue = null)
        {
            return (await _permissionRepository.GetSystemPermissionsByPrivilegeMasterValueAndValue(privilegeMaster, privilegeMasterValue)).ToList();
        }

        /// <summary>
        /// 根据功能项获取权限信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemPermission>> GetSystemPermissionsMvcRote(SystemPermissionsByMvcRoteInput input)
        {
            return (await _permissionRepository.GetSystemPermissionsMvcRote(input)).ToList();
        }

        /// <summary>
        /// 获取字段权限Sql
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> GetFieldPermissionSql(SystemPermissionSqlInput input)
        {
            StringBuilder sql = new StringBuilder();
            //拼接字段权限Sql
            IList<SystemField> fields = (await _permissionRepository.GetFieldPermission(input)).ToList();
            //是否具有字段权限
            foreach (var field in fields)
            {
                sql.Append(field.SqlField + ",");
            }
            return sql.Length > 0 ? sql.ToString().TrimEnd(',') : "*";
        }

        /// <summary>
        /// 获取数据权限Sql
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> GetDataPermissionSql(SystemPermissionSqlInput input)
        {
            StringBuilder stringBuilder = new StringBuilder();
            //获取当前用户是否为超级管理员
            var userInfo = await _userInfoRepository.FindByIdAsync(input.PrincipalUser.UserId);
            if (userInfo.IsAdmin)
            {
                return "  1=1";
            }
            IList<SystemData> datas = (await _permissionRepository.GetDataPermission(input)).ToList();
            if (datas.Any())
            {
                foreach (var data in datas)
                {
                    if (!data.RuleSql.IsNullOrEmpty())
                    {
                        //替换Html标签
                        data.RuleSql = data.RuleSql.ReplaceHtmlTag();
                        //是否具有规则数据
                        if (!data.RuleJson.IsNullOrEmpty())
                        {
                            IList<SystemDataRuleJsonDoubleWay> ruleJsons = JsonConvert.DeserializeObject<IList<SystemDataRuleJsonDoubleWay>>(data.RuleJson).ToList(); 
                            foreach (var ruleJson in ruleJsons)
                            {
                                //替换Sql
                                data.RuleSql = data.RuleSql.Replace(ruleJson.Field, ruleJson.Value.InSql());
                            }
                        }
                        //替换固定信息
                        data.RuleSql = await GetRuleSql(data.RuleSql, input.PrincipalUser.UserId);
                        //追加替换后的Sql
                        stringBuilder.Append(data.RuleSql + " OR ");
                    }

                }
            }
            //去除最后一个OR
            string sql = stringBuilder.ToString();
            return sql.Contains("OR") ? sql.Substring(0, sql.Length - 3) : sql;
        }

        /// <summary>
        /// 替换规则Sql
        /// </summary>
        /// <param name="ruleSql"></param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        private async Task<string> GetRuleSql(string ruleSql, Guid userId)
        {
            //获取角色、组、岗位数据
            IList<SystemPrivilegeDetailListOutput> privilegeDetailDtos = (await _permissionUsernRepository.GetSystemPrivilegeDetailOutputsByUserId(new IdInput { Id = userId })).ToList();

            if (ruleSql.Contains("{所有}"))
            {
                ruleSql = ruleSql.Replace("{所有}", "1=1");
            }
            if (ruleSql.Contains("{当前用户}"))
            {
                ruleSql = ruleSql.Replace("{当前用户}", userId.ToString());
            }
            if (ruleSql.Contains("{所在组织}"))
            {
                //获取当前人员所在组织
                ruleSql = ruleSql.Replace("{所在组织}", privilegeDetailDtos.Where(w => w.PrivilegeMaster == EnumPrivilegeMaster.组织机构).Select(d => d.PrivilegeMasterValue).ToList().ExpandAndToString().InSql());
            }
            if (ruleSql.Contains("{所在组织及下级组织}"))
            {
                //查找机构
                var orgId = privilegeDetailDtos.Where(w => w.PrivilegeMaster == EnumPrivilegeMaster.组织机构)
                    .Select(d => d.PrivilegeMasterValue);
                //获取当前人员所在组织及下级组织
                ruleSql = ruleSql.Replace("{所在组织及下级组织}", (await _organizationRepository.GetOrganizationsByParentId(new SystemOrganizationsByParentIdInput { Id = orgId.FirstOrDefault() })).Select(s => s.OrganizationId).ToList().ExpandAndToString().InSql());
            }
            if (ruleSql.Contains("{所在组织代码}"))
            {
                //获取当前人员所在组织
                ruleSql = ruleSql.Replace("{所在组织代码}", privilegeDetailDtos.Where(w => w.PrivilegeMaster == EnumPrivilegeMaster.组织机构).Select(d => d.PrivilegeMasterValue).ToList().ExpandAndToString().InSql());
            }
            if (ruleSql.Contains("{所在岗位}"))
            {
                //获取当前人员所在岗位
                ruleSql = ruleSql.Replace("{所在岗位}", privilegeDetailDtos.Where(w => w.PrivilegeMaster == EnumPrivilegeMaster.岗位).Select(d => d.PrivilegeMasterValue).ToList().ExpandAndToString().InSql());
            }
            if (ruleSql.Contains("{所在工作组}"))
            {
                //获取当前人员所在工作组
                ruleSql = ruleSql.Replace("{所在工作组}", privilegeDetailDtos.Where(w => w.PrivilegeMaster == EnumPrivilegeMaster.组).Select(d => d.PrivilegeMasterValue).ToList().ExpandAndToString().InSql());
            }
            return ruleSql;
        }

        public async Task<int> DeleteSystemPermissionsByPrivilegeAccessAndValue(EnumPrivilegeAccess privilegeAccess, Guid? privilegeAccessValue = null)
        {
            return await _permissionRepository.DeleteSystemPermissionsByPrivilegeAccessAndValue(privilegeAccess, privilegeAccessValue);
        }
        #endregion
    }
}