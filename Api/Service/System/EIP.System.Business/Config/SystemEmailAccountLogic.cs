using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Core.Extensions;
using EIP.Common.Core.Utils;
using EIP.Common.Entities;
using EIP.System.DataAccess.Config;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Config
{
    /// <summary>
    ///     邮箱账号
    /// </summary>
    public class SystemEmailAccountLogic : DapperAsyncLogic<SystemEmailAccount>, ISystemEmailAccountLogic
    {
        #region 构造函数
        private readonly ISystemEmailAccountRepository _emailAccountRepository;
        public SystemEmailAccountLogic(ISystemEmailAccountRepository emailAccountRepository)
            : base(emailAccountRepository)
        {
            _emailAccountRepository = emailAccountRepository;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     保存数据库配置
        /// </summary>
        /// <param name="emailAccount">数据库配置</param>
        /// <returns></returns>
        public async Task<OperateStatus> SaveEmailAccount(SystemEmailAccount emailAccount)
        {
            if (!emailAccount.EmailAccountId.IsEmptyGuid())
                return await UpdateAsync(emailAccount);
            emailAccount.EmailAccountId= CombUtil.NewComb();
            return await InsertAsync(emailAccount);
        }
        #endregion
    }
}