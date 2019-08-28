using EIP.Common.DataAccess;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Config
{
    /// <summary>
    ///     文章新闻表数据访问接口实现
    /// </summary>
    public class SystemArticleRepository : DapperAsyncRepository<SystemArticle>, ISystemArticleRepository
    {
    }
}