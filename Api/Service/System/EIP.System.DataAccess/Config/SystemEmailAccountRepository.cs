using EIP.Common.DataAccess;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Config
{
    /// <summary>
    ///    邮箱账号
    /// </summary>
    public class SystemEmailAccountRepository : DapperAsyncRepository<SystemEmailAccount>, ISystemEmailAccountRepository
    {
       
    }
}