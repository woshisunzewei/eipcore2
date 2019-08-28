using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Config
{
    public interface ISystemEmailAccountLogic : IAsyncLogic<SystemEmailAccount>
    {
        /// <summary>
        ///     保存邮箱账号
        /// </summary>
        /// <param name="emailAccount">邮箱账号</param>
        /// <returns></returns>
        Task<OperateStatus> SaveEmailAccount(SystemEmailAccount emailAccount);
    }
}