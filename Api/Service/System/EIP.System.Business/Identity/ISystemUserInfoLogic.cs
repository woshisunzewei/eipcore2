using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Dtos.Reports;
using EIP.Common.Entities.Paging;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Identity
{
    /// <summary>
    ///     用户业务逻辑
    /// </summary>
    public interface ISystemUserInfoLogic : IAsyncLogic<SystemUserInfo>
    {
        /// <summary>
        ///     根据登录代码和密码查询用户信息
        /// </summary>
        /// <param name="input">用户名、密码等</param>
        /// <returns></returns>
        Task<OperateStatus<SystemUserLoginOutput>> CheckUserByCodeAndPwd(SystemUserLoginInput input);

        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SystemUserGetByIdOutput> GetById(IdInput input);

        /// <summary>
        ///     分页查询
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        Task<PagedResults<SystemUserOutput>> PagingUserQuery(SystemUserPagingInput paging);

        /// <summary>
        ///     Excel导出方式
        /// </summary>
        /// <param name="paging">查询参数</param>
        /// <param name="excelReportDto"></param>
        /// <returns></returns>
        Task<OperateStatus> ReportExcelUserQuery(SystemUserPagingInput paging,
            ExcelReportDto excelReportDto);

        /// <summary>
        ///     检测配置项代码是否已经具有重复项
        /// </summary>
        /// <param name="input">需要验证的参数</param>
        /// <returns></returns>
        Task<OperateStatus> CheckUserCode(CheckSameValueInput input);

        /// <summary>
        ///     保存人员信息
        /// </summary>
        /// <param name="input">人员信息</param>
        /// <returns></returns>
        Task<OperateStatus> SaveUser(SystemUserSaveInput input);

        /// <summary>
        ///     获取所有用户
        /// </summary>
        /// <param name="input">是否冻结</param>
        /// <returns></returns>
        Task<IEnumerable<SystemChosenUserOutput>> GetChosenUser(FreezeInput input = null);

        /// <summary>
        ///     删除用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperateStatus> DeleteUser(IdInput input);

        /// <summary>
        ///     根据用户Id重置某人密码
        /// </summary>
        /// <param name="input">用户Id</param>
        /// <returns></returns>
        Task<OperateStatus> ResetPassword(SystemUserResetPasswordInput input);

        /// <summary>
        ///     保存用户头像
        /// </summary>
        /// <param name="input">用户头像</param>
        /// <returns></returns>
        Task<OperateStatus> SaveHeadImage(SystemUserSaveHeadImageInput input);

        /// <summary>
        /// 获取角色用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SystemUserGetRoleUsersOutput> GetChosenPrivilegeMasterUser(SystemUserGetChosenPrivilegeMasterUser input);

        /// <summary>
        /// 保存修改后密码信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
       Task< OperateStatus> SaveChangePassword(SystemUserChangePasswordInput input);

        /// <summary>
        ///     验证旧密码是否输入正确
        /// </summary>
        /// <param name="input">需要验证的参数</param>
        /// <returns></returns>
        Task<OperateStatus> CheckOldPassword(CheckSameValueInput input);

        /// <summary>
        ///     根据用户Id获取该用户信息
        /// </summary>
        /// <param name="input">用户Id</param>
        /// <returns></returns>
        Task<SystemUserDetailOutput> GetDetailByUserId(IdInput input);
    }
}