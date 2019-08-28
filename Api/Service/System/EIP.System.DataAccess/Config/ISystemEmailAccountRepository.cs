using EIP.Common.DataAccess;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Config
{
    /// <summary>
    /// 邮箱账号
    /// </summary>
    public interface ISystemEmailAccountRepository : IAsyncRepository<SystemEmailAccount>
    {
      
    }
}