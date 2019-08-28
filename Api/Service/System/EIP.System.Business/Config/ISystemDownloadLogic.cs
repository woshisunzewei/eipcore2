using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Config
{
	/// <summary>
    /// 文章下载记录表业务逻辑接口
    /// </summary>
    public interface ISystemDownloadLogic : IAsyncLogic<SystemDownload>
    {
        /// <summary>
        ///     保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<OperateStatus> Save(SystemDownload entity);
    }
}