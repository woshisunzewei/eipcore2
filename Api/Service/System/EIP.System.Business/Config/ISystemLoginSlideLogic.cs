using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Config
{
	/// <summary>
    /// 登录幻灯片业务逻辑接口
    /// </summary>
    public interface ISystemLoginSlideLogic : IAsyncLogic<SystemLoginSlide>
    {
        /// <summary>
        ///     保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<OperateStatus> Save(SystemLoginSlide entity);
    }
}