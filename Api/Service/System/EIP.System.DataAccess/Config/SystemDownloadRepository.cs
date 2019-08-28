using EIP.Common.DataAccess;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Config
{
    /// <summary>
    ///     文章下载记录表数据访问接口实现
    /// </summary>
    public class SystemDownloadRepository : DapperAsyncRepository<SystemDownload>, ISystemDownloadRepository
    {
    }
}