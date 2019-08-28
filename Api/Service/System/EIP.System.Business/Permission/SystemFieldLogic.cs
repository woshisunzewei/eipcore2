using System.Linq;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Core.Extensions;
using EIP.Common.Core.Utils;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Paging;
using EIP.System.DataAccess.Permission;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using EIP.System.Models.Enums;
using EIP.System.Models.Resx;

namespace EIP.System.Business.Permission
{
    /// <summary>
    ///     系统字段项业务逻辑
    /// </summary>
    public class SystemFieldLogic : DapperAsyncLogic<SystemField>, ISystemFieldLogic
    {
        #region 构造函数

        private readonly ISystemFieldRepository _fieldRepository;
        private readonly ISystemPermissionRepository _permissionRepository;

        public SystemFieldLogic(ISystemFieldRepository fieldRepository, ISystemPermissionRepository permissionRepository)
            : base(fieldRepository)
        {
            _fieldRepository = fieldRepository;
            _permissionRepository = permissionRepository;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     根据菜单Id获取字段信息
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        public async Task<PagedResults<SystemFieldOutput>> GetFieldByMenuId(SystemFieldPagingInput paging)
        {
            return await _fieldRepository.GetFieldByMenuId(paging);
        }

        /// <summary>
        ///     保存字段信息
        /// </summary>
        /// <param name="field">字段信息</param>
        /// <returns></returns>
        public async Task<OperateStatus> SaveField(SystemField field)
        {
            if (field.FieldId.IsEmptyGuid())
            {
                field.FieldId = CombUtil.NewComb();
                return await InsertAsync(field);
            }
            return await UpdateAsync(field);
        }

        /// <summary>
        ///     删除字段信息
        /// </summary>
        /// <param name="input">字段Id</param>
        /// <returns></returns>
        public async Task<OperateStatus> DeleteField(IdInput input)
        {
            var operateStatus = new OperateStatus();
            //查看该功能项是否已被特性占用
            var permissions = await _permissionRepository.GetSystemPermissionsByPrivilegeAccessAndValue(EnumPrivilegeAccess.字段, input.Id);
            if (permissions.Any())
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message = ResourceSystem.已被赋予权限;
                return operateStatus;
            }
            return await DeleteAsync(new SystemField { FieldId = input.Id });
        }

        #endregion
    }
}