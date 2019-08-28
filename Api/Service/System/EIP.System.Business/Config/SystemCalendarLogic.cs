using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.System.DataAccess.Config;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Config
{
    /// <summary>
    ///     系统配置文件接口实现
    /// </summary>
    public class SystemCalendarLogic : DapperAsyncLogic<SystemCalendar>, ISystemCalendarLogic
    {
        public Task<IEnumerable<SystemCalendar>> QueryCalendars(DateTime st, DateTime ed, Guid userId)
        {
            return _calendarRepository.QueryCalendars(st, ed, userId);
        }

        #region 构造函数

        private readonly ISystemCalendarRepository _calendarRepository;

        public SystemCalendarLogic(ISystemCalendarRepository calendarRepository)
            : base(calendarRepository)
        {
            _calendarRepository = calendarRepository;
        }

        #endregion
    }
}