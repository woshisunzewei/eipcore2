using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.System.Models.Entities;
using EIP.Common.Entities.Dtos;

namespace EIP.System.Business.Config
{
	/// <summary>
    /// 文章新闻表业务逻辑接口
    /// </summary>
    public interface ISystemArticleLogic : IAsyncLogic<SystemArticle>
    {
        /// <summary>
        ///     保存
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        Task<OperateStatus> Save(SystemArticle model);

        /// <summary>
        /// 保存浏览次数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperateStatus> SaveViewNums(IdInput input);
    }
}