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
    ///     文章下载记录表业务逻辑接口实现
    /// </summary>
    public class SystemDownloadLogic : DapperAsyncLogic<SystemDownload>, ISystemDownloadLogic
    {
        #region 构造函数
        private readonly ISystemDownloadRepository _systemDownloadRepository;
        public SystemDownloadLogic(ISystemDownloadRepository systemDownloadRepository)
            : base(systemDownloadRepository)
        {
            _systemDownloadRepository = systemDownloadRepository;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<OperateStatus> Save(SystemDownload entity)
        {
            if (!entity.DownloadId.IsEmptyGuid())
                return await UpdateAsync(entity);
            entity.DownloadId = CombUtil.NewComb();
            return await InsertAsync(entity);
        }
        #endregion
    }
}