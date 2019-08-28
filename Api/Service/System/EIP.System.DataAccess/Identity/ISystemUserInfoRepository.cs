using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Paging;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Identity
{
    public interface ISystemUserInfoRepository : IAsyncRepository<SystemUserInfo>
    {
        /// <summary>
        ///     根据用户名和密码查询用户信息
        ///     1:用户登录使用
        /// </summary>
        /// <param name="input">用户名、密码等</param>
        /// <returns></returns>
        Task<SystemUserLoginOutput> CheckUserByCodeAndPwd(SystemUserLoginInput input);

        /// <summary>
        ///     更新最后登录时间
        /// </summary>
        /// <param name="input">用户Id</param>
        /// <returns></returns>
        Task<int> UpdateLastLoginTime(IdInput input);

        /// <summary>
        /// 根据id获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SystemUserGetByIdOutput> GetById(IdInput input);

        /// <summary>
        ///     更新第一次登录时间
        /// </summary>
        /// <param name="input">用户Id</param>
        /// <returns></returns>
        Task<int> UpdateFirstVisitTime(IdInput input);

        /// <summary>
        ///     复杂查询分页方式
        /// </summary>
        /// <param name="paging">查询参数</param>
        /// <returns>分页</returns>
        Task<PagedResults<SystemUserOutput>> PagingUserQuery(SystemUserPagingInput paging);
        
        /// <summary>
        ///     检查代码:唯一性检查
        /// </summary>
        /// <param name="input">代码</param>
        /// <returns></returns>
        Task<bool> CheckUserCode(CheckSameValueInput input);

        /// <summary>
        ///     获取所有用户
        /// </summary>
        /// <param name="input">是否冻结</param>
        /// <returns></returns>
        Task<IEnumerable<SystemChosenUserOutput>> GetChosenUser(FreezeInput input = null);

        /// <summary>
        ///     获取所有用户
        /// </summary>
        /// <param name="input">是否冻结</param>
        /// <returns></returns>
        Task<IEnumerable<SystemUserInfo>> GetUser(FreezeInput input = null);

        /// <summary>
        ///     根据用户Id重置某人密码
        /// </summary>
        /// <param name="input">重置密码参数</param>
        /// <returns></returns>
        Task<bool> ResetPassword(SystemUserResetPasswordInput input);

        /// <summary>
        ///     保存用户头像
        /// </summary>
        /// <param name="input">用户头像</param>
        /// <returns></returns>
        Task<bool> SaveHeadImage(SystemUserSaveHeadImageInput input);

        /// <summary>
        ///     检查代码:唯一性检查
        /// </summary>
        /// <param name="input">代码</param>
        /// <returns></returns>
        Task<bool> CheckOldPassword(CheckSameValueInput input);
    }
}