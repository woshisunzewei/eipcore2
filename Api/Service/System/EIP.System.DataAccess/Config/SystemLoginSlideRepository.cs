using EIP.Common.DataAccess;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Config
{
    /// <summary>
    ///     登录幻灯片数据访问接口实现
    /// </summary>
    public class SystemLoginSlideRepository : DapperAsyncRepository<SystemLoginSlide>, ISystemLoginSlideRepository
    {
    }
}