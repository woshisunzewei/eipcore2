using EIP.Common.Business;
using EIP.Common.Core.Extensions;
using EIP.Common.Core.Utils;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.System.DataAccess.Permission;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using EIP.System.Models.Enums;
using EIP.System.Models.Resx;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIP.System.Business.Permission
{
    /// <summary>
    ///     数据权限规则业务逻辑
    /// </summary>
    public class SystemDataLogic : DapperAsyncLogic<SystemData>, ISystemDataLogic
    {
        #region 构造函数

        private readonly ISystemDataRepository _dataRepository;
        private readonly ISystemPermissionRepository _permissionRepository;
        private readonly ISystemMenuRepository _menuRepository;
        public SystemDataLogic(ISystemDataRepository dataRepository, ISystemPermissionRepository permissionRepository, ISystemMenuRepository menuRepository)
            : base(dataRepository)
        {
            _dataRepository = dataRepository;
            _permissionRepository = permissionRepository;
            _menuRepository = menuRepository;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     根据菜单Id获取数据权限规则
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async  Task<IEnumerable<SystemDataOutput>> GetDataByMenuId(SystemDataGetDataByMenuIdInput input)
        {
            var datas= (await _dataRepository.GetDataByMenuId(input)).ToList();
            var menus = (await _menuRepository.FindAllAsync()).ToList();
            foreach (var item in datas)
            {
                var menu = menus.FirstOrDefault(w => w.MenuId == item.MenuId);
                if (menu != null && !menu.ParentIds.IsNullOrEmpty())
                {
                    foreach (var parent in menu.ParentIds.Split(','))
                    {
                        //查找上级
                        var dicinfo = menus.FirstOrDefault(w => w.MenuId.ToString() == parent);
                        if (dicinfo != null) item.MenuNames += dicinfo.Name + ">";
                    }
                    if (!item.MenuNames.IsNullOrEmpty())
                        item.MenuNames = item.MenuNames.TrimEnd('>');
                }
            }
            return datas;
        }

        /// <summary>
        ///     保存数据权限规则
        /// </summary>
        /// <param name="data">数据权限规则</param>
        /// <returns></returns>
        public async  Task<OperateStatus> SaveData(SystemDataOutput data)
        {
            SystemData systemData = data.MapTo<SystemData>();
            if (data.DataId.IsEmptyGuid())
            {
                systemData.DataId = CombUtil.NewComb();
                return await InsertAsync(systemData);
            }
            return await UpdateAsync(systemData);
        }

        /// <summary>
        ///     删除数据权限规则信息
        /// </summary>
        /// <param name="input">数据权限规则Id</param>
        /// <returns></returns>
        public async  Task<OperateStatus> DeleteByDataId(IdInput input)
        {
            var operateStatus = new OperateStatus();
            //查看该功能项是否已被特性占用
            var permissions =await _permissionRepository.GetSystemPermissionsByPrivilegeAccessAndValue(EnumPrivilegeAccess.数据权限, input.Id);
            if (permissions.Any())
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message =  ResourceSystem.已被赋予权限;
                return operateStatus;
            }
            return await DeleteAsync(new SystemData()
            {
                DataId = input.Id
            });
        }

        #endregion
    }
}