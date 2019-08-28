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
    ///     登录幻灯片业务逻辑接口实现
    /// </summary>
    public class SystemLoginSlideLogic : DapperAsyncLogic<SystemLoginSlide>, ISystemLoginSlideLogic
    {
        #region 构造函数
        private readonly ISystemLoginSlideRepository _systemLoginSlideRepository;
        public SystemLoginSlideLogic(ISystemLoginSlideRepository systemLoginSlideRepository)
            : base(systemLoginSlideRepository)
        {
            _systemLoginSlideRepository = systemLoginSlideRepository;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<OperateStatus> Save(SystemLoginSlide entity)
        {
            if (!entity.LoginSlideId.IsEmptyGuid())
                return await UpdateAsync(entity);
            entity.LoginSlideId = CombUtil.NewComb();
            return await InsertAsync(entity);
        }
        #endregion
    }
}